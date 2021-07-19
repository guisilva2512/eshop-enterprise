using eShopEnterprise.Clientes.API.Application.Commands;
using eShopEnterprise.Core.Mediator;
using eShopEnterprise.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eShopEnterprise.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("")]
        public async Task<IActionResult> Teste()
        {
            var resultado = await _mediatorHandler.EnviarCommand(new RegistrarClienteCommand(Guid.NewGuid(), "Guilherme", "teste@teste.com.br", "91818697033"));

            return CustomResponse(resultado);
        }
    }
}
