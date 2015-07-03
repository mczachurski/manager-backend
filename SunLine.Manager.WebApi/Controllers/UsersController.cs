using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.DataTransferObject;
using SunLine.Manager.WebApi.HttpResult;

namespace SunLine.Manager.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;
        
        public UsersController(IUnitOfWork unitOfWork, IUserService userService, ITeamService teamService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _teamService = teamService;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.FindById(id);
            if(user == null)
            {
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.Users);
            }
            
            var userDto = new UserDto(user);
            return new JsonResult(userDto);
        }

        [Route("{id}/Team")]
        [HttpGet]
        public IActionResult GetUserTeam(int id)
        {
            var user = _userService.FindById(id);
            if(user == null)
            {
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.Users);
            }
            
            var team = _teamService.FindById(user.TeamId);
            if(team == null)
            {
                return this.HttpNotFound($"Team for user ({id}) not exists", DocumentationLinks.Users);
            }
            
            var teamDto = new TeamDto(team);
            return new JsonResult(teamDto);
        }
        
        [HttpPost]
        public IActionResult CreateUser([FromBody]UserDto userDto)
        {            
            if(userDto == null)
            {
                return this.HttpBadRequest("User data not specified", DocumentationLinks.Users);
            }
            
            if (!ModelState.IsValid)
            {
                return this.HttpBadModelState("Error in user data model", DocumentationLinks.Users);
            }
            
            var userAlreadyExists = _userService.FindByEmail(userDto.Email);
            if(userAlreadyExists != null)
            {
                return this.HttpForbidden("User with this email already exists", DocumentationLinks.Users);   
            }
            
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email  
            };
             
            _userService.Create(user);
            _unitOfWork.Commit();
            
            return new JsonResult(ResultDto.CreateSuccess());
        }
        
        [Route("{id}/Team")]
        [HttpPost]
        public IActionResult CreateTeam(int id, [FromBody]TeamDto teamDto)
        {
            if(teamDto == null)
            {
                return this.HttpBadRequest("Team data not specified", DocumentationLinks.Users);
            }
            
            var user = _userService.FindById(id);
            if(user == null)
            {
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.Users);
            }
            
            if (user.TeamId > 0)
            {
                return this.HttpForbidden("User already have a team", DocumentationLinks.Users);
            }
            
            if (!ModelState.IsValid)
            {
                return this.HttpBadModelState("Error in team data model", DocumentationLinks.Users);
            }
            
            var team = new Team
            {
                Name = teamDto.Name,
                User = user,
                UserId = user.Id
            };
                        
            _teamService.Create(team);
            _unitOfWork.Commit();
            
            return new JsonResult(ResultDto.CreateSuccess());
        }
    }
}
