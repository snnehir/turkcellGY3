using System.Diagnostics;

abstract class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    public Product(int id, string? name, double unitPrice)
    {
        Id = id;
        Name = name;
        Price = unitPrice;
    }

    public virtual void DisplayProductDetail()
    {
        Console.WriteLine($"{Name}\t{Price}$\t-");
    }
}

class Clothing : Product
{
    public Clothing(int id, string? name, double price) : base(id, name, price)
    {
    }

}

class Electronic : Product
{
    public int WarrantyPeriod { get; set; }
    public Electronic(int id, string? name, double price, int warrantyYear) : base(id, name, price)
    {
        WarrantyPeriod = warrantyYear;
    }

    public override void DisplayProductDetail()
    {
        Console.WriteLine($"{Name}\t{Price}$\t(Warranty Period: {WarrantyPeriod})");
    }

}

