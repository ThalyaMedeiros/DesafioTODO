using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class RespostaUsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<RespostaItensViewModel> Itens { get; set; }


    }
}
