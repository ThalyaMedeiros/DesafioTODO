using Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> ObterTodos();
        Task CriarUsuario(Usuarios usuarios);
    }
}
