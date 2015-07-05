using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.AspNet.Mvc.ModelBinding;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.Football;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.DataTransferObjects.Response;
using SunLine.Manager.DataTransferObjects.Request;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.Controllers
{
    [ServiceFilter(typeof(CheckClientKeyAttribute))]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IUserSessionService _userSessionService;
        private readonly IStadiumService _stadiumService;
        private readonly IModelValidator _modelValidator;
        
        public UsersController(
            IUnitOfWork unitOfWork, 
            IUserService userService, 
            ITeamService teamService, 
            IUserSessionService userSessionService,
            IStadiumService stadiumService
        )
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _teamService = teamService;
            _userSessionService = userSessionService;
            _stadiumService = stadiumService;
        }
        
        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn([FromBody]SignInDto signInDto)
        {
            if(signInDto == null)
            {
                return this.HttpBadRequest("Sign in data not specified", DocumentationLinks.SignIn);
            }
            
            if(!ModelState.IsValid)
            {
                return this.HttpBadModelState("Error in sign in data model", DocumentationLinks.Users);
            }
            
            User user = _userService.FindByCredentials(signInDto.Email, signInDto.Password);
            if(user == null)
            {
                return new JsonResult(ErrorDto.Create("Email or password are incorrect", DocumentationLinks.SignIn));
            }
            
            UserSession userSession = _userSessionService.CreateUserSession(user, "http://localhost/");
            _unitOfWork.Commit();
            
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
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.Teams);
            }
            
            var team = _teamService.FindById(user.TeamId);
            if(team == null)
            {
                return this.HttpNotFound($"Team for user ({id}) not exists", DocumentationLinks.Teams);
            }
            
            var teamDto = new TeamDto(team);
            return new JsonResult(teamDto);
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [Route("{id}/Stadium")]
        [HttpGet]
        public IActionResult GetUserStadium(int id)
        {   
            var user = _userService.FindById(id);
            if(user == null)
            {
                return this.HttpNotFound($"User ({id}) not exists", DocumentationLinks.Stadiums);
            }
            
            var team = _teamService.FindById(user.TeamId);
            if(team == null)
            {
                return this.HttpNotFound($"Team for user ({id}) not exists", DocumentationLinks.Stadiums);
            }
            
            var stadium = _stadiumService.FindById(team.StadiumId);
            if(stadium == null)
            {
                return this.HttpNotFound($"Stadium for user ({id}) not exists", DocumentationLinks.Stadiums);
            }
            
            var stadiumDto = new StadiumDto(stadium);
            return new JsonResult(stadiumDto);
        }
        
        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateUserDto createUserDto)
        {            
            if(createUserDto == null)
            {
                return this.HttpBadRequest("User data not specified", DocumentationLinks.Users);
            }

            if(!ModelState.IsValid)
            {
                return this.HttpBadModelState("Error in user data model", DocumentationLinks.Users);
            }
            
            var userAlreadyExists = _userService.FindByEmail(createUserDto.Email);
            if(userAlreadyExists != null)
            {
                return this.HttpForbidden("User with this email already exists", DocumentationLinks.Users);   
            }
            
            _userService.Create(createUserDto);
            _unitOfWork.Commit();
            
            return new JsonResult(SuccessDto.Create());
        }
    }
}
