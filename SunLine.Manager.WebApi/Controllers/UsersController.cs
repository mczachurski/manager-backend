using System.Linq;
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
        public ActionResult GetUser(int id)
        {
            var user = _userService.FindById(id);
            if(user == null)
            {
                return HttpNotFound(HttpErrorMessageDto.Create($"User ({id}) not exists"));
            }
            
            return new JsonResult(user);
        }

        [Route("{id}/Team")]
        [HttpGet]
        public ActionResult GetUserTeam(int id)
        {
            var user = _userService.FindById(id);
            if(user == null)
            {
                return HttpNotFound(HttpErrorMessageDto.Create($"User ({id}) not exists"));
            }
            
            var team = _teamService.FindById(user.TeamId);
            if(user == null)
            {
                return HttpNotFound(HttpErrorMessageDto.Create($"Team for user ({id}) not exists"));
            }
            
            return new JsonResult(new { Name = team.Name, UserId = team.UserId });
        }
        
        [HttpPost]
        public ActionResult CreateUser([FromBody]UserDto userDto)
        {            
            if(userDto == null)
            {
                return HttpBadRequest(HttpErrorMessageDto.Create($"User data not specified"));
            }
            
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);;
                return this.HttpBadRequest(allErrors);
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
        public ActionResult CreateTeam(int id, [FromBody]TeamDto teamDto)
        {
            if(teamDto == null)
            {
                return HttpBadRequest(HttpErrorMessageDto.Create($"Team data not specified"));
            }
            
            var user = _userService.FindById(id);
            if(user == null)
            {
                return HttpNotFound(HttpErrorMessageDto.Create($"User ({id}) not exists"));
            }
            
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);;
                return this.HttpBadRequest(allErrors);
            }
            
            if (user.TeamId > 0)
            {
                return this.HttpForbidden(HttpErrorMessageDto.Create($"User already have a team"));
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
