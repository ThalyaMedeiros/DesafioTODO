using System.Threading.Tasks;
using Api.Services;
using Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItensService _itensService;
        public ItemController(IItensService itensService)
        {
            _itensService = itensService;
        }

        [HttpGet]
        [Route("GetItens")]
        public async Task<IActionResult> Get()
        {        

            return Ok("Get");
        }

        [HttpPost]
        public async Task<IActionResult> CriarItem([FromBody] ItensViewModel model)
        {           
            //salvar no banco
            await _itensService.CriarItem(model);

            return Ok();
        }
        
    }
}
