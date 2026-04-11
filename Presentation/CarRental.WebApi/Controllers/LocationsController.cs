using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.LocationCommands;
using CarRental.Application.Features.Mediator.Queries.LocationQueries;
using CarRental.Application.Features.Mediator.Results.LocationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class LocationsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> LocationList()
		{
			var values = await mediator.Send(new GetLocationQuery());
			return Ok(ApiResponse<List<GetLocationQueryResult>>.SuccessResponse(values, "Konum bilgileri başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> LocationById(int id)
		{
			var value = await mediator.Send(new GetLocationByIdQuery(id));
			return Ok(ApiResponse<GetLocationByIdQueryResult>.SuccessResponse(value, "İlgili konum bilgisi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateLocation(CreateLocationCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateLocationCommand>.SuccessResponse(createdData, "Yeni konum bilgisi başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveLocation(int id)
		{
			var removedData = await mediator.Send(new RemoveLocationCommand(id));
			return Ok(ApiResponse<RemoveLocationCommand>.SuccessResponse(removedData, "Konum bilgisi başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateLocation(UpdateLocationCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateLocationCommand>.SuccessResponse(updatedData, "Konum bilgisi başarıyla güncellendi"));
		}
	}
}
