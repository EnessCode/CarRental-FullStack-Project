using CarRental.Application.Common;
using CarRental.Application.Features.CQRS.Commands.CarCommands;
using CarRental.Application.Features.CQRS.Handlers.CarHandlers;
using CarRental.Application.Features.CQRS.Queries.CarQueries;
using CarRental.Application.Features.CQRS.Results.CarResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController(
		CreateCarCommandHandler createCarCommandHandler,
		GetCarQueryHandler getCarQueryHandler,
		GetCarByIdQueryHandler getCarByIdHandler,
		UpdateCarCommandHandler updateCarCommandHandler,
		RemoveCarCommandHandler removeCarCommandHandler,
		GetCarWithBrandQueryHandler getCarWithBrandQueryHandler,
		GetLast5CarsWithBrandQueryHandler getLast5CarsWithBrandQueryHandler,
		GetCarWithBrandByIdQueryHandler getCarWithBrandByIdQueryHandler
		) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> CarList()
		{
			var values = await getCarQueryHandler.Handle();
			return Ok(ApiResponse<List<GetCarQueryResult>>.SuccessResponse(values, "Tüm araçların listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> CarById(int id)
		{
			var value = await getCarByIdHandler.Handle(new GetCarByIdQuery(id));
			return Ok(ApiResponse<GetCarByIdQueryResult>.SuccessResponse(value, "İstenen aracın detay bilgileri getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateCar(CreateCarCommand command)
		{ 
			var createdData = await createCarCommandHandler.Handle(command);
			return StatusCode(201, ApiResponse<CreateCarCommand>.SuccessResponse(createdData, "Yeni araç kaydı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveCar(int id)
		{
			var removedData = await removeCarCommandHandler.Handle(new RemoveCarCommand(id));
			return Ok(ApiResponse<RemoveCarCommand>.SuccessResponse(removedData, "Araç kaydı sistemden başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
		{
			var updatedData = await updateCarCommandHandler.Handle(command);
			return Ok(ApiResponse<UpdateCarCommand>.SuccessResponse(updatedData, "Araç bilgileri başarıyla güncellendi"));
		}

		[HttpGet("GetCarWithBrand")]
		public async Task<IActionResult> GetCarWithBrand()
		{
			var values = await getCarWithBrandQueryHandler.Handle();
			return Ok(ApiResponse<List<GetCarWithBrandQueryResult>>.SuccessResponse(values, "Araçlar marka bilgileriyle birlikte listelendi"));
		}

		[HttpGet("GetLast5CarsWithBrand")]
		public async Task<IActionResult> GetLast5CarsWithBrand()
		{
			var values = await getLast5CarsWithBrandQueryHandler.Handle();
			return Ok(ApiResponse<List<GetLast5CarsWithBrandQueryResult>>.SuccessResponse(values, "Son 5 araç marka bilgileriyle birlikte listelendi"));
		}

		[HttpGet("GetCarWithBrandById/{id}")]
		public async Task<IActionResult> GetCarWithBrandById(int id)
		{
			var value = await getCarWithBrandByIdQueryHandler.Handle(new GetCarByIdQuery(id));
			return Ok(ApiResponse<GetCarWithBrandByIdQueryResult>.SuccessResponse(value, "Araç ve marka detayları başarıyla getirildi"));
		}
	}
}
