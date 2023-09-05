using System.Linq;
using TMPro;
using UnityEngine;

public class BalanceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private OperationsCreator _operationsCreator;

    private int _balance;

    public int Balance => _balance;

    public void DoOperation()
    {
        _operationsCreator.CreatePurchaseOperation();
        var operation = _operationsCreator.Operations.LastOrDefault();
        if (operation == null) return;

        var sum = operation.Value;
        switch (operation.OperationType)
        {
            case OperationType.Purchase: _balance -= sum;
                break;
            case OperationType.Add: _balance += sum;
                break;
        }

        _operationsCreator.AddOperationInHistory();
        _balanceText.text = _balance.ToString() + " RUB";
    }
}