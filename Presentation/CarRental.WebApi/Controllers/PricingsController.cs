using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.PricingCommands;
using CarRental.Application.Features.Mediator.Queries.PricingQueries;
using CarRental.Application.Features.Mediator.Results.PricingResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class PricingsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> PricingList()
		{
			var values = await mediator.Send(new GetPricingQuery());
			return Ok(ApiResponse<List<GetPricingQueryResult>>.SuccessResponse(values, "Ödeme türleri başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> PricingById(int id)
		{
			var value = await mediator.Send(new GetPricingByIdQuery(id));
			return Ok(ApiResponse<GetPricingByIdQueryResult>.SuccessResponse(value, "İlgili ödeme türü bilgisi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreatePricing(CreatePricingCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreatePricingCommand>.SuccessResponse(createdData, "Yeni ödeme türü başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemovePricing(int id)
		{
			var removedData = await mediator.Send(new RemovePricingCommand(id));
			return Ok(ApiResponse<RemovePricingCommand>.SuccessResponse(removedData, "Ödeme türü başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePricing(UpdatePricingCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdatePricingCommand>.SuccessResponse(updatedData, "Ödeme türü bilgisi başarıyla güncellendi"));
		}
	}
}