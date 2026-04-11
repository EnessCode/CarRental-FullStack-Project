using CarRental.Dto;
using CarRental.Dto.FooterAddressesDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class FooterAddressController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("FooterAddresses");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultFooterAddressesDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultFooterAddressesDto>());
		}

		[HttpGet]
		public async Task<IActionResult> CreateFooterAddress()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateFooterAddress(CreateFooterAddressesDto CreateFooterAddressesDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(CreateFooterAddressesDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("FooterAddresses", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "FooterAddress", new { area = "Admin" });
			}
			return View(CreateFooterAddressesDto);
		}
		public async Task<IActionResult> DeleteFooterAddress(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("FooterAddresses/" + id);

			return RedirectToAction("Index", "FooterAddress", new { area = "Admin" });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateFooterAddress(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("FooterAddresses/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<UpdateFooterAddressesDto>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return RedirectToAction("Index", "FooterAddress", new { area = "Admin" });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddressesDto UpdateFooterAddressesDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(UpdateFooterAddressesDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("FooterAddresses", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "FooterAddress", new { area = "Admin" });
			}
			return View(UpdateFooterAddressesDto);
		}
	}
}
