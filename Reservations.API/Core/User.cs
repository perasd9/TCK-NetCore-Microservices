namespace Reservations.API.Core
{
    public class User
    {
        public Guid UserId { get; set; }
        public string JMBG { get; set; } = "";
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public Guid PlaceId { get; set; }
        //public Reservation? Place { get; set; }
    }
}
