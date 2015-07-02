using System;
using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

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
        public User Get(int id)
        {
            var user = _userService.FindById(id);
            return user;
        }
        
        [HttpPost]
        public void CreateUser([FromBody]User user)
        {            
            _userService.Create(user);
            _unitOfWork.Commit();
        }
        
        [Route("{id}/Team")]
        [HttpPost]
        public void CreateTeam(int id, [FromBody]Team team)
        {
            var user = _userService.FindById(id);
            team.User = user;
                        
            _teamService.Create(team);
            _unitOfWork.Commit();
        }
    }
}
