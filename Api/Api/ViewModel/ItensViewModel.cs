using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class ItensViewModel
    {
        public string Descricao { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime DataVencimento { get; private set; }

        public DateTime DataConclusao { get; private set; }

        public DateTime DataAtualizacao { get; private set; }

        //duvida entre string e Enum
        //public EStatusItens Status { get; private set; }

        public string ApplicationUserId { get; private set; }
    }
}
