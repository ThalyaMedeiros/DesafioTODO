using Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IItensRepository
    {
        Task<List<Itens>> ObterTodos();
        Task<List<Itens>> ObterTodosItensAtrasados();
        Task<List<Itens>> ObterTodosPorUsuarioId(string usuarioId);
        Task<List<Itens>> ObterTodosItensAtrasadosPorUsuarioId(string usuarioId);
        Task<Itens> ObterPorId(string id);
        Task CriarItem(Itens item);
        Task AtualizarItem(Itens item);
        Task ExcluirItem(Itens item);
    }
}
