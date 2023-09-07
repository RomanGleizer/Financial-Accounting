using System.Linq;
using TMPro;
using UnityEngine;

public class BalanceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private OperationsCreator _operationsCreator;
    [SerializeField] private KeyboardOpener _balanceKeyboard;

    private int _balance;

    public int Balance => _balance;

    private void Update()
    {
        if (_balanceKeyboard.Keyboard is null) return;
        if (_balanceKeyboard.Keyboard.status is TouchScreenKeyboard.Status.Done) ChangeBalanceValue();
    }

    public void DoOperation()
    {
        _operationsCreator.CreateOperation();
        var operation = _operationsCreator.CurrentOperation;
        if (operation == null) return;

        var sum = operation.Value;
        switch (operation.OperationType)
        {
            case OperationType.Purchase: _balance -= sum;
                break;
            case OperationType.Add: if (sum >= 0) _balance += sum;
                break;
        }

        _operationsCreator.AddOperationInHistory();
        _balanceText.text = _balance.ToString() + " RUB";
    }

    private void ChangeBalanceValue()
    {
        if (!int.TryParse(_balanceKeyboard.Keyboard.text, out _)) return;

        _balance = int.Parse(_balanceKeyboard.Keyboard.text);
        _balanceText.text = _balanceKeyboard.Keyboard.text + " RUB";
    }
}