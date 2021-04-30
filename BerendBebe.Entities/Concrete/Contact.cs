using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class Contact : Loggable, IEntity
    {
        public string Name { get; set; }
        public string EmailAdress { get; set; }
        //public string SendedEmailAdress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ReplyMessage { get; set; }

        //MailType false ise gelen mail, true ise gönderilen maildir.
        public bool MailType { get; set; }

        public Contact()
        {
            MailType = false;
        }
    }
}
