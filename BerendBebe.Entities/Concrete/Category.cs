
using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class Category:Loggable,IEntity
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
        
    }
}
