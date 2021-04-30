using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.CommentDtos
{
    public class CommentListDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string EmailAddress { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
