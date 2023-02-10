using System.Collections.Immutable;
using CustomExceptions;

namespace Shops.Models
{
    public class Shop
    {
        private Dictionary<Guid, Product> _products;

        public Shop(Guid uniqueId, string adress, string shopname, List<Product> products)
        {
            if (string.IsNullOrWhiteSpace(adress))
                throw new NotCorrectShopAdressException();

            if (string.IsNullOrWhiteSpace(shopname))
                throw new NotCorrectShopNameException();

            _products = new Dictionary<Guid, Product>();
            UniqueId = uniqueId;
            Adress = adress;
            ShopName = shopname;
            products?.ForEach(x => _products.Add(x.UniqueId, new Product(x.UniqueId, x.Quantity, x.Price)));
        }

        public string Adress { get; }

        public string ShopName { get; }

        public Guid UniqueId { get; }

        public ImmutableDictionary<Guid, Product> ProductsDictionary { get => _products.ToImmutableDictionary(); }

        public void Delivery(List<Product> products)
        {
            foreach (Product product in products)
            {
                if (product == null)
                    throw new NotCorrectProductException();

                if (_products.ContainsKey(product.UniqueId))
                {
                    _products[product.UniqueId].Replenishment(product.Quantity);
                }
                else
                {
                    _products[product.UniqueId] = new Product(product.UniqueId, product.Quantity, product.Price);
                }
            }
        }

        public void Purchase(Buyer buyer, List<Product> products)
        {
            if (buyer == null)
            {
                throw new NotCorrectBuyerException();
            }

            decimal allProductsPrice = 0;
            for (int i = 0; i < products.Count; ++i)
            {
                if (products[i] == null)
                    throw new NotCorrectProductException();
                if (products[i].Quantity > _products[products[i].UniqueId].Quantity)
                    throw new NotEnoughtQuantityException();

                allProductsPrice += products[i].Price * products[i].Quantity;
            }

            if (allProductsPrice > buyer.Money)
                throw new NotEnoughtMoneyException();

            for (int i = 0; i < products.Count; ++i)
            {
                _products[products[i].UniqueId].Purchase(products[i].Quantity);
                buyer.Purchase(products[i].Quantity * products[i].Price);
            }
        }

        public void ChangePrice(Product product, decimal newprice)
        {
            if (product == null || _products[product.UniqueId] == null)
                throw new NotCorrectProductException();

            _products[product.UniqueId].ChangePrice(newprice);
        }
    }
}