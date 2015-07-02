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
        private readonly IUnitOfWork _unitOfWork;
        
        public UsersController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var user = _userService.FindById(id);
            return user != null ? user.ToString() : "Nie znaleziono użytkownika";
        }
        
        [HttpPost]
        public void Post([FromBody]User user)
        {
            user.CreationDate = DateTime.Now;
            user.Version = 1;
            
            _userService.Create(user);
            _unitOfWork.Commit();
        }
    }
}
