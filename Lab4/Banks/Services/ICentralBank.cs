using System.Collections.Immutable;
using Banks.Entities;
using Banks.Models;
using CustomExceptions;

namespace Banks.Models;

public interface ICentralBank
{
    Bank CreateBank(string name, List<Account> accounts, INotifiction notifiction);

    void Transfer(Guid guidToWithdraw, Guid guidToAdd, decimal money);

    void CancelTransfer(Guid guidToWithdraw, Guid guidToAdd, decimal money);

    void UpToDateAccounts(DateTime curDateTime);

    void SetLimitForUnverifiedClients(Bank bank, decimal limit);

    void SetPercentForTransaction(Bank bank, decimal percent);

    void SetLimitForTransaction(Bank bank, decimal limit);
}