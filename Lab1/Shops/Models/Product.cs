using CustomExceptions;
using Shops.Entities;

namespace Shops.Models
{
    public class Product
    {
        private ProductStats _productStats = new ProductStats();

        public Product(Guid uniqueId, int quantity = 0, decimal price = 0)
        {
            if (quantity < 0)
                throw new NotCorrectQuantityException();

            _productStats.Quantity = quantity;
            _productStats.Price = price;
            UniqueId = uniqueId;
        }

        public decimal Price { get => _productStats.Price; }

        public int Quantity { get => _productStats.Quantity; }

        public Guid UniqueId { get; }

        public void Replenishment(int quantity)
        {
            if (quantity < 0)
                throw new NotCorrectQuantityException();
            _productStats.Quantity += quantity;
        }

        public void Purchase(int quantity)
        {
            if (quantity > _productStats.Quantity)
                throw new NotEnoughtQuantityException();

            if (quantity < 0)
                throw new NotCorrectQuantityException();

            _productStats.Quantity -= quantity;
        }

        public void ChangePrice(decimal newprice)
        {
            _productStats.Price = newprice;
        }
    }
}