using System;
using System.Collections.Generic;
using System.Text;

namespace General.Core.Entities.Concrete
{
    public class Entity : IEntity
    {
        public Entity()
        {
        }

        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool Deleted { get; set; }
        public long? ModifyBy { get; set; }
        public long CreateBy { get; set; }
        public long Id { get ; set ; }
    }
}
