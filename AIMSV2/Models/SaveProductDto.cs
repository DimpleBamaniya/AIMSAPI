namespace AIMSV2.Models
{
    public class SaveProductDto
   {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public int? Quantity { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

    }

}
