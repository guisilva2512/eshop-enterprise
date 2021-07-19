using eShopEnterprise.Core.Data;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace eShopEnterprise.Core.Messages
{
    public abstract class CommandHandler
    {
        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult;

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AdicionarErro("Houve um erro ao persistir os dados.");

            return ValidationResult;
        }
    }
}
