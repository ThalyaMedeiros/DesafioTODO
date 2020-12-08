using Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFuncoesRepository
    {
        Task<IEnumerable<Funcoes>> ObterTodos();
        Task CriarFuncao(Funcoes funcoes);
        Task<Funcoes> ObterPorNome(string nome);
        Task<string> ObterNomePorId(string id);
    }
}
