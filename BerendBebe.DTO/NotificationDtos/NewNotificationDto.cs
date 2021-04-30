using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.NotificationDtos
{
    public class NewNotificationDto
    {
        public int OrderCount { get; set; }
        public int MailCount { get; set; }
        public int CommentCount { get; set; }
        public int CountTotal { get; set; }
    }
}
