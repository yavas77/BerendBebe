using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.ProductDtos
{
    public class ProductAddDto
    {
        [Display(Name = "Ürün Adı :")]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat :")]
        public decimal? Price { get; set; }

        [Display(Name = "Açıklama :")]
        public string Description { get; set; }

        [Display(Name = "Stok :")]
        public int? UnitInStok { get; set; }

        [Display(Name = "Kategori :")]
        public int? CategoryId { get; set; }
    }
}
