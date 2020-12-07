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
    public class UsuariosService : IUsuariosService
    {
        public readonly IUsuariosRepository _usuariosRepository;
        public IMapper _mapper;
        public UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _mapper = mapper;
        }
        public async Task<Usuarios> CriarUsuario(UsuariosViewModel usuariosViewModel)
        {
            //Fazer mapeamento
            var usuarios = _mapper.Map<Usuarios>(usuariosViewModel);

            //Fazer validações

            //persistir no banco
            await _usuariosRepository.CriarUsuario(usuarios);

            return usuarios;
        }
    }
}
