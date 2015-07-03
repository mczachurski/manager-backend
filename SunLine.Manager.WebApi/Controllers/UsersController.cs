using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.DataTransferObject;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.Controllers
{
    [ServiceFilter(typeof(CheckClientKeyAttribute))]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IUserSessionService _userSessionService;
        
        public UsersController(IUnitOfWork unitOfWork, IUserService userService, ITeamService teamService, IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _teamService = teamService;
            _userSessionService = userSessionService;
        }
        
        [HttpPost]
        public IActionResult SignIn(SignInDto signInDto)
        {
            if(signInDto == null)
            {
                return this.HttpBadRequest("Sign in data not specified", DocumentationLinks.SignIn);
            }
            
            if (!ModelState.IsValid)
            {
                return this.HttpBadModelState("Error in sign in data model", DocumentationLinks.SignIn);
            }
            
            User user = _userService.FindByCredentials(signInDto.Email, signInDto.Password);
            if(user == null)
            {
                return new JsonResult(ResultDto.CreateError("Email or password are incorrect", DocumentationLinks.SignIn));
            }
            
            UserSession userSession = _userSessionService.CreateUserSession(user, "http://localhost/");
            
            var accessTokenDto = new AccessTokenDto
            {
                AccessToken = userSession.AccessToken.ToString(),
                IsSuccess = true
            };
            
            return new JsonResult(accessTokenDto);
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.FindById(id);
            if(user == null)
            {
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.SignIn);
            }
            
            var userDto = new UserDto(user);
            return new JsonResult(userDto);
        }

        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
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
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
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
             
            _userService.Create(user, userDto.Password);
            _unitOfWork.Commit();
            
            return new JsonResult(ResultDto.CreateSuccess());
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
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
