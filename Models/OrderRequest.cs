namespace waterapi;

public class OrderRequest
{
    public List<OrderDTO>? OrderDTOs {get; set;}
}

public class OrderDTO
{
    public int ProductId {get; set;}
    public int Amount {get; set;}
}

public class OrderResponse
{
    public int UserId {get; set;}
    public int OrderId {get; set;}
    public DateTime OrderDate {get; set;}
    public double TotalCost {get; set;}
    public List<OrderDetailResponse>? OrderDetailResponses {get; set;}
}

public class OrderDetailResponse
{
    public string? ProductName {get; set;}
    public double UnitPrice {get; set;}
    public int Amount {get; set;}
    public double TotalOfUnitPrice {get; set;}

}


