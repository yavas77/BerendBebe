using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Areas.Admin.Models.CategoryModels
{
    public class CategoryAddViewModel
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }

        public CategoryAddViewModel()
        {
            IsActive = true;
        }
    }
}
