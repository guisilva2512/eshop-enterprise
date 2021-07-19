﻿using eShopEnterprise.Core.DomainObjects;
using System;

namespace eShopEnterprise.Clientes.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        // EF Relational
        protected Cliente() { }

        public Cliente(Guid id, string nome, string email, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluido = false;
        }

        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }   

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirEndereço(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}
