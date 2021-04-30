using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.CommentDtos
{
    public class CommentAddDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string EmailAdress { get; set; }
        public bool IsActive { get; set; }
    }
}
