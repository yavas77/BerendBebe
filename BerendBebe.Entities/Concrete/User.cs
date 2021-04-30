using BerendBebe.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public User()
        {
            GenderId = 1;
        }
    }
}
