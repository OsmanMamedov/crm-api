using General.Core.Entities.Concrete;
using General.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.Complex.Dtos
{
    public class CompanyDto : Dto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(Company item)
        {
            Id = item.Id;
            Name = item.Name;
            CreateBy = item.CreateBy;
            CreateDate = item.CreateDate;
            Deleted = item.Deleted;
            ModifyBy = item.ModifyBy;
            ModifyDate = item.ModifyDate;
        }

        //Entity properties
        public string Name { get; set; }


        public Company GetCompany()
        {
            var item = new Company
            {
                Id = Id,
                Name = Name,
                CreateBy = CreateBy,
                CreateDate = CreateDate,
                Deleted = Deleted,
                ModifyBy = ModifyBy,
                ModifyDate = ModifyDate
            };

            return item;
        }

    }
}
