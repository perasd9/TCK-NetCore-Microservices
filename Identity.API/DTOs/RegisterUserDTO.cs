using Identity.API.Core;

namespace Identity.API.DTOs
{
    public class RegisterUserDTO
    {
        public Guid UserId { get; set; }
        public string JMBG { get; set; } = "";
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public Guid PlaceId { get; set; }
        public Guid RoleId { get; set; }
    }
}
