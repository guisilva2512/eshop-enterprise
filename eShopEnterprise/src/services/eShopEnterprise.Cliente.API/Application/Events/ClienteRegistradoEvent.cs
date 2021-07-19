using eShopEnterprise.Core.Messages;
using System;

namespace eShopEnterprise.Clientes.API.Application.Events
{
    public class ClienteRegistradoEvent : Event
    {
        public ClienteRegistradoEvent(Guid id, string nome, string email, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
    }
}
