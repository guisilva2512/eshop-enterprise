using eShopEnterprise.Jwt.Jwk;
using Microsoft.IdentityModel.Tokens;
using System;

namespace eShopEnterprise.Jwt
{
    public class JwksOptions
    {
        public JwsAlgorithm Jws { get; set; } = JwsAlgorithm.ES256;
        public JweAlgorithm Jwe { get; set; } = JweAlgorithm.RsaOAEP.WithEncryption(Encryption.Aes128CbcHmacSha256);
        public int DaysUntilExpire { get; set; } = 90;
        public string KeyPrefix { get; set; } = $"{Environment.MachineName}_";
        public int AlgorithmsToKeep { get; set; } = 2;
        public TimeSpan CacheTime { get; set; } = TimeSpan.FromMinutes(15);
    }

    public abstract class Algorithm
    {
        public KeyType KeyType { get; internal set; }
        public string Curve { get; internal set; }
        public string Alg { get; internal set; }


        /// <summary>
        /// See RFC 7518 - JSON Web Algorithms (JWA) - Section 6.1. "kty" (Key Type) Parameter Values
        /// </summary>
        public string Kty()
        {
            return KeyType switch
            {
                KeyType.RSA => JsonWebAlgorithmsKeyTypes.RSA,
                KeyType.ECDsa => JsonWebAlgorithmsKeyTypes.EllipticCurve,
                KeyType.HMAC => JsonWebAlgorithmsKeyTypes.Octet,
                KeyType.AES => JsonWebAlgorithmsKeyTypes.Octet,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        public static implicit operator string(Algorithm value) => value.Alg;
    }

    public sealed class JwsAlgorithm : Algorithm
    {
        // HMAC
        public static readonly JwsAlgorithm HS256 = new JwsAlgorithm(SecurityAlgorithms.HmacSha256, KeyType.HMAC);
        public static readonly JwsAlgorithm HS384 = new JwsAlgorithm(SecurityAlgorithms.HmacSha384, KeyType.HMAC);
        public static readonly JwsAlgorithm HS512 = new JwsAlgorithm(SecurityAlgorithms.HmacSha512, KeyType.HMAC);

        // RSA
        public static readonly JwsAlgorithm RS256 = new JwsAlgorithm(SecurityAlgorithms.RsaSha256, KeyType.RSA);
        public static readonly JwsAlgorithm RS384 = new JwsAlgorithm(SecurityAlgorithms.RsaSha384, KeyType.RSA);
        public static readonly JwsAlgorithm RS512 = new JwsAlgorithm(SecurityAlgorithms.RsaSha512, KeyType.RSA);
        public static readonly JwsAlgorithm PS256 = new JwsAlgorithm(SecurityAlgorithms.RsaSsaPssSha256, KeyType.RSA);
        public static readonly JwsAlgorithm PS384 = new JwsAlgorithm(SecurityAlgorithms.RsaSsaPssSha384, KeyType.RSA);
        public static readonly JwsAlgorithm PS512 = new JwsAlgorithm(SecurityAlgorithms.RsaSsaPssSha512, KeyType.RSA);

        // Elliptic Curve
        public static readonly JwsAlgorithm ES256 = new JwsAlgorithm(SecurityAlgorithms.EcdsaSha256, KeyType.ECDsa, JsonWebKeyECTypes.P256);
        public static readonly JwsAlgorithm ES384 = new JwsAlgorithm(SecurityAlgorithms.EcdsaSha384, KeyType.ECDsa, JsonWebKeyECTypes.P384);
        public static readonly JwsAlgorithm ES512 = new JwsAlgorithm(SecurityAlgorithms.EcdsaSha512, KeyType.ECDsa, JsonWebKeyECTypes.P521);

        // Not supported
        // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/wiki/Supported-Algorithms
        // public static readonly Algorithm A192KW = new Algorithm("A192KW", KeyType.AES); -> Not supported

        private JwsAlgorithm(string alg, KeyType keyType, string curve)
        {
            Alg = alg;
            KeyType = keyType;
            Curve = curve;
        }

        private JwsAlgorithm(string alg, KeyType keyType)
        {
            this.Alg = alg;
            this.KeyType = keyType;
        }


        public static JwsAlgorithm Create(string value, KeyType key)
        {
            return new JwsAlgorithm(value, key);
        }


        public static implicit operator string(JwsAlgorithm value) => value.Alg;

    }

    /// <summary>
    ///  See RFC 7518 - JSON Web Algorithms (JWA) 
    /// - Section 7.1. JSON Web Signature and Encryption Algorithms Registry
    /// - Section 4.1.  "alg" (Algorithm) Header Parameter Values for JWE
    /// - Section 5.1.  "enc" (Encryption) Header Parameter Values for JWE
    /// </summary>
    public sealed class JweAlgorithm : Algorithm
    {
        // See: https://tools.ietf.org/html/rfc7518#section-5.1
        public static readonly JweAlgorithm RsaOAEP = new JweAlgorithm(SecurityAlgorithms.RsaOAEP, KeyType.RSA);
        public static readonly JweAlgorithm RSA1_5 = new JweAlgorithm(SecurityAlgorithms.RsaPKCS1, KeyType.RSA);
        public static readonly JweAlgorithm A128KW = new JweAlgorithm(SecurityAlgorithms.Aes128KW, KeyType.AES);
        public static readonly JweAlgorithm A256KW = new JweAlgorithm(SecurityAlgorithms.Aes256KW, KeyType.AES);

        public string Encryption { get; private set; }
        private JweAlgorithm(string alg, KeyType keyType, string curve)
        {
            Alg = alg;
            KeyType = keyType;
            Curve = curve;
        }

        private JweAlgorithm(string alg, KeyType keyType)
        {
            this.Alg = alg;
            this.KeyType = keyType;
        }

        public JweAlgorithm WithEncryption(Encryption encryption)
        {
            Encryption = encryption;
            return this;
        }
        public JweAlgorithm WithEncryption(string encryption)
        {
            Encryption = encryption;
            return this;
        }

        public static JweAlgorithm Create(string alg, KeyType key)
        {
            return new JweAlgorithm(alg, key);
        }



    }

    /// <summary>
    ///  See RFC 7518 - JSON Web Algorithms (JWA) 
    /// - Section 7.1. JSON Web Signature and Encryption Algorithms Registry
    /// - Section 4.1.  "alg" (Algorithm) Header Parameter Values for JWS
    /// </summary>
    public sealed class Encryption
    {
        // HMAC
        public static readonly Encryption Aes128CbcHmacSha256 = new Encryption(SecurityAlgorithms.Aes128CbcHmacSha256);
        public static readonly Encryption Aes192CbcHmacSha384 = new Encryption(SecurityAlgorithms.Aes192CbcHmacSha384);
        public static readonly Encryption Aes256CbcHmacSha512 = new Encryption(SecurityAlgorithms.Aes256CbcHmacSha512);

        public string Enc { get; }

        private Encryption(string enc)
        {
            this.Enc = enc;
        }

        public static Encryption Create(string alg)
        {
            return new Encryption(alg);
        }

        public static implicit operator string(Encryption value) => value.Enc;

    }
}
