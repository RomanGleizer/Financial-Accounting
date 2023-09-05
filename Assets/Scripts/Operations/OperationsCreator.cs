using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class OperationsCreator : MonoBehaviour
{
    [Header("Operation data")]
    [SerializeField] private TMP_Dropdown _operationType;
    [SerializeField] private TMP_InputField _sum;
    [SerializeField] private TMP_Dropdown _categories;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _addOperationMenu;

    [Header("History of operations")]
    [SerializeField] private RectTransform[] _history;

    private List<Operation> _operations;
    private MenuOpener _opener;
    private Operation _currentOperation;
    private int _operationsInHistoryCount;

    public List<Operation> Operations => _operations;

    public TMP_Dropdown OperationType => _operationType;

    private void Start()
    {
        _operations = new List<Operation>();
        _opener = GetComponent<MenuOpener>();
    }

    public void CreatePurchaseOperation()
    {
        if (!int.TryParse(_sum.text, out _)) return;

        var operation = new Operation(int.Parse(_sum.text), (OperationType)_operationType.value);
        _sum.text = string.Empty;
        _currentOperation = operation;
        _operations.Add(operation);

        _opener.OpenMenu(_mainMenu);
        _opener.CloseMenu(_addOperationMenu);
    }

    public void AddOperationInHistory()
    {
        if (_operationsInHistoryCount == 3) _operationsInHistoryCount = 0;
        var operationType = _history[_operationsInHistoryCount].GetChild(1);
        var operationSum = _history[_operationsInHistoryCount].GetChild(2);

        if (operationType.TryGetComponent(out TMP_Text type))
        {
            print(_operationType.value);

            var text = _operationType.options[_operationType.value].text + $" ({_categories.options[_categories.value].text})";
            type.text = text;
        }

        if (operationSum.TryGetComponent(out TMP_Text sum))
            sum.text = _currentOperation.Value.ToString() + " RUB";

        _operationsInHistoryCount++;
    }
}