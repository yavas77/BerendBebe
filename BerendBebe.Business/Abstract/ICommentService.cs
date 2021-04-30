using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface ICommentService : IBaseService<Comment>
    {
        Task<List<Comment>> GetAllConfirmedByProducdIdAsync(int id);
        Task<List<Comment>> GetAllWithProducAsync();
        Task<List<Comment>> GetAllPassiveAsync();
        Task<bool> CommentExistsAsync(int id);
    }
}
