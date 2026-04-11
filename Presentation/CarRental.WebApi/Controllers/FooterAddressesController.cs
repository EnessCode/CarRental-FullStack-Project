using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.FooterAddressCommands;
using CarRental.Application.Features.Mediator.Queries.FooterAddressQueries;
using CarRental.Application.Features.Mediator.Results.FooterAddressResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class FooterAddressesController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> FooterAddressList()
		{
			var values = await mediator.Send(new GetFooterAddressQuery());
			return Ok(ApiResponse<List<GetFooterAddressQueryResult>>.SuccessResponse(values, "Alt bilgi adres listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> FooterAddressById(int id)
		{
			var value = await mediator.Send(new GetFooterAddressByIdQuery(id));
			return Ok(ApiResponse<GetFooterAddressByIdQueryResult>.SuccessResponse(value, "İlgili alt bilgi adresi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateFooterAddress(CreateFooterAddressCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateFooterAddressCommand>.SuccessResponse(createdData, "Yeni alt bilgi adresi başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveFooterAddress(int id)
		{
			var removedData = await mediator.Send(new RemoveFooterAddressCommand(id));
			return Ok(ApiResponse<RemoveFooterAddressCommand>.SuccessResponse(removedData, "Alt bilgi adresi başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddressCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateFooterAddressCommand>.SuccessResponse(updatedData, "Alt bilgi adresi başarıyla güncellendi"));
		}
	}
}
