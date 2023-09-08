using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    [SerializeField] private Transform _purchaseStatistics;
    [SerializeField] private Transform _addStatistics;

    private string _path;
    private OperationsCreator _operationsCreator;
    private Dictionary<string, int> _operations;
    private TMP_Dropdown _operationType;

    private void Awake()
    {
        _operationType = GetComponent<TMP_Dropdown>();
        _operations = new Dictionary<string, int>();
        _operationsCreator = GetComponent<OperationsCreator>();
        _path = Application.dataPath + "/Statistics Data.json";

        ProcessStatistics(_path);
    }

    private void OnApplicationQuit()
    {
        List<string> purchaseSums = new List<string>(), addSums = new List<string>();

        for (int i = 1; i < _purchaseStatistics.childCount; i += 2)
            purchaseSums.Add(_purchaseStatistics.GetChild(i).GetComponent<TextMeshProUGUI>().text);
        for (int i = 1; i < _addStatistics.childCount; i += 2)
            addSums.Add(_addStatistics.GetChild(i).GetComponent<TextMeshProUGUI>().text);

        SaveLoadSystem.SaveData(_path, new StatisticsData(purchaseSums, addSums));
    }
    public void ChangeStatistics()
    {
        var operation = _operationsCreator.CurrentOperation;
        var sum = GetTotalSum(operation);
        var name = GetOperationName(operation);

        if (operation is null) return;

        if (!_operations.ContainsKey(name)) _operations.Add(name, 0);
        _operations[name] += operation.Value;

        if (sum.TryGetComponent(out TextMeshProUGUI sumText))
            sumText.text = $"{_operations[name]} RUB";
    }

    public void ChangeOperationsList()
    {
        switch (_operationType.value)
        {
            case 0:
                SwtichOperations(true, false);
                break;
            case 1:
                SwtichOperations(false, true);
                break;
        }
    }

    private void ProcessStatistics(string path)
    {
        if (!File.Exists(path)) return;

        var data = SaveLoadSystem.GetData<StatisticsData>(path);

        var index = 0;
        for (int i = 1; i < _purchaseStatistics.childCount; i += 2)
            _purchaseStatistics.GetChild(i).GetComponent<TextMeshProUGUI>().text = data.PurchaseSums[index++];

        index = 0;
        for (int i = 1; i < _addStatistics.childCount; i += 2)
            _addStatistics.GetChild(i).GetComponent<TextMeshProUGUI>().text = data.AddSums[index++];
    }

    private void SwtichOperations(bool purchaseStatus, bool addStatus)
    {
        _purchaseStatistics.gameObject.SetActive(purchaseStatus);
        _addStatistics.gameObject.SetActive(addStatus);
    }

    private Transform GetTotalSum(Operation operation)
    {
        if (operation is null) return null;

        var index = IsOperationWasPurchase(operation)
            ? _operationsCreator.PurchaseOperationsData[_operationsCreator.PurchaseType]
            : _operationsCreator.AddOperationsData[_operationsCreator.AddType];

        var sceneIndex = index - 1;
        return IsOperationWasPurchase(operation)
            ? _purchaseStatistics.GetChild(sceneIndex)
            : _addStatistics.GetChild(sceneIndex);
    }

    private bool IsOperationWasPurchase(Operation operation)
        => operation.OperationType is OperationType.Purchase;

    private string GetOperationName(Operation operation)
    {
        if (operation is null) return null;

        return IsOperationWasPurchase(operation)
            ? _operationsCreator.PurchaseCategories.options[_operationsCreator.PurchaseCategories.value].text
            : _operationsCreator.AddCategories.options[_operationsCreator.AddCategories.value].text;
    }
}