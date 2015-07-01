using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Core;

namespace SunLine.Manager.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var user = _userService.FindById(id);
            return user != null ? user.ToString() : "Nie znaleziono użytkownika";
        }
    }
}
