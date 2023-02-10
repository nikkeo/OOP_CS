using CustomExceptions;

namespace Banks.Models;

public class DepositPercent
{
    public const int LowBound = 0;
    public const decimal LowPercentBound = 0;
    public const decimal HighPercentBound = 100;
    public DepositPercent(decimal lowLimit, decimal highLimit, decimal percent)
    {
        if (lowLimit < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        if (highLimit < LowBound)
            throw new NotCorrectAmountOfMoneyException();
        if (percent < LowPercentBound || percent > HighPercentBound)
            throw new NotCorrectPercentException();
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Percent = percent;
    }

    public decimal LowLimit { get; }
    public decimal HighLimit { get; }
    public decimal Percent { get; }
}