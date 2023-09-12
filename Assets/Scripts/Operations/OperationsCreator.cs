using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class OperationsCreator : MonoBehaviour
{
    [Header("Operation data")]
    [SerializeField] private TMP_Dropdown _operationType;
    [SerializeField] private TMP_InputField _sum;
    [SerializeField] private TMP_Dropdown _purchaseCategories;
    [SerializeField] private TMP_Dropdown _addCategories;

    [Header("History of operations")]
    [SerializeField] private RectTransform[] _history;

    private short _operationsInHistoryCount;
    private string _path;
    private string _purchaseType;
    private string _addType;
    private Operation _currentOperation;
    private Dictionary<string, int> _purchaseOperationsData;
    private Dictionary<string, int> _addOperationsData;
    private const int MaxHistoryLength = 5;

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
        _path = Application.persistentDataPath + "/Operations Data.json";
        _purchaseOperationsData = new Dictionary<string, int>();
        _addOperationsData = new Dictionary<string, int>();

        ProcessOperationsData(_purchaseOperationsData, _addOperationsData);
        ProcessHistory(_path);
    }

    private void LateUpdate()
    {
        string[] sums = new string[MaxHistoryLength], names = new string[MaxHistoryLength];
        for (int i = 0; i < sums.Length; i++)
        {
            names[i] = _history[i].GetChild(1).GetComponent<TextMeshProUGUI>().text;
            sums[i] = _history[i].GetChild(2).GetComponent<TextMeshProUGUI>().text;
        }

        SaveLoadSystem.SaveData(_path, new HistoryOfOperationsData(_operationsInHistoryCount, sums, names));
    }

    public void CreateOperation()
    {
        if (!int.TryParse(_sum.text, out _)) return;

        var sum = int.Parse(_sum.text);
        var operation = new Operation(sum, (OperationType)_operationType.value);
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
    }

    public void AddOperationInHistory()
    {
        if (_operationsInHistoryCount == 5) _operationsInHistoryCount = 0;
        ChangeOperationsText(_operationType.options[_operationType.value].text, $"{_currentOperation.Value} RUB");
    }

    private void ProcessHistory(string path)
    {
        if (!File.Exists(path)) return;

        var data = SaveLoadSystem.GetData<HistoryOfOperationsData>(path);

        for (int i = 0; i < _history.Length; i++)
            ChangeOperationsText(data.OperationNames[i], data.Sums[i]);
        _operationsInHistoryCount = data.OperationsCount;
    }

    private void ChangeOperationsText(string operationText, string sumText)
    {
        var operationType = _history[_operationsInHistoryCount].GetChild(1);
        var operationSum = _history[_operationsInHistoryCount].GetChild(2);

        if (operationType.TryGetComponent(out TMP_Text type)) type.text = operationText;
        if (operationSum.TryGetComponent(out TMP_Text sum)) sum.text = sumText;
        _operationsInHistoryCount++;
    }

    private void ProcessOperationsData(Dictionary<string, int> purchaseOperations, Dictionary<string, int> addOperations)
    {
        var index = 0;
        for (int i = 0; i < _purchaseCategories.options.Count; i++)
            purchaseOperations.Add(_purchaseCategories.options[i].text, index += 2);

        index = 0;
        for (int i = 0; i < _addCategories.options.Count; i++)
            addOperations.Add(_addCategories.options[i].text, index += 2);
    }
}