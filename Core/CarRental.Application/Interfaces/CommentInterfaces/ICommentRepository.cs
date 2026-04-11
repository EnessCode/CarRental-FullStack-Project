using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Interfaces.CommentInterfaces
{
	public interface ICommentRepository : IRepository<Comment>
	{
		List<Comment> GetCommentsByBlogId(int id);
	}
}
