using Api.ViewModel;
using Domain.Context.Entities;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IItensService
    {
        Task<Itens> CriarItem(ItensViewModel itensViewModel);
    }
}