namespace AIMSV2.Models
{
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
        public DateOnly Created { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly? Modified { get; set; }
        public int? ModifiedBy { get; set; }
    }

    public class ProductIdRequest
    {
        public int Id { get; set; }
    }

}
