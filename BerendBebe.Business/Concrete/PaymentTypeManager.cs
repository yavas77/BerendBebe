using BerendBebe.Business.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Concrete
{
    public class PaymentTypeManager : BaseManager<PaymentType>, IPaymentTypeService
    {
        private readonly IBaseDal<PaymentType> _baseDal;

        public PaymentTypeManager(IBaseDal<PaymentType> baseDal) : base(baseDal)
        {
            _baseDal = baseDal;
        }
    }
}
