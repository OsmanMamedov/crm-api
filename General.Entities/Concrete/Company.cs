using General.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.Concrete
{
    public class Company : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
