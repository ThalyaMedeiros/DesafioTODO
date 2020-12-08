using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class PaginacaoViewModel
    {
        public int PaginalAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
        public object Itens { get; set; }
    }
}
