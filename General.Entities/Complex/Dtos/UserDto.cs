using General.Core.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace General.Entities.Complex.Dtos
{
    public class UserDto : Dto
    {
        public UserDto()
        {
            
        }

        public UserDto(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            PasswordHash = user.PasswordHash;
            PasswordSalt = user.PasswordSalt;
            BirthDate = user.BirthDate;
            BirthDateString = user.BirthDate.ToString("dd.mm.yyyy");
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            CreateBy = user.CreateBy;
            CreateDate = user.CreateDate;
            ModifyBy = user.ModifyBy;
            ModifyDate = user.ModifyDate;
            Id = user.Id;
            CompanyId = user.CompanyId;
            Status = user.Status;

            if (user.UserRoles != null && user.UserRoles.Count > 0)
            {
                foreach (var ur in user.UserRoles)
                {
                    Roles.Add(ur.Role.Name);
                }
            }
        }

        //Entity Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDateString { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        //Dto Properties
        public string FullName { get; set; }
        public string NewPassword { get; set; }
        public long CompanyId { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<string> Roles { get; set; }

        //methods
        public User GetUser()
        {
            var user = new User
            {
                CreateBy = CreateBy,
                CreateDate = CreateDate,
                Email = Email,
                Name = Name,
                Id = Id,
                BirthDate = BirthDate,
                Surname = Surname,
                ModifyBy = ModifyBy,
                ModifyDate = ModifyDate,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                Phone = Phone,
                Photo = Photo,
                CompanyId = CompanyId,
                Status = Status
            };
            return user;
        }
    }
}
