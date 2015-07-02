using System;
using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;
        
        public TeamsController(IUnitOfWork unitOfWork, IUserService userService, ITeamService teamService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _teamService = teamService;
        }
        
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            var team = _teamService.FindById(id);
            return team;
        }
    }
}
