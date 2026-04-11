using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.BlogCommands;
using CarRental.Application.Features.Mediator.Queries.BlogQueries;
using CarRental.Application.Features.Mediator.Results.BlogResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class BlogsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> BlogList()
		{
			var values = await mediator.Send(new GetBlogQuery());
			return Ok(ApiResponse<List<GetBlogQueryResult>>.SuccessResponse(values, "Blog listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> BlogById(int id)
		{
			var value = await mediator.Send(new GetBlogByIdQuery(id));
			return Ok(ApiResponse<GetBlogByIdQueryResult>.SuccessResponse(value, "İlgili blog kaydı başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateBlog(CreateBlogCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateBlogCommand>.SuccessResponse(createdData, "Yeni blog kaydı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveBlog(int id)
		{
			var removedData = await mediator.Send(new RemoveBlogCommand(id));
			return Ok(ApiResponse<RemoveBlogCommand>.SuccessResponse(removedData, "Blog kaydı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBlog(UpdateBlogCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateBlogCommand>.SuccessResponse(updatedData, "Blog bilgisi başarıyla güncellendi"));
		}

		[HttpGet("GetLast3BlogsWithAuthor")]
		public async Task<IActionResult> GetLast3BlogsWithAuthor()
		{
			var values = await mediator.Send(new GetLast3BlogsWithAuthorQuery());
			return Ok(ApiResponse<List<GetLast3BlogsWithAuthorQueryResult>>.SuccessResponse(values, "Son 3 blog kaydı başarıyla getirildi"));
		}

		[HttpGet("GetAllBlogsWithAuthor")]
		public async Task<IActionResult> GetAllBlogsWithAuthor()
		{
			var values = await mediator.Send(new GetAllBlogsWithAuthorQuery());
			return Ok(ApiResponse<List<GetAllBlogsWithAuthorQueryResult>>.SuccessResponse(values, "Tüm bloglar yazar bilgileriyle başarıyla getirildi"));
		}

		[HttpGet("GetBlogDetails/{id}")]
		public async Task<IActionResult> GetBlogDetails(int id)
		{
			var value = await mediator.Send(new GetBlogDetailsQuery(id));
			return Ok(ApiResponse<GetBlogDetailsQueryResult>.SuccessResponse(value, "Blog detayları ve yazar bilgisi getirildi"));
		}

		[HttpGet("GetBlogsByCategoryId/{id}")]
		public async Task<IActionResult> GetBlogsByCategoryId(int id)
		{
			var values = await mediator.Send(new GetBlogByCategoryIdQuery(id));
			return Ok(ApiResponse<List<GetBlogByCategoryIdQueryResult>>.SuccessResponse(values, "Kategoriye ait bloglar başarıyla getirildi"));
		}
	}
}