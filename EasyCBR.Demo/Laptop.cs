public class Laptop
{
    public Laptop() { }
    public Laptop(string modelName, Manufacture manufacture, int ram, string cpu, bool ssd, decimal price)
    {
        ModelName = modelName;
        Manufacture = manufacture;
        RAM = ram;
        CPU = cpu;
        SSD = ssd;
        Price = price;
    }

    public string ModelName { get; set; }
    public Manufacture Manufacture { get; set; }
    public int RAM { get; set; }
    public string CPU { get; set; }
    public bool SSD { get; set; }
    public decimal Price { get; set; }
}

public enum Manufacture
{
    Dell,
    Hp,
    Asus,
    Lenovo
}
