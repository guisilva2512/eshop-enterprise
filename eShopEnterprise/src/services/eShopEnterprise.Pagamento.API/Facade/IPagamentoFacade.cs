using eShopEnterprise.Pagamento.API.Models;
using System.Threading.Tasks;

namespace eShopEnterprise.Pagamento.API.Facade
{
    public interface IPagamentoFacade
    {
        Task<Transacao> AutorizarPagamento(Models.Pagamento pagamento);
        Task<Transacao> CapturarPagamento(Transacao transacao);
        Task<Transacao> CancelarAutorizacao(Transacao transacao);
    }
}
