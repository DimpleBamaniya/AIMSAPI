namespace Models; 
public class Pagination
{
    public int? PageNo { get; set; }
    public int? PageSize { get; set; }
    public string? SearchString { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class UsersWithPagination
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public int TotalRecords { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
