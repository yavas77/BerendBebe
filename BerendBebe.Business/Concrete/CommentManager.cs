using BerendBebe.Business.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Concrete
{
    public class CommentManager : BaseManager<Comment>, ICommentService

    {
        private readonly IBaseDal<Comment> _baseDal;
        private readonly ICommentDal _commentDal;

        public CommentManager(IBaseDal<Comment> baseDal, ICommentDal commentDal) : base(baseDal)
        {
            _baseDal = baseDal;
            _commentDal = commentDal;
        }

        public async Task<bool> CommentExistsAsync(int id)
        {
            return await _baseDal.GetAsync(x => x.Id == id) == null ? false : true;
        }

        public async Task<List<Comment>> GetAllConfirmedByProducdIdAsync(int id)
        {
            return await _baseDal.GetAllAsync(x => x.ProductId == id && x.IsActive == true);
        }

        public async Task<List<Comment>> GetAllPassiveAsync()
        {
            return await _baseDal.GetAllAsync(x => x.IsActive == false);
        }

        public async Task<List<Comment>> GetAllWithProducAsync()
        {
            return await _commentDal.GetAllWithProductAsync();
        }
    }
}
