using Api.ViewModel;
using AutoMapper;
using Domain.Context.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
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
        public readonly IFuncoesRepository _funcoesRepository;
        public readonly ITokenServices _tokenServices;
        public readonly ICriptografia _criptografia;
        public UsuariosService(IUsuariosRepository usuariosRepository,
         IMapper mapper,
         IFuncoesRepository funcoesRepository,
         ITokenServices tokenServices,
         ICriptografia criptografia)
        {
            _usuariosRepository = usuariosRepository;
            _mapper = mapper;
            _funcoesRepository = funcoesRepository;
            _tokenServices = tokenServices;
            _criptografia = criptografia;
        }
        public async Task<Usuarios> CriarUsuario(UsuariosViewModel usuariosViewModel, string funcao)
        {
            //Verificar se o usuário ja existe 
            if(await _usuariosRepository.EmailExistente(usuariosViewModel.Email))
            {
                throw new Exception("Usuario com este email já existe.");
            }            
            
            //Vefificar se a função já existe 
            var funcaoUsuario =  await _funcoesRepository.ObterPorNome(funcao);
            if(funcaoUsuario == null)
            {
                await _funcoesRepository.CriarFuncao(new Funcoes(funcao));

                funcaoUsuario = await _funcoesRepository.ObterPorNome(funcao);
            }

            //Fazer mapeamento
            var usuarios = _mapper.Map<Usuarios>(usuariosViewModel);
            usuarios.DefinirFuncao(funcaoUsuario.Id);

            //Fazer validações
            var senhaCriptografada = _criptografia.Codifica(usuarios.Senha);
            usuarios.SenhaCriptografada(senhaCriptografada);

            //persistir no banco
             await _usuariosRepository.CriarUsuario(usuarios);

            return usuarios;
        }

        public async Task<TokenReturnViewModel> VerificarConta(LoginViewModel model)
        {
            var usuario = await _usuariosRepository.ObterPorEmail(model.Email);            
            if(usuario == null) { throw new Exception("Usuario Inválido."); }
            
            var validarSenha = _criptografia.Compara(model.Senha, usuario.Senha);
            if (validarSenha)
            {
                //Gerar token Autenticação
                var tokenString = _tokenServices.CriarToken(usuario.Id, usuario.Email);

                return new TokenReturnViewModel { Token = tokenString, Email = model.Email };

            }
            else
            {
                throw new Exception("Senha Inválida");
            }
        }
    }
}
