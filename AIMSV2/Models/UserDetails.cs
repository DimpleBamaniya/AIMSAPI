namespace Models;
public class UserDetails
{
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailID { get; set; }
        public string? Password { get; set; }
        public string? CityID { get; set; }
        public int? DepartmentID { get; set; }
        public bool? Permissions { get; set; } = false;
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? Deleted { get; set; }
        public int? DeletedBy { get; set; }

}
