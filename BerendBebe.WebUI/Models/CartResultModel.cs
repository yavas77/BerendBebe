using BerendBebe.MvcHelpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Models
{
    public class CartResultModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
        public int Quantity { get; set; }
        public string Total { get; set; }
        public string Count { get; set; }
        public string Title { get; set; }
    }
}
