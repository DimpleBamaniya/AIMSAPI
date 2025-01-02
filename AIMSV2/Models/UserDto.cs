namespace AIMSV2.Models
{
    public class UserDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? CityID { get; set; }
        public int? EducationID { get; set; }
    }

    public class LoginUserDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
    }

    public class LoginUserDetailsDto
    {
        public int? ID { get; set; }
        public bool? Permissions { get; set; } = false;
    }
}
