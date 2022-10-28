using General.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.Complex.Dtos
{
    public class RoleDto : Dto
    {

        public RoleDto()
        {

        }
        public RoleDto(Role role)
        {
            Name = role.Name;
            Id = role.Id;
        }

        //entities
        public string Name { get; set; }

        //methods
        public Role GetRole()
        {
            var role = new Role
            {
                CreateBy = CreateBy,
                CreateDate = CreateDate,
                Id = Id,
                ModifyBy = ModifyBy,
                ModifyDate = ModifyDate,
                Name = Name,
            };
            return role;
        }
    }
}
