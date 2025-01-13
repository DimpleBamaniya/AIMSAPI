namespace AIMSV2.Models
{
    public class UserDto
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public int? CityID { get; set; } = null;
        public int? DepartmentID { get; set; }
        public DateOnly Created { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly? Modified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateOnly? Deleted { get; set; }
        public int? DeletedBy { get; set; }
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
        public string FirstName { get; set; }
    }

    public class DeleteUserDto
    {
        public int ID { get; set; }
        public int DeletedBy { get; set; }
    }

}
