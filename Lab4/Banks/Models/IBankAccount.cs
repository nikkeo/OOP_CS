namespace Banks.Models;

public interface IBankAccount
{
    void Add(decimal money, AddTransaction addTransaction);
    void Transfer(decimal money, DateTime curDateTime, TransferTransaction transaction);
    void Withdraw(decimal money, DateTime curDateTime, WithdrawTransaction withdrawTransaction);
    decimal MoneyInTheFuture(DateTime futureDateTime);
    void AccountUpToDate(DateTime futureDateTime);

    void SetVerificationLimit(decimal limit);

    void SetTransferPercent(decimal percent);

    void SetTransferLimit(decimal limit);
    Guid GetId();
}