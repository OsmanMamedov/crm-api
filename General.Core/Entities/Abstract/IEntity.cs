using System;

namespace General.Core.Entities
{
    public interface IEntity : IBaseEntity
    {
        DateTime CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
        long? ModifyBy { get; set; }
        long CreateBy { get; set; }
        long Id { get; set; }
        bool Deleted { get; set; }
    }
}
