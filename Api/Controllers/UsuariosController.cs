using Api.Services;
using Api.ViewModel;
using Domain.Context.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controller
{
    [Route("Usuarios")]
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController: ControllerBase
    {
        public readonly IUsuariosService _usuariosService;
        public readonly IUsuariosRepository _usuariosRepository;
        public UsuariosController(IUsuariosService usuariosService,
            IUsuariosRepository usuariosRepository)
        {
            _usuariosService = usuariosService;
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            await CriarUsuarioPadrao();
            return Ok();
        }

        [HttpPost]
        [Route("CriarCliente")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarCliente([FromBody] UsuariosViewModel model)
        {
            await _usuariosService.CriarUsuario(model, "Cliente");

            string stringRetorno = $"Usuário criado com sucesso!";

            return Ok(stringRetorno);
        }

        [HttpGet]
        [Route("ListUsuarios")]
        [AllowAnonymous]
        public async Task<IActionResult> ListUsuarios()
        {
            return Ok(await _usuariosRepository.ObterTodos());
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            // Conferir email e senha 
            var usuario = await _usuariosService.VerificarConta(model);

            return Ok(usuario);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CriarUsuarioPadrao()
        {
            var model = new UsuariosViewModel
            {
                Nome = "Admin",
                Email = "adiministrador@ubi.com",
                Senha = "@DesafioUbi123"
            };
            var usuario = await _usuariosService.CriarUsuario(model, "Adiministrador");

            return Ok(usuario);
        }
    }
}
