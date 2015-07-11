using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Football;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.DataTransferObjects.Response;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.Controllers
{
    [ServiceFilter(typeof(CheckClientKeyAttribute))]
    [Route("api/[controller]")]
    public class PlayersController : BaseController
    {
        private readonly IPlayerService _playerService;
        
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var player = _playerService.FindById(id);
            if(player == null)
            {
                return this.HttpNotFound($"Player ({id}) not exists", DocumentationLinks.Players);
            }
            
            var playerDto = new PlayerDto(player);
            return new JsonResult(playerDto);
        }
    }
}
