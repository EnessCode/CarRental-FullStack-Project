using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.CarFeatureCommands;
using CarRental.Application.Features.Mediator.Queries.CarFeatureQueries;
using CarRental.Application.Features.Mediator.Results.CarFeatureResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class CarFeaturesController(IMediator mediator) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCarFeaturesByCarId(int id)
		{
			var values = await mediator.Send(new GetCarFeatureByCarIdQuery(id));
			return Ok(ApiResponse<List<GetCarFeatureByCarIdQueryResult>>.SuccessResponse(values,"Araca ait özellikler başarıyla getirildi"));
		}

		[HttpPost("UpdateCarFeatureAvailable")]
		public async Task<IActionResult> UpdateCarFeatureAvailable([FromBody] UpdateCarFeatureAvailableCommand command)
		{
			var result = await mediator.Send(command);
			return Ok(ApiResponse<UpdateCarFeatureAvailableCommand>.SuccessResponse(result, "Araç özelliği durumu başarıyla güncellendi"));
		}
	}
}