using General.Core.Entities.Entities;
using System;

namespace General.Core.Entities.Concrete
{
    public class Dto : IDto
    {

        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool Deleted { get; set; }
        public long? ModifyBy { get; set; }
        public long CreateBy { get; set; }
        public long Id { get; set; }
        public long Order { get; set; }
    }
}
