using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eShopEnterprise.Clientes.API.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public async Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Enviar e-mail de notificação

            Console.WriteLine("Será enviado e-mail!");
        }
    }
}
