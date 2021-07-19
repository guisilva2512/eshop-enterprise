using eShopEnterprise.Catalogo.API.Models;
using eShopEnterprise.WebApi.Core.Controllers;
using eShopEnterprise.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopEnterprise.Catalogo.API.Controllers
{
    [Authorize]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("catalogo/produtos")]
        [AllowAnonymous]
        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        [HttpGet("catalogo/produtos/{id}")]
        [ClaimsAuthorize("Catalogo", "Ler")]
        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }
    }
}
