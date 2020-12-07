using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Context.Entities
{
    public class Usuarios: Entity
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

        //public void ValidarUsuario(string nome, string email, string senha)
        //{
        //    //Validar nome
        //    if (string.IsNullOrEmpty(nome)) new Exception("Nome do Usuário não pode ser nulo");

        //    //Validar email
        //    var regexEmail = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        //    if(!regexEmail.IsMatch(email)) new Exception("Email inválido");
            
        //    //Validar senha
        //}

    }
}
