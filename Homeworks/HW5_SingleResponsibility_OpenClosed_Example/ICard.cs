interface ICard
{
    void DisplayCardSpecialPrice(double totalPrice);
}

class GoldCard : ICard
{
    private readonly double discount = 0.20;
    public void DisplayCardSpecialPrice(double totalPrice)
    {
        double newPrice = totalPrice - (totalPrice * discount);
        Console.WriteLine($"Golden Card Special Price: {newPrice}$");
    }
}

class SilverCard : ICard
{
    private readonly double discount = 0.15;
    public void DisplayCardSpecialPrice(double totalPrice)
    {
        double newPrice = totalPrice - (totalPrice * discount);
        Console.WriteLine($"Silver Card Special Price: {newPrice}$");
    }
}