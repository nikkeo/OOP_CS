using CustomExceptions;

namespace Shops.Models
{
    public class Buyer
    {
        public Buyer(string name, decimal money = 0)
        {
            if (money < 0)
                throw new NotCorrectMoneyAmountException();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NotCorrectBuyerNameException();
            }

            BuyerName = name;
            Money = money;
        }

        public string BuyerName { get; }

        public decimal Money { get; private set; }

        public void Purchase(decimal price)
        {
            if (price > Money)
                throw new NotEnoughtMoneyException();

            Money -= price;
        }

        public void TopUp(decimal money)
        {
            if (money < 0)
                throw new NotCorrectMoneyAmountException();

            Money += money;
        }
    }
}