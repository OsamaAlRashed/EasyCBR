public class Order
{
    public Order() { }
    public Order(int id, string customerName, OrderType type, int price)
    {
        this.Id = id;
        this.CustomerName = customerName;
        this.Type = type;
        this.Price = price;
    }

    public int Id { get; set; }
    public string CustomerName { get; set; }
    public OrderType Type { get; set; }
    public decimal Price { get; set; }
}
