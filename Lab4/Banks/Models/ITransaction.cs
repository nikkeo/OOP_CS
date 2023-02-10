namespace Banks.Models;

public interface ITransaction
{
    decimal MoneyOfTransaction();

    DateTime TimeOfTransaction();
}