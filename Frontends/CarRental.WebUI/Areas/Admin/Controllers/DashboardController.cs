using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
