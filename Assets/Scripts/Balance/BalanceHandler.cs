using System.IO;
using TMPro;
using UnityEngine;

public class BalanceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private OperationsCreator _operationsCreator;
    [SerializeField] private KeyboardOpener _keyboard;

    private double _balance;
    private string _path;

    public double Balance => _balance;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/Balance Data.json";

        if (!File.Exists(_path)) return;

        var data = SaveLoadSystem.GetData<BalanceData>(_path);
        _balance = data.Balance;
        _balanceText.text = $"{_balance} RUB";
    }

    private void LateUpdate()
        => SaveLoadSystem.SaveData(_path, new BalanceData(_balance));

    public void DoOperation()
    {
        _operationsCreator.CreateOperation();
        var operation = _operationsCreator.CurrentOperation;

        if (operation == null) return;
        var sum = operation.Value;
        if (sum <= 0) return;

        switch (operation.OperationType)
        {
            case OperationType.Purchase: _balance -= sum;
                break;
            case OperationType.Add: _balance += sum;
                break;
        }

        _operationsCreator.AddOperationInHistory();
        _balanceText.text = $"{_balance} RUB";
    }
}