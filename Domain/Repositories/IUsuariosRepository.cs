using Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> ObterTodos();
        Task CriarUsuario(Usuarios usuarios);
        Task<Usuarios> LoginUsuario(string email, string senha);
        Task<string> ObterFuncaoIdPorUsuarioId(string usuarioId);
        Task<Usuarios> ObterPorId(string usuarioId);
        Task<Usuarios> ObterPorEmail(string email);
        Task<bool> EmailExistente(string email);
    }
}
