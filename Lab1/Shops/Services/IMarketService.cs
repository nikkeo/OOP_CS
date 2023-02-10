using Shops.Models;

namespace Shops.Services
{
    public interface IMarketService
    {
        Shop AddShop(Guid uniqueId, string adress, string shopname, List<Product> products);

        Buyer AddBuyer(string name, decimal money = 0);

        void TopUpBalance(Buyer buyer, decimal money);

        public void ChangePrice(Shop shop, Product product, decimal price);

        void SupplyShop(Shop shop, List<Product> products);

        void MakeOrder(Buyer buyer, Shop shop, List<Product> products);

        public Shop? FindBestShopToBuy(Buyer buyer, List<Product> products);
    }
}