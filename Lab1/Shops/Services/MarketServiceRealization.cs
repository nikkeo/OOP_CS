using CustomExceptions;
using Shops.Models;

namespace Shops.Services
{
    public class MarketServiceRealization : IMarketService
    {
        private List<Shop> shops = new List<Shop>();
        private List<Buyer> buyers = new List<Buyer>();
        public Shop AddShop(Guid uniqueId, string adress, string shopname, List<Product> products)
        {
            Shop shop = new Shop(uniqueId, adress, shopname, products);
            shops.Add(shop);
            return shop;
        }

        public Buyer AddBuyer(string name, decimal money = 0)
        {
            Buyer buyer = new Buyer(name, money);
            buyers.Add(buyer);
            return new Buyer(name, money);
        }

        public void TopUpBalance(Buyer buyer, decimal money)
        {
            buyer.TopUp(money);
        }

        public void ChangePrice(Shop shop, Product product, decimal price)
        {
            shop.ChangePrice(product, price);
        }

        public void SupplyShop(Shop shop, List<Product> products)
        {
            shop.Delivery(products);
        }

        public void MakeOrder(Buyer buyer, Shop shop, List<Product> products)
        {
            shop.Purchase(buyer, products);
        }

        public Shop? FindBestShopToBuy(Buyer buyer, List<Product> products)
        {
            List<decimal> pricesInShops = new List<decimal>();
            for (int i = 0; i < shops.Count; ++i)
            {
                pricesInShops.Add(0);
            }

            foreach (Product product in products)
            {
                for (int i = 0; i < shops.Count; ++i)
                {
                    if (pricesInShops[i] != -1)
                    {
                        if (shops[i].ProductsDictionary.ContainsKey(product.UniqueId)
                            && shops[i].ProductsDictionary[product.UniqueId].Quantity >= product.Quantity)
                        {
                            pricesInShops[i] += shops[i].ProductsDictionary[product.UniqueId].Price * product.Quantity;
                        }
                        else
                        {
                            pricesInShops[i] = -1;
                        }
                    }
                }
            }

            decimal bestvalue = 10e10m;
            Guid starter = Guid.Empty;
            for (int i = 0; i < shops.Count; ++i)
            {
                if (bestvalue > pricesInShops[i] && pricesInShops[i] > 0)
                {
                    bestvalue = pricesInShops[i];
                    starter = shops[i].UniqueId;
                }
            }

            return starter == Guid.Empty ? null : shops.Find(x => x.UniqueId.Equals(starter));
        }
    }
}