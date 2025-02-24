namespace Entities;
public class UserDetailsList
{
    public int ID { get; set; }
    public string? UserCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? DepartmentID { get; set; }
    public string? DepartmentName { get; set; }
    public int? CityID { get; set; }
    public string? CityName { get; set; }
    public bool? Permissions { get; set; } = false;
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public int? ModifiedBy { get; set; }
    public int? TotalRecords { get; set; }

}
