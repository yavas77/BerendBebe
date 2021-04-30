using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class PaymentType:IEntity
    {
        public int Id { get; set; }
        public string PaymentName { get; set; }
    }
}
