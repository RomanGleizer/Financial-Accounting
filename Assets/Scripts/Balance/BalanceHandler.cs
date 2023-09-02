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
        _operationsCreator.CreateOperation();
        _balance -= _operationsCreator.Operations.LastOrDefault().Key.Value;
        _balanceText.text = _balance.ToString() + " RUB";
    }
}