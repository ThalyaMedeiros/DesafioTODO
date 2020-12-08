using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Context.Entities
{
    public class Usuarios : Entity
    {

        public Usuarios(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string FuncaoId { get; private set; }

        public void SenhaCriptografada(string senha)
        {
            this.Senha = senha;
        }
        public void DefinirFuncao(string funcaoId)
        {
            this.FuncaoId = funcaoId;
        }
    }
}
