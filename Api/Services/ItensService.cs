using Api.ViewModel;
using AutoMapper;
using Domain.Context.Entities;
using Domain.Context.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ItensService : IItensService
    {
        public readonly IItensRepository _itensRepository;
        public IMapper _mapper;
        private IFuncoesRepository _funcoesRepository;
        private IUsuariosRepository _usuariosRepository;

        public ItensService(IItensRepository itensRepository, IMapper mapper,
            IFuncoesRepository funcoesRepository,
            IUsuariosRepository usuariosRepository)
        {
            _itensRepository = itensRepository;
            _mapper = mapper;

            _funcoesRepository = funcoesRepository;
            _usuariosRepository = usuariosRepository;
        }
        public async Task<RespostaItensViewModel> CriarItem(ItensViewModel itensViewModel, string usuarioId)
        {
            //Fazer mapeamento
            var itens = _mapper.Map<Itens>(itensViewModel);
            
            //Fazer validações
            itens.CriarItem(usuarioId, itensViewModel.DataVencimento);

            //persistir no banco
            await _itensRepository.CriarItem(itens);

            //Fazer mapeamento de retorno
            var respostaItens = _mapper.Map<RespostaItensViewModel>(itens);

            return respostaItens;
        }

        public async Task<RespostaItensViewModel> ConcluirItem(string id)
        {
            //Fazer validações
            var item = await _itensRepository.ObterPorId(id);

            if(item == null) { throw new Exception("Item não encontrado."); }

            item.ConcluirItem();

            //persistir no banco
            await _itensRepository.AtualizarItem(item);

            //Fazer mapeamento de retorno
            var respostaItens = _mapper.Map<RespostaItensViewModel>(item);

            return respostaItens;
        }

        public async Task<RespostaItensViewModel> EditarItem(ItensViewModel itensViewModel)
        {
            //Fazer validações
            var item = await _itensRepository.ObterPorId(itensViewModel.Id);

            if (item == null) { throw new Exception("Item não encontrado."); }

            item.EditarItem(itensViewModel.Descricao, itensViewModel.DataVencimento);

            //persistir no banco
            await _itensRepository.AtualizarItem(item);

            //Fazer mapeamento de retorno
            var respostaItens = _mapper.Map<RespostaItensViewModel>(item);

            return respostaItens;
        }

        public async Task<List<RespostaUsuarioViewModel>> ListarItens(string usuarioId)
        {
            //Verificar a função do usuario
            var funcaoId = await _usuariosRepository.ObterFuncaoIdPorUsuarioId(usuarioId);           
            var funcao = await _funcoesRepository.ObterNomePorId(funcaoId);
            if (funcao == null) { throw new Exception("Não é possivel verificar a lista de itens."); }


            List<Itens> itens = new List<Itens>();
            List<RespostaUsuarioViewModel> respostaUsuarioViewModels = new List<RespostaUsuarioViewModel>();

            //Verificar Lista
            if (funcao == EFuncaoUsuarios.Cliente.ToString())
            {
                itens = await _itensRepository.ObterTodosPorUsuarioId(usuarioId);
                itens = AtualizarItensAtrasados(itens);

                var usuario = await _usuariosRepository.ObterPorId(usuarioId);
                respostaUsuarioViewModels.Add(
                    new RespostaUsuarioViewModel
                    {
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Itens = _mapper.Map<List<RespostaItensViewModel>>(itens)
                    });
            }
            else
            {
                var usuarios = await _usuariosRepository.ObterTodos();
                foreach (var item in usuarios)
                {
                    itens = await _itensRepository.ObterTodosPorUsuarioId(item.Id);
                    itens = AtualizarItensAtrasados(itens);

                    respostaUsuarioViewModels.Add(
                    new RespostaUsuarioViewModel
                    {
                        Nome = item.Nome,
                        Email = item.Email,
                        Itens = _mapper.Map<List<RespostaItensViewModel>>(itens)
                    });
                }
            }

            return respostaUsuarioViewModels;
        }

        public async Task<List<RespostaUsuarioViewModel>> ListarItensAtrasados(string usuarioId)
        {
            //Verificar função
            var funcaoId =await _usuariosRepository.ObterFuncaoIdPorUsuarioId(usuarioId);
            var funcao = await _funcoesRepository.ObterNomePorId(funcaoId);
            if (funcao == EFuncaoUsuarios.Cliente.ToString()) { throw new Exception("Você não tem permissão para isso."); }

            List<RespostaUsuarioViewModel> respostaUsuarioViewModels = new List<RespostaUsuarioViewModel>();

            //Verificar Lista
            var usuarios = await _usuariosRepository.ObterTodos();
            foreach (var item in usuarios)
            {
                var itens = await _itensRepository.ObterTodosItensAtrasadosPorUsuarioId(item.Id);
                respostaUsuarioViewModels.Add(
                new RespostaUsuarioViewModel
                {
                    Nome = item.Nome,
                    Email = item.Email,
                    Itens = _mapper.Map<List<RespostaItensViewModel>>(itens)
                });
            }

            return respostaUsuarioViewModels;
        }

        public List<Itens> AtualizarItensAtrasados(List<Itens> itens)
        {
            foreach (var item in itens)
            {
                item.VerificarListaDataVencimento(item);
            }
            return itens;
        }

    }
}
