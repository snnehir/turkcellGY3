class ShoppingCart
{
    // Encapsulation: All operations to this list can be done through defined methods in the class.
    private readonly List<Product> products;
    // Open-closed Principle: Open for extension (different cards) closed for modification (DisplayCartSummary method will not be effected.) 
    public ICard Card { get; set; }
    public ShoppingCart(ICard card)
    {
        Card = card;
        products = new List<Product>();
    }
    // Single Responsibility Principle: ShoppingCart class is only responsible for Cart operations
    public void AddProductToCart(Product product)
    {
        products.Add(product);
    }
    public void DeleteProductFromCart(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
        }
    }
    public void DisplayCartSummary()
    {
        double totalPrice = 0;
        Console.WriteLine("Name\tPrice\tDetail\n****\t*****\t******");
        foreach (Product p in products)
        {
            totalPrice += p.Price;
            p.DisplayProductDetail();
        }
        Console.WriteLine($"\nTotal Price: {totalPrice}$");
        Card.DisplayCardSpecialPrice(totalPrice);
    }
}
