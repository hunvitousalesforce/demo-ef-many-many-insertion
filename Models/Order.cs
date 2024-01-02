using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waterapi;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public DateTime OrderDate {get; set;}
    public ICollection<OrderDetail> OrderDetails {get; set;} = new List<OrderDetail>();

    public ICollection<Product> Products {get; set;} = new List<Product>();
}
