using CryptSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class Criptografia : ICriptografia
    {
        public string Codifica(string senha)
        {
            return Crypter.MD5.Crypt(senha);
        }

        public bool Compara(string senha, string hash)
        {
            return Crypter.CheckPassword(senha, hash);
        }
    }
}
