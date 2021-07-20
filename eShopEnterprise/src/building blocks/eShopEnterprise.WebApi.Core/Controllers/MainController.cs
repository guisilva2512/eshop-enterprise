using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace eShopEnterprise.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                AdicionarErrorProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {

            foreach (var erro in validationResult.Errors)
            {
                AdicionarErrorProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected void AdicionarErrorProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErros()
        {
            Erros.Clear();
        }

        protected bool OperacaoValida() => !Erros.Any();
    }

}
