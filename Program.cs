using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using waterapi;

var builder = WebApplication.CreateBuilder(args);

var jsonConnection = builder.Configuration.GetConnectionString("Psql");
var dockercnnstring = Environment.GetEnvironmentVariable("DBCNND");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WaterContext>(options => {
    options.UseNpgsql(jsonConnection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/order", async (WaterContext db, OrderRequest request) =>
{
    var newOrder = new Order
    {
        OrderDate = DateTime.UtcNow,
        OrderDetails = request.OrderDTOs.Select(e => new OrderDetail{ Amount = e.Amount, ProductId = e.ProductId}).ToList()
    };
    await db.Orders.AddAsync(newOrder);
    await db.SaveChangesAsync();
    var result = new OrderResponse
    {
        UserId = 1,
        OrderId = newOrder.Id,
        OrderDate = newOrder.OrderDate,
        OrderDetailResponses = db.OrderDetails.Where(e => e.OrderId == newOrder.Id)
        .Select(e => new OrderDetailResponse{
            ProductName = e.Product.Name,
            Amount = e.Amount,
            UnitPrice = e.Product.Price,
            TotalOfUnitPrice = e.Amount * e.Product.Price,
        })
        .ToList()
    };
    var jsonOption = new JsonSerializerOptions{
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.Preserve
    };
    double total = 0;
    foreach (var e in result.OrderDetailResponses) {
        total += e.TotalOfUnitPrice;
    }
    result.TotalCost = total;
    var json = JsonSerializer.Serialize(result, jsonOption);
    return json;
})
.WithName("GetWater")
.WithOpenApi();

app.Run();


