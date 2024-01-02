using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waterapi;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    public string Name {get; set;} = string.Empty;

    public double Price {get; set;}

    public ICollection<OrderDetail> OrderDetails {get; set;} = new List<OrderDetail>();
    public ICollection<Order> Orders {get; set;} = new List<Order>();
}
