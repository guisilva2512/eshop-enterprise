using eShopEnterprise.Core.Messages.Integration;
using eShopEnterprise.Identidade.API.Models;
using eShopEnterprise.MessageBus;
using eShopEnterprise.WebApi.Core.Controllers;
using eShopEnterprise.WebApi.Core.Identidade;
using eShopEnterprise.WebApi.Core.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopEnterprise.Identidade.API.Controllers
{
    [Route("api/identidade")]
    public class IdentidadeController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IMessageBus _bus;

        private readonly IJsonWebKeySetService _jwksService;
        private readonly IAspNetUser _aspNetUser;

        public IdentidadeController(
                SignInManager<IdentityUser> signInManager, 
                UserManager<IdentityUser> userManager, 
                IOptions<AppSettings> appSettings,
                IMessageBus bus,
                IJsonWebKeySetService jwksService,
                IAspNetUser aspNetUser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _bus = bus;

            _jwksService = jwksService;
            _aspNetUser = aspNetUser;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                var clienteResult = await RegistrarCliente(usuarioRegistro);

                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(clienteResult.ValidationResult);
                }

                return CustomResponse(await GerarJwt(usuarioRegistro.Email));
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("authenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválias");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha inválidos");
            return CustomResponse();
        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(user, claims);
            string encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(user, claims, encodedToken);
        }

        private UsuarioRespostaLogin ObterRespostaToken(IdentityUser user, IList<Claim> claims, string encodedToken)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                //ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentIssuer =
                $"{_aspNetUser.ObterHttpContext().Request.Scheme}://{_aspNetUser.ObterHttpContext().Request.Host}";
            var key = _jwksService.GenerateSigningCredentials();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                //Issuer = _appSettings.Emissor,
                Issuer = currentIssuer,
                //Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                //Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                Expires = DateTime.UtcNow.AddHours(1),
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                SigningCredentials = key
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(IdentityUser user, IList<Claim> claims)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity(claims);
            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistro usuarioRegistro)
        {
            var usuario = await _userManager.FindByEmailAsync(usuarioRegistro.Email);

            var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(Guid.Parse(usuario.Id), usuarioRegistro.Nome, usuarioRegistro.Email, usuarioRegistro.Cpf);

            try
            {
                return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);
            }
            catch(Exception)
            {
                await _userManager.DeleteAsync(usuario);
                throw;
            }
        }
    }
}
