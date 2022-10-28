using General.Business.Abstract;
using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.Entities;
using General.Entities.Complex;
using General.Entities.Complex.Dtos;
using General.Entities.Complex.Requests;
using General.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace General.Api
{
    //[Authorize]
    [Route("api/[controller]s")]
    [Produces("application/json")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;
        public UserController(IUserService service,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost]
        public IActionResult Index([FromBody] RequestService<UserDto> requestService)
        {
            var userId = 1; //GetCurrentUserId();

            //if (GetCurrentUserId() == 0)
            //    return Ok(CouldntUser());

            switch (requestService.RequestType)
            {
                case Enums.RequestTypes.Create:
                    if (_service.Exists(requestService.Data.Email))
                        return Ok(CheckPhoneResponse());

                    requestService.Data.CreateBy = userId;
                    requestService.Data.CompanyId = 1;

                    if (!string.IsNullOrEmpty(requestService.Data.Photo))
                    {
                        string GuidKey = Guid.NewGuid().ToString();
                        string filename = GuidKey + "_" + requestService.Data.Id + ".jpg";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/personal/images/profiles/");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        General.Helper.ConvertHelper.ImageFromBase64String(requestService.Data.Photo, path + filename);
                        requestService.Data.Photo = "/personal/images/profiles/" + filename;
                    }

                    byte[] passwordHash, passwordSalt;
                    Hasher.PasswordHash(requestService.Data.Password, out passwordHash, out passwordSalt);
                    requestService.Data.PasswordHash = Convert.ToBase64String(passwordHash);
                    requestService.Data.PasswordSalt = Convert.ToBase64String(passwordSalt);
                    return Ok(_service.Add(requestService.Data));
                case Enums.RequestTypes.Read:
                    return Ok(_service.Get(requestService.Request));
                case Enums.RequestTypes.Update:
                    requestService.Data.ModifyBy = userId;
                    if (!string.IsNullOrEmpty(requestService.Data.Photo) && !requestService.Data.Photo.Contains("/personal"))
                    {
                        string GuidKey = Guid.NewGuid().ToString();
                        string filename = GuidKey + "_" + requestService.Data.Id + ".jpg";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/personal/images/profiles/");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        General.Helper.ConvertHelper.ImageFromBase64String(requestService.Data.Photo, path + filename);
                        requestService.Data.Photo = "/personal/images/profiles/" + filename;
                    }
                    if (!string.IsNullOrEmpty(requestService.Data.Password))
                    {
                        Hasher.PasswordHash(requestService.Data.Password, out byte[] updatePasswordHash, out byte[] updatePasswordSalt);
                        requestService.Data.PasswordHash = Convert.ToBase64String(updatePasswordHash);
                        requestService.Data.PasswordSalt = Convert.ToBase64String(updatePasswordSalt);
                    }
                    return Ok(_service.Update(requestService.Data));
                case Enums.RequestTypes.Delete:
                    requestService.Data.ModifyBy = userId;
                    return Ok(_service.Delete(requestService.Data));
                case Enums.RequestTypes.List:
                    return Ok(_service.GetList(requestService.Request));
                case Enums.RequestTypes.Any:
                    return Ok(_service.Any(requestService.Data));
                case Enums.RequestTypes.Token:
                    requestService.Request.Id = userId;
                    return Ok(_service.Get(requestService.Request));
                default:
                    return Ok(CheckRequestTypeResponse());
            }
        }

        [HttpPost("role")]
        public IActionResult Role([FromBody] RequestService<UserRole> requestService)
        {
            if (GetCurrentUserId() == 0)
                return Ok(CouldntUser());

            switch (requestService.RequestType)
            {
                case Enums.RequestTypes.Create:
                    return Ok(_service.AddRole(requestService.Data));
                case Enums.RequestTypes.Delete:
                    return Ok(_service.DeleteRole(requestService.Data));
                default:
                    return Ok(CheckRequestTypeResponse());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/token")]
        public IActionResult Login([FromBody] UserDto item)
        {
            if (string.IsNullOrEmpty(item.Phone) || string.IsNullOrEmpty(item.Password))
                return Ok(CheckBlankFieldResponse());

            var user = _service.GetByPhone(item.Phone);

            if (user == null)
                return Ok(CheckBlankFieldResponse());

            if (!Hasher.VerifyPassword(item.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                return Ok(CheckBlankFieldResponse());

            if (user.Id >= 0)
                return Ok(GetTokenResponse(user));
            else
                return Ok(CheckBlankFieldResponse());

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register([FromBody] UserDto item)
        {
            if (string.IsNullOrEmpty(item.Phone) || string.IsNullOrEmpty(item.Password) || string.IsNullOrEmpty(item.NewPassword))
                return Ok(CheckBlankFieldResponse());

            if (!_service.Exists(item.Phone))
            {
                return Ok(CheckPhoneResponse());
            }

            byte[] passwordHash, passwordSalt;
            Hasher.PasswordHash(item.Password, out passwordHash, out passwordSalt);
            item.PasswordHash = Convert.ToBase64String(passwordHash);
            item.PasswordSalt = Convert.ToBase64String(passwordSalt);
            return Ok(_service.Add(item));
        }

        private ResponseService GetTokenResponse(UserDto item)
        {
            var token = GetToken(item);
            JwtTokenResult result = new JwtTokenResult
            {
                Token = token,
                Expire = _configuration.GetValue<int>("Tokens:Lifetime"),
                User = item,
                Success = true
            };
            _response = new ResponseService
            {
                Data = result,
                Success = true
            };
            return _response;
        }
        private string GetToken(UserDto item)
        {
            var utcNow = DateTime.UtcNow;

            Claim[] claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, item.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, item.Phone),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddDays(_configuration.GetValue<int>("Tokens:LifeDay")),
                audience: _configuration.GetValue<string>("Tokens:Audience"),
                issuer: _configuration.GetValue<string>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
