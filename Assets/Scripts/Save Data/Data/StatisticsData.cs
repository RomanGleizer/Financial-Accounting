using System;
using System.Collections.Generic;

[Serializable]
public class StatisticsData
{
    public List<int> PurchaseSums;
    public List<int> AddSums;

    public StatisticsData(List<int> purchaseSums, List<int> addSums)
    {
        PurchaseSums = purchaseSums;
        AddSums = addSums;
    }
}