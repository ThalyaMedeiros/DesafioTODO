using Api.ViewModel;
using Domain.Context.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IItensService
    {
        Task<RespostaItensViewModel> CriarItem(ItensViewModel itensViewModel, string usuarioId);

        Task<RespostaItensViewModel> ConcluirItem(string id);

        Task<RespostaItensViewModel> EditarItem(ItensViewModel itensViewModel);

        Task<List<RespostaUsuarioViewModel>> ListarItens(string usuarioId);

        Task<List<RespostaUsuarioViewModel>> ListarItensAtrasados(string usuarioId);
    }
}