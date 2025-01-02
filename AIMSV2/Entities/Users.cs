using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public int? CityID { get; set; }
        public int? DepartmentID { get; set; }
        public bool? Permissions { get; set; } = false;
        public DateOnly Created { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly? Modified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateOnly? Deleted { get; set; }
        public int? DeletedBy { get; set; }
    }
}
