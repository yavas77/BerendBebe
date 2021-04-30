using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı :")]
        public string CategoryName { get; set; }

        [Display(Name = "Durum :")]
        public bool IsActive { get; set; }
    }
}
