using System;
using System.Collections.Generic;

[Serializable]
public class StatisticsData
{
    public List<string> PurchaseSums;
    public List<string> AddSums;

    public StatisticsData(List<string> purchaseSums, List<string> addSums)
    {
        PurchaseSums = purchaseSums;
        AddSums = addSums;
    }
}
