using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
        _path = Application.persistentDataPath + "/Statistics Data.json";
        _operations = new Dictionary<string, int>();
        _operationType = GetComponent<TMP_Dropdown>();
        _operationsCreator = GetComponent<OperationsCreator>();

        ProcessStatistics(_path);
    }

    private void LateUpdate()
    {
        var purchaseSums = SaveSum(_purchaseStatistics).ToList();
        var addSums = SaveSum(_addStatistics).ToList();
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
        LoadStatistics(_purchaseStatistics, data.PurchaseSums);
        LoadStatistics(_addStatistics, data.AddSums);
    }

    private IEnumerable<int> SaveSum(Transform statistics)
    {
        for (int i = 1; i < statistics.childCount; i += 2)
        {
            var text = statistics.GetChild(i).GetComponent<TextMeshProUGUI>().text;
            var possibleSum = new string(text.TakeWhile(e => e != ' ').ToArray());
            if (int.TryParse(possibleSum, out _))
                yield return int.Parse(possibleSum);
        }
    }

    private void LoadStatistics(Transform statistics, List<int> sums)
    {
        var index = 0;
        for (int i = 0; i < sums.Count; i += 2)
        {
            var category = new string(statistics.GetChild(i).GetComponent<TextMeshProUGUI>().text.TakeWhile(e => e != ':').ToArray());
            _operations[category] = sums[index++];
        }

        index = 0;
        for (int i = 1; i <= sums.Count; i += 2)
            statistics.GetChild(i).GetComponent<TextMeshProUGUI>().text = $"{sums[index++]} RUB";
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