using General.Entities.Complex.Dtos;

namespace General.Entities.Complex
{
    public class JwtTokenResult
    {
        public string Token { get; set; }
        public int Expire { get; set; }
        public UserDto User { get; set; }
        public bool Success { get; set; }
    }
}
