using Api.Services;
using Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controller
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController: ControllerBase
    {
        public readonly IUsuariosService _usuariosService;
        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuariosViewModel model)
        {
            //salvar no banco
            var usuario = await _usuariosService.CriarUsuario(model);

            return Ok(usuario);
        }
    }
}
