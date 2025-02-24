namespace Models;
public class UserProductIdRequest
{
    public int ID { get; set; }
}

public class SaveUserProduct
{
    public int ID { get; set; }
    public int CategoryID { get; set; }
    public int BrandID { get; set; }
    public int CreatedBy { get; set; }
}
