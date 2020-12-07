using Api.ViewModel;
using AutoMapper;
using Domain.Context.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ItensService : IItensService
    {
        public readonly IItensRepository _itensRepository;
        public IMapper _mapper;
        public ItensService(IItensRepository itensRepository, IMapper mapper)
        {
            _itensRepository = itensRepository;
            _mapper = mapper;
        }
        public async Task<Itens> CriarItem(ItensViewModel itensViewModel)
        {
            //Fazer mapeamento
            var itens = _mapper.Map<Itens>(itensViewModel);

            //Fazer validações
            itens.ValidarDescricao();

            //persistir no banco
            await _itensRepository.CriarItem(itens);

            return itens;
        }
    }
}
