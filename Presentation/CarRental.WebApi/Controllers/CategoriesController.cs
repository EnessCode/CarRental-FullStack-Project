using CarRental.Application.Common;
using CarRental.Application.Features.CQRS.Commands.CategoryCommands;
using CarRental.Application.Features.CQRS.Handlers.CategoryHandlers;
using CarRental.Application.Features.CQRS.Queries.CategoryQueries;
using CarRental.Application.Features.CQRS.Results.CategoryResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController(
		CreateCategoryCommandHandler createCategoryCommandHandler,
		GetCategoryQueryHandler getCategoryQueryHandler,
		GetCategoryByIdQueryHandler getCategoryByIdHandler,
		UpdateCategoryCommandHandler updateCategoryCommandHandler,
		RemoveCategoryCommandHandler removeCategoryCommandHandler,
		GetCategoryWithBlogCountQueryHandler getCategoryWithBlogCountQueryHandler
		) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> CategoryList()
		{
			var values = await getCategoryQueryHandler.Handle();
			return Ok(ApiResponse<List<GetCategoryQueryResult>>.SuccessResponse(values, "Araç kategorileri listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> CategoryById(int id)
		{
			var value = await getCategoryByIdHandler.Handle(new GetCategoryByIdQuery(id));
			return Ok(ApiResponse<GetCategoryByIdQueryResult>.SuccessResponse(value, "İstenen kategori bilgisi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
		{
			var createdData = await createCategoryCommandHandler.Handle(command);
			return StatusCode(201, ApiResponse<CreateCategoryCommand>.SuccessResponse(createdData, "Yeni kategori başarıyla sisteme eklendi"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveCategory(int id)
		{
			var removedData = await removeCategoryCommandHandler.Handle(new RemoveCategoryCommand(id));
			return Ok(ApiResponse<RemoveCategoryCommand>.SuccessResponse(removedData, "Kategori kaydı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
		{
			var updatedData = await updateCategoryCommandHandler.Handle(command);
			return Ok(ApiResponse<UpdateCategoryCommand>.SuccessResponse(updatedData, "Kategori bilgisi başarıyla güncellendi"));
		}

		[HttpGet("GetCategoriesWithBlogCount")]
		public async Task<IActionResult> GetCategoriesWithBlogCount()
		{
			var values = await getCategoryWithBlogCountQueryHandler.Handle();
			return Ok(ApiResponse<List<GetCategoryWithBlogCountQueryResult>>.SuccessResponse(values, "Kategoriler ve blog sayıları başarıyla listelendi"));
		}
	}
}
