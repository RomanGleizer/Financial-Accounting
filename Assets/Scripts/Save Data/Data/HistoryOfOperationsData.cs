using System;
using UnityEngine;

[Serializable]
public class HistoryOfOperationsData
{
    public short OperationsCount;
    public string[] Sums;
    public string[] OperationNames;

    public HistoryOfOperationsData(short count, string[] sums, string[] operationsNames)
    {
        OperationsCount = count;
        Sums = sums;
        OperationNames = operationsNames;
    }
}