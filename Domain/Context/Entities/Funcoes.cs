using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context.Entities
{
    public class Funcoes : Entity
    {
        public Funcoes(string nome)
        {
            this.Nome = nome;
            this.DataCadastro = DateTime.Now;
        }

        public string Nome { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}
