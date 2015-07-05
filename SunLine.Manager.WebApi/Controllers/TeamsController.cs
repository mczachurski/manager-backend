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
        
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
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
    }
}
