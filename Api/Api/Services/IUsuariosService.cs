using Api.ViewModel;
using Domain.Context.Entities;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUsuariosService
    {
        Task<Usuarios> CriarUsuario(UsuariosViewModel usuariosViewModel);
    }
}