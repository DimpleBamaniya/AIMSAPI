namespace AIMSV2.Entities
{
    public class UserDetailsList
    {
            public int ID { get; set; }
            public string? UserCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? DepartmentID { get; set; }
            public string? DepartmentName { get; set; }
            public int? CityID { get; set; }
            public string? CityName { get; set; }
            public bool? Permissions { get; set; } = false;
            public DateOnly Created { get; set; }
            public int CreatedBy { get; set; }
            public DateOnly? Modified { get; set; }
            public int? ModifiedBy { get; set; }

    }
}
