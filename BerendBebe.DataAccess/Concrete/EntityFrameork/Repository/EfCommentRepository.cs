using BerendBebe.DataAccess.Absctract;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Context;
using BerendBebe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DataAccess.Concrete.EntityFrameork.Repository
{
    public class EfCommentRepository : EfBaseRepository<Comment>, ICommentDal
    {
        public async Task<List<Comment>> GetAllWithProductAsync()
        {
            using var context = new BerendBebeContext();
            return await context.Comments.Include(x => x.Product).ToListAsync();
        }
    }
}
