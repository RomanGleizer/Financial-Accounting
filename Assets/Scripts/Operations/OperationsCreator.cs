using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class OperationsCreator : MonoBehaviour
{
    [Header("Operation data")]
    [SerializeField] private TMP_Dropdown _operationType;
    [SerializeField] private TMP_InputField _sum;
    [SerializeField] private TMP_Dropdown _purchaseCategories;
    [SerializeField] private TMP_Dropdown _addCategories;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _addOperationMenu;

    [Header("History of operations")]
    [SerializeField] private RectTransform[] _history;

    private short _operationsInHistoryCount;
    private string _purchaseType;
    private string _addType;
    private Operation _currentOperation;
    private MenuOpener _opener;
    private Dictionary<string, int> _purchaseOperationsData;
    private Dictionary<string, int> _addOperationsData;

    public string PurchaseType => _purchaseType;

    public string AddType => _addType;

    public TMP_Dropdown PurchaseCategories => _purchaseCategories;

    public TMP_Dropdown AddCategories => _addCategories;

    public TMP_Dropdown OperationType => _operationType;

    public Operation CurrentOperation => _currentOperation;

    public Dictionary<string, int> PurchaseOperationsData => _purchaseOperationsData;

    public Dictionary<string, int> AddOperationsData => _addOperationsData;

    private void Awake()
    {
        _purchaseOperationsData = new Dictionary<string, int>();
        _addOperationsData = new Dictionary<string, int>();

        _opener = GetComponent<MenuOpener>();

        for (int i = 0; i < _purchaseCategories.options.Count; i += 2)
            _purchaseOperationsData.Add(_purchaseCategories.options[i].text, i);
        for (int i = 0; i < _addCategories.options.Count; i += 2)
            _addOperationsData.Add(_addCategories.options[i].text, i);
    }

    public void CreateOperation()
    {
        if (!int.TryParse(_sum.text, out _)) return;

        var operation = new Operation(int.Parse(_sum.text), (OperationType)_operationType.value);
        if (operation.Value < 0) return;

        _sum.text = string.Empty;
        _currentOperation = operation;

        switch (_currentOperation.OperationType)
        {
            case global::OperationType.Purchase:
                _purchaseType = _purchaseCategories.options[_purchaseCategories.value].text;
                break;
            case global::OperationType.Add:
                _addType = _addCategories.options[_addCategories.value].text;
                break;
        }

        _opener.OpenMenu(_mainMenu);
        _opener.CloseMenu(_addOperationMenu);
    }

    public void AddOperationInHistory()
    {
        if (_operationsInHistoryCount == 5) _operationsInHistoryCount = 0;
        var operationType = _history[_operationsInHistoryCount].GetChild(1);
        var operationSum = _history[_operationsInHistoryCount].GetChild(2);

        if (operationType.TryGetComponent(out TMP_Text type))
        {
            var text = _operationType.options[_operationType.value].text;
            type.text = text;
        }

        if (operationSum.TryGetComponent(out TMP_Text sum))
            sum.text = $"{_currentOperation.Value} RUB";

        _operationsInHistoryCount++;
    }
}