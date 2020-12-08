using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class RespostaItensViewModel
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataConclusao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Status { get; set; }
        //public string UsuarioId { get; set; }
    }
}
