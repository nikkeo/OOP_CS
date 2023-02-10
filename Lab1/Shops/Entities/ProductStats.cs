using CustomExceptions;
namespace Shops.Entities;

public class ProductStats
{
    private decimal _price;
    private int _quantity;

    public ProductStats(decimal price = 0, int quantuty = 0)
    {
        Price = price;
        Quantity = quantuty;
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new NotCorrectMoneyAmountException();
            _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new NotCorrectQuantityException();
            _quantity = value;
        }
    }
}