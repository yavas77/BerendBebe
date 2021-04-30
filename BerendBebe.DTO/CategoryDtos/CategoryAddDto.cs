using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.CategoryDtos
{
    public class CategoryAddDto
    {
        [Display(Name = "Kategori Adı :")]
        public string CategoryName { get; set; }
    }
}
