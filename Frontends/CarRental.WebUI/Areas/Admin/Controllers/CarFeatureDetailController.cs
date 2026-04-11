using CarRental.Dto;
using CarRental.Dto.CarFeatureDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CarFeatureDetailController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("CarFeatures/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultCarFeatureByCarIdDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultCarFeatureByCarIdDto>());
		}

		[HttpPost]
		public async Task<IActionResult> ChangeAvailableStatus(int carId, int featureId, bool status)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");

			var updateCommand = new
			{
				CarId = carId,
				FeatureId = featureId,
				Available = status
			};

			var jsonData = JsonConvert.SerializeObject(updateCommand);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PostAsync("CarFeatures/UpdateCarFeatureAvailable", stringContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				return Json(new { success = true });
			}

			return BadRequest();
		}
	}
}
