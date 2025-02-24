namespace Models;
public class ProductDetails
{
    public int ID { get; set; }
    public string? Code { get; set; }
    public int? CategoryID { get; set; }
    public string? CategoryName { get; set; }
    public int? BrandID { get; set; }
    public string? BrandName { get; set; }
    public int? Quantity { get; set; }
    public int? UseQuantity { get; set; }
    public int? AvailableQuantity { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public int? ModifiedBy { get; set; }
    public int? TotalRecords { get; set; }
}
