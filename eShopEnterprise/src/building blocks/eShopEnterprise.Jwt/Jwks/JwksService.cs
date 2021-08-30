using eShopEnterprise.Jwt.Interfaces;
using eShopEnterprise.Jwt.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace eShopEnterprise.Jwt.Jwks
{
    /// <summary>
    /// Util class to allow restoring RSA/ECDsa parameters from JSON as the normal
    /// parameters class won't restore private key info.
    /// </summary>
    public class JwksService : IJsonWebKeySetService
    {
        private readonly IJsonWebKeyStore _store;
        private readonly IJsonWebKeyService _jwkService;
        private readonly IOptions<JwksOptions> _options;

        public JwksService(IJsonWebKeyStore store, IJsonWebKeyService jwkService, IOptions<JwksOptions> options)
        {
            _store = store;
            _jwkService = jwkService;
            _options = options;
        }

        public SigningCredentials GenerateSigningCredentials(JwksOptions options = null)
        {
            if (options == null)
                options = _options.Value;
            var key = _jwkService.Generate(options.Jws);
            var t = new SecurityKeyWithPrivate();
            t.SetJwsParameters(key, options.Jws);
            _store.Save(t);

            return new SigningCredentials(key, options.Jws);
        }

        public EncryptingCredentials GenerateEncryptingCredentials(JwksOptions options = null)
        {
            if (options == null)
                options = _options.Value;
            var key = _jwkService.Generate(options.Jwe);
            var t = new SecurityKeyWithPrivate();
            t.SetJweParameters(key, options.Jwe);
            _store.Save(t);

            return new EncryptingCredentials(key, options.Jwe, options.Jwe.Encryption);
        }

        private void RemovePrivateKeys(JsonWebKeyType jsonWebKeyType)
        {
            foreach (var securityKeyWithPrivate in _store.Get(jsonWebKeyType, _options.Value.AlgorithmsToKeep))
            {
                _store.Revoke(securityKeyWithPrivate);
            }
        }

        private bool CheckCompatibility(SecurityKeyWithPrivate currentKey, JwksOptions options)
        {
            if (options == null)
                options = _options.Value;

            if (currentKey.JwkType == JsonWebKeyType.Jws)
            {
                if (currentKey.JwsAlgorithm == options.Jws) return true;
                GenerateSigningCredentials(options);
            }

            if (currentKey.JwkType == JsonWebKeyType.Jwe)
            {
                if (currentKey.JweAlgorithm == options.Jwe) return true;
                GenerateEncryptingCredentials(options);
            }


            return false;

        }

        /// <summary>
        /// If current doesn't exist will generate new one
        /// </summary>
        public SigningCredentials GetCurrentSigningCredentials(JwksOptions options = null)
        {
            if (_store.NeedsUpdate(JsonWebKeyType.Jws))
            {
                // According NIST - https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-57pt1r4.pdf - Private key should be removed when no longer needs
                RemovePrivateKeys(JsonWebKeyType.Jws);
                return GenerateSigningCredentials(options);
            }

            var currentKey = _store.GetCurrentKey(JsonWebKeyType.Jws);

            // options has change. Change current key
            if (!CheckCompatibility(currentKey, options))
                currentKey = _store.GetCurrentKey(JsonWebKeyType.Jws);

            return currentKey.GetSigningCredentials();
        }

        public IReadOnlyCollection<JsonWebKey> GetLastKeysCredentials(JsonWebKeyType jsonWebKeyType, int qty)
        {
            // sometimes current force update
            var store = _store.Get(jsonWebKeyType, qty);
            if (!store.Any())
            {
                GetCurrentSigningCredentials();
                return _store.Get(jsonWebKeyType, qty).OrderByDescending(o => o.CreationDate).Select(s => s.GetSecurityKey()).ToList().AsReadOnly();
            }

            var keys = store.Where(w => w.JwkType == jsonWebKeyType).OrderByDescending(o => o.CreationDate);
            return keys.Select(s => s.GetSecurityKey()).ToList().AsReadOnly();
        }
    }
}
