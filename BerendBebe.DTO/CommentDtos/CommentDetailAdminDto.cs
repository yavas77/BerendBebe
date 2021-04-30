using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.CommentDtos
{
    public class CommentDetailAdminDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string EmailAdress { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
