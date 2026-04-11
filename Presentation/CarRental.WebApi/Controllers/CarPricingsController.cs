using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Queries.CarPricingQueries;
using CarRental.Application.Features.Mediator.Results.CarPricingResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class CarPricingsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetCarPricingWithCarsList()
		{
			var values = await mediator.Send(new GetCarPricingWithCarsQuery());
			return Ok(ApiResponse<List<GetCarPricingWithCarsQueryResult>>.SuccessResponse(values, "Araç fiyat listesi başarıyla getirildi"));
		}

		[HttpGet("GetCarPricingWithTimePeriodList")]
		public async Task<IActionResult> GetCarPricingWithTimePeriodList()
		{
			var values = await mediator.Send(new GetCarPricingWithTimePeriodQuery());
			return Ok(ApiResponse<List<GetCarPricingWithTimePeriodQueryResult>>.SuccessResponse(values, "Süreli fiyat listesi başarıyla getirildi"));
		}
	}
}
