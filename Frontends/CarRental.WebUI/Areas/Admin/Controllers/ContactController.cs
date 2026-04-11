using CarRental.Dto;
using CarRental.Dto.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ContactController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Contacts");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultContactDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultContactDto>());
		}

		[HttpPost] 
		public async Task<IActionResult> MarkAsRead(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Contacts/MarkContactAsRead/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				return Ok(); 
			}
			return BadRequest();
		}

		public async Task<IActionResult> DeleteContact(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Contacts/" + id);

			return RedirectToAction("Index", "Contact", new { area = "Admin" });
		}
	}
}
