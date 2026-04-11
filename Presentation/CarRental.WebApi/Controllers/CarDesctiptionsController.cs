using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Queries.CarDescriptionQueries;
using CarRental.Application.Features.Mediator.Results.CarDescriptionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class CarDescriptionsController(IMediator mediator) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCarDescriptionByCarId(int id)
		{
			var value = await mediator.Send(new GetCarDescriptionByCarIdQuery(id));
			return Ok(ApiResponse<GetCarDescriptionQueryResult>.SuccessResponse(value, "Araç açıklaması başarıyla getirildi."));
		}
	}
}