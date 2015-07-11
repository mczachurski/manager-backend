using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Football;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.DataTransferObjects.Response;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.Controllers
{
    [ServiceFilter(typeof(CheckClientKeyAttribute))]
    [Route("api/[controller]")]
    public class TeamsController : BaseController
    {
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;
        
        public TeamsController(ITeamService teamService, IPlayerService playerService)
        {
            _teamService = teamService;
            _playerService = playerService;
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = _teamService.FindById(id);
            if(team == null)
            {
                return this.HttpNotFound($"Team ({id}) not exists", DocumentationLinks.Teams);
            }
            
            var teamDto = new TeamDto(team);
            return new JsonResult(teamDto);
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [Route("{id}/Players")]
        [HttpGet]
        public IActionResult GetPlayers(int id)
        {   
            var team = _teamService.FindById(id);
            if(team == null)
            {
                return this.HttpNotFound($"Team ({id}) not exists", DocumentationLinks.Teams);
            }
            
            var players = _playerService.FindAllPlayersForTeam(id);
            
            IList<PlayerDto> playersDtos = new List<PlayerDto>();
            foreach(var player in players)
            {
                var playerDto = new PlayerDto(player);
                playersDtos.Add(playerDto);
            }
                        
            return new JsonResult(playersDtos);
        }
    }
}
