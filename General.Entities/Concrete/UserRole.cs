﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using General.Core.Entities.Concrete;

namespace General.Entities
{
    public partial class UserRole : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}