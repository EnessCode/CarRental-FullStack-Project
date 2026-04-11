using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.ServiceCommands;
using CarRental.Application.Features.Mediator.Queries.ServiceQueries;
using CarRental.Application.Features.Mediator.Results.ServiceResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class ServicesController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> ServiceList()
		{
			var values = await mediator.Send(new GetServiceQuery());
			return Ok(ApiResponse<List<GetServiceQueryResult>>.SuccessResponse(values, "Hizmetler listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> ServiceById(int id)
		{
			var value = await mediator.Send(new GetServiceByIdQuery(id));
			return Ok(ApiResponse<GetServiceByIdQueryResult>.SuccessResponse(value, "İlgili hizmet bilgisi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateService(CreateServiceCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateServiceCommand>.SuccessResponse(createdData, "Yeni hizmet kaydı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveService(int id)
		{
			var removedData = await mediator.Send(new RemoveServiceCommand(id));
			return Ok(ApiResponse<RemoveServiceCommand>.SuccessResponse(removedData, "Hizmet kaydı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateService(UpdateServiceCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateServiceCommand>.SuccessResponse(updatedData, "Hizmet bilgisi başarıyla güncellendi"));
		}
	}
}