using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class Loggable
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpDate { get; set; }
        public int CreateUser { get; set; }
        public int LastupUser { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public Loggable()
        {
            IsActive = true;
            IsDelete = true;
            CreateDate = DateTime.Now;
            LastUpDate = DateTime.Now;
        }
    }
}
