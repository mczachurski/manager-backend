using Microsoft.AspNet.Mvc;
using SunLine.Manager.Services.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.HttpResult;
using SunLine.Manager.DataTransferObjects.Response;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.Controllers
{
    [ServiceFilter(typeof(CheckClientKeyAttribute))]
    [Route("api/[controller]")]
    public class StadiumsController : Controller
    {
        private readonly IStadiumService _stadiumService;
        
        public StadiumsController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }
        
        [ServiceFilter(typeof(CheckAccessTokenAttribute))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var stadium = _stadiumService.FindById(id);
            if(stadium == null)
            {
                return this.HttpNotFound($"Stadium ({id}) not exists", DocumentationLinks.Stadiums);
            }
            
            var stadiumDto = new StadiumDto(stadium);
            return new JsonResult(stadiumDto);
        }
    }
}
