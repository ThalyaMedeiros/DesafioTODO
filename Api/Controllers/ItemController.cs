using System.Collections;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Services;
using Api.ViewModel;
using AspNetCore.PaginatedList;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Api.Controller
{
    [Route("Item")]
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private IItensService _itensService;
        private IItensRepository _itensRepository;
       
        public ItemController(IItensService itensService, 
            IItensRepository itensRepository)
        {
            _itensService = itensService;
            _itensRepository = itensRepository;
        }

        [HttpGet]
        [Route("ListarItens")]
        public async Task<IActionResult> ListarItens(int paginaAtual = 1, int TotalPaginas = 1)
        {
            //Verificar usuario 
            var usuarioId = HttpContext.User.FindFirst("Id").Value;

            var itens = await _itensService.ListarItens(usuarioId);
            
            var ItensPaginados = new PaginatedList<RespostaUsuarioViewModel>(itens, itens.Count, paginaAtual, TotalPaginas);
            
            var lista = ItensPaginados.ToPagedList(paginaAtual, TotalPaginas);
            
            return Ok(new PaginacaoViewModel {
                PaginalAtual = lista.PageNumber, 
                TotalPaginas = lista.PageCount,
                TotalItens = lista.TotalItemCount,
                Itens = lista
            });
        }

        [HttpGet]
        [Route("ListarItensAtrasados")]
        public async Task<IActionResult> ListarItensAtrasados(int paginaAtual = 1, int TotalPaginas = 1)
        {
            var usuarioId = HttpContext.User.FindFirst("Id").Value;
           
            var itens = await _itensService.ListarItensAtrasados(usuarioId);
            var ItensPaginados = new PaginatedList<RespostaUsuarioViewModel>(itens, itens.Count, paginaAtual, TotalPaginas);

            var lista = ItensPaginados.ToPagedList(paginaAtual, TotalPaginas);

            return Ok(new PaginacaoViewModel
            {
                PaginalAtual = lista.PageNumber,
                TotalPaginas = lista.PageCount,
                TotalItens = lista.TotalItemCount,
                Itens = lista
            });
        }

        [HttpPost]
        [Route("CriarItem")]
        public async Task<IActionResult> CriarItem([FromBody] ItensViewModel model)
        {
            //Verificar usuario 
            var usuarioId = HttpContext.User.FindFirst("Id").Value;

            var itens = await _itensService.CriarItem(model, usuarioId);

            return Ok(itens);
        }

        [HttpPatch]
        [Route("ConcluirItem/{Id}")]
        public async Task<IActionResult> ConcluirItem(string Id)
        {          
            //atualizar no banco
            var itens = await _itensService.ConcluirItem(Id);

            return Ok(itens);
        }

        [HttpPut]
        [Route("EditarItem")]
        public async Task<IActionResult> EditarItem([FromBody] ItensViewModel model)
        {
            //Verificar usuario 
            var usuarioId = HttpContext.User.FindFirst("Id").Value;

            //atualizar no banco
            var itens = await _itensService.EditarItem(model);

            return Ok(itens);
        }

        [HttpDelete]
        [Route("ExcluirItem")]
        public async Task<IActionResult> ExcluirItem(string id)
        {
            var item = await _itensRepository.ObterPorId(id);
            if(item == null) { BadRequest("Item não existe."); }
           
            await _itensRepository.ExcluirItem(item);
            return Ok("Item excluido com sucesso.");
        }

    }
}
