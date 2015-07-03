using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.WebApi.DataTransferObject;

namespace SunLine.Manager.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;
        
        public TeamsController(IUnitOfWork unitOfWork, ITeamService teamService)
        {
            _unitOfWork = unitOfWork;
            _teamService = teamService;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = _teamService.FindById(id);
            if(team == null)
            {
                return this.HttpNotFound($"Team ({id}) not exists", DocumentationLinks.Users);
            }
            
            var teamDto = new TeamDto(team);
            return new JsonResult(teamDto);
        }
    }
}
