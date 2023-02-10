using CustomExceptions;
using Shops.Entities;
using Shops.Models;
using Shops.Services;

using Xunit;

namespace Shops.Tests
{
    public class ShopsTests
    {
        [Fact]
        public void DeliveryToTheShopAndProductsInTheShop_ThrowException()
        {
            MarketServiceRealization marketService = new MarketServiceRealization();
            List<Product> products = new List<Product>();
            Shop shop = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);
            products.Add(new Product(Guid.NewGuid(), 1, 10));
            products.Add(new Product(Guid.NewGuid(), 2, 20));
            products.Add(new Product(Guid.NewGuid(), 3, 30));
            products.Add(new Product(Guid.NewGuid(), 4, 40));
            products.Add(new Product(Guid.NewGuid(), 5, 50.12m));

            marketService.SupplyShop(shop, products);

            Assert.True(
                products.All(x => shop.ProductsDictionary.ContainsKey(x.UniqueId)),
                "Not all orifinal products in the shop");
        }

        [Fact]
        public void SettingAndChangingPricesInTheShop_ThrowException()
        {
            MarketServiceRealization marketService = new MarketServiceRealization();
            List<Product> products = new List<Product>();
            Product product1 = new Product(Guid.NewGuid(), 1, 10m);
            products.Add(product1);

            // setting price contition
            Shop shop = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);
            Assert.True(
                shop.ProductsDictionary[product1.UniqueId].Price == product1.Price,
                "Not valid product price in shop");

            // changing price contition
            decimal newprice = 40.27575756m;
            shop.ProductsDictionary[product1.UniqueId].ChangePrice(newprice);
            Assert.True(
                shop.ProductsDictionary[product1.UniqueId].Price == newprice,
                "The price haven`t changed");
        }

        [Fact]
        public void BestShopToBuyIfExists_ThrowException()
        {
            MarketServiceRealization marketService = new MarketServiceRealization();
            Buyer buyer = marketService.AddBuyer("Arnold", 100);
            Guid productGuidId = Guid.NewGuid();
            Guid productGuidId2 = Guid.NewGuid();
            Product product = new Product(productGuidId, 1, 20);
            Product product2 = new Product(productGuidId2, 1, 20);
            List<Product> products = new List<Product>();
            products.Add(product);
            products.Add(product2);
            Shop shop1 = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);
            Shop shop2 = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);
            Shop shop3 = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);
            marketService.ChangePrice(shop2, product, 10);
            marketService.ChangePrice(shop3, product, 30);

            // best price statement
            decimal bestprice = 1000;
            List<Product> productsForBestShopTest = new List<Product>();
            productsForBestShopTest.Add(product);
            Shop? bestshop = marketService.FindBestShopToBuy(buyer, productsForBestShopTest);
            if (bestshop != null)
            {
                bestprice = bestshop.ProductsDictionary[product.UniqueId].Price
                    + bestshop.ProductsDictionary[product2.UniqueId].Price;
            }

            Assert.True(bestprice == 30, "Not the best price");

            // enought quantity statement
            marketService.SupplyShop(shop3, products);
            marketService.ChangePrice(shop3, product, 30);
            Product sameProductWithAnotherQuantity = new Product(productGuidId, 2, 20);
            bestprice = 1000;
            productsForBestShopTest = new List<Product>();
            productsForBestShopTest.Add(sameProductWithAnotherQuantity);
            Shop? bestshop2 = marketService.FindBestShopToBuy(buyer, productsForBestShopTest);
            if (bestshop2 != null)
            {
                bestprice = bestshop2.ProductsDictionary[product.UniqueId].Price;
            }

            Assert.True(bestprice == 30, "Not the best price");

            // not enought quantity for product
            Product sameProductWithUnrealQuantity = new Product(productGuidId, 3, 20);
            bestprice = 1000;
            productsForBestShopTest = new List<Product>();
            productsForBestShopTest.Add(sameProductWithUnrealQuantity);
            Shop? bestshop3 = marketService.FindBestShopToBuy(buyer, productsForBestShopTest);
            Assert.True(bestshop3 == null, "Shop with unreal quantity were found");

            // product we cant find in shop
            Product unrealProduct = new Product(Guid.NewGuid(), 3, 20);
            bestprice = 1000;
            productsForBestShopTest = new List<Product>();
            productsForBestShopTest.Add(unrealProduct);
            Shop? bestshop4 = marketService.FindBestShopToBuy(buyer, productsForBestShopTest);
            Assert.True(bestshop4 == null, "Not product in shop, but we found it");
        }

        [Fact]
        public void BuyFromTheShopAllIsCorrect_ThrowException()
        {
            MarketServiceRealization marketService = new MarketServiceRealization();
            Buyer buyer = new Buyer("roma", 10);

            List<Product> products = new List<Product>();
            Product product1 = new Product(Guid.NewGuid(), 1, 20);
            Product product2 = new Product(Guid.NewGuid(), 1, 30);
            Product product3 = new Product(Guid.NewGuid(), 1, 40);
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            Shop shop1 = marketService.AddShop(Guid.NewGuid(), "justshop", "1", products);

            marketService.TopUpBalance(buyer, 10000);
            marketService.MakeOrder(buyer, shop1, products);

            Assert.True(
                shop1.ProductsDictionary.Values.All(x => x.Quantity == 0),
                "Not all good are gone");

            Assert.Throws<NotEnoughtQuantityException>(() => marketService.MakeOrder(buyer, shop1, products));

            marketService.SupplyShop(shop1, products);
            marketService.MakeOrder(buyer, shop1, products);

            Assert.True(
                shop1.ProductsDictionary.Values.All(x => x.Quantity == 0),
                "Not all good are gone");
        }
    }
}