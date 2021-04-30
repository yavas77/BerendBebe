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
    public class ContactManager : BaseManager<Contact>, IContactService
    {
        private readonly IBaseDal<Contact> _baseDal;

        public ContactManager(IBaseDal<Contact> baseDal) : base(baseDal)
        {
            _baseDal = baseDal;
        }

        public async Task<List<Contact>> GetIncomingByActiveAsync()
        {
            return await _baseDal.GetAllAsync(x => x.IsActive == true && x.MailType == false);
        }

        public async Task<List<Contact>> GetAllIsDeleteShortByIdAsync()
        {
            return await _baseDal.GetAllAsync(x => x.IsDelete == false && x.MailType == false);

        }

        public async Task<List<Contact>> GetIncomingShortByIdAsync()
        {
            return await _baseDal.GetAllAsync(x => x.MailType == false && x.IsDelete == true, x => x.Id);
        }

        public async Task<bool> MailExistsAsync(int id)
        {
            return await _baseDal.FindByIdAsync(id) != null;
        }

        public async Task<List<Contact>> GetAllSendedShortByIdAsync()
        {
            return await _baseDal.GetAllAsync(x => x.MailType == true && x.IsDelete == true, x => x.Id);
        }
    }
}
