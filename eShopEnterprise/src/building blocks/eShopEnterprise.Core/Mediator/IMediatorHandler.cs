using eShopEnterprise.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace eShopEnterprise.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarCommand<T>(T comando) where T : Command;
    }
}
