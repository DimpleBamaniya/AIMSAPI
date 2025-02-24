namespace Entities;
public class Products
{
    public int ID { get; set; }
    public int? CategoryID { get; set; }
    public int BrandID { get; set; }
    public int? Quantity { get; set; }
    public int? UseQuantity { get; set; }
    public int? AvailableQuantity { get; set; }
    public string? Code { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public int? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? Deleted { get; set; }
    public int? DeletedBy { get; set; }
}
