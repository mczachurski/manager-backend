using Microsoft.AspNet.Mvc;

namespace SunLine.Manager.WebApi
{
	public class BaseController : Controller
	{
		public ApplicationContext ApplicationContext { get; set; }	
	}
}