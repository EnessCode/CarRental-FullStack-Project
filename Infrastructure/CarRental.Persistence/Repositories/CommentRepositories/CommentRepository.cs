using CarRental.Application.Interfaces.CommentInterfaces;
using CarRental.Domain.Entities;
using CarRental.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Persistence.Repositories.CommentRepositories
{
	public class CommentRepository(CarRentalContext context) : ICommentRepository
	{
		public async Task CreateAsync(Comment entity)
		{
			await context.Comments.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task<List<Comment>> GetAllAsync()
		{
			return await context.Comments.Select(x => new Comment
			{
				Id = x.Id,
				BlogId = x.BlogId,
				CreatedDate = x.CreatedDate,
				Description = x.Description
			}).ToListAsync();
		}

		public async Task<Comment?> GetByFilterAsync(Expression<Func<Comment, bool>> filter)
		{
			return await context.Comments.SingleOrDefaultAsync(filter);
		}

		public async Task<Comment> GetByIdAsync(int id)
		{
			return await context.Comments.FindAsync(id);
		}

		public List<Comment> GetCommentsByBlogId(int id)
		{
			return context.Comments.Where(x => x.BlogId == id).Include(x => x.Blog).OrderByDescending(x => x.CreatedDate).ToList();
		}

		public async Task RemoveAsync(Comment entity)
		{
			context.Comments.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Comment entity)
		{
			context.Comments.Update(entity);
			await context.SaveChangesAsync();
		}
	}
}