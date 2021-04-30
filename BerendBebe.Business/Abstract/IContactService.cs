using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IContactService : IBaseService<Contact>
    {
        Task<List<Contact>> GetIncomingShortByIdAsync();
        Task<List<Contact>> GetAllIsDeleteShortByIdAsync();
        Task<List<Contact>> GetIncomingByActiveAsync();

        Task<List<Contact>> GetAllSendedShortByIdAsync();
        Task<bool> MailExistsAsync(int id);
    }
}
