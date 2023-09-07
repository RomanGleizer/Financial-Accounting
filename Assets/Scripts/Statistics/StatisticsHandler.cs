using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    [SerializeField] private Transform _purchaseStatistics;
    [SerializeField] private Transform _addStatistics;

    private OperationsCreator _operationsCreator;
    private Dictionary<string, int> _operations;
    private TMP_Dropdown _operationType;

    private void Awake()
    {
        _operationType = GetComponent<TMP_Dropdown>();
        _operations = new Dictionary<string, int>();
        _operationsCreator = GetComponent<OperationsCreator>();
    }

    public void ChangeStatistics()
    {
        var operation = _operationsCreator.CurrentOperation;
        var sum = GetTotalSum(operation);
        var name = GetOperationName(operation);

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

    private void SwtichOperations(bool purchaseStatus, bool addStatus)
    {
        _purchaseStatistics.gameObject.SetActive(purchaseStatus);
        _addStatistics.gameObject.SetActive(addStatus);
    }

    private bool IsOperationWasPurchase(Operation operation) 
        => operation.OperationType is OperationType.Purchase;

    private Transform GetTotalSum(Operation operation)
    {
        var index = IsOperationWasPurchase(operation)
            ? _operationsCreator.PurchaseOperationsData[_operationsCreator.PurchaseType]
            : _operationsCreator.AddOperationsData[_operationsCreator.AddType];

        var sumIndex = index + 1;
        return IsOperationWasPurchase(operation)
            ? _purchaseStatistics.GetChild(sumIndex)
            : _addStatistics.GetChild(sumIndex);
    }

    private string GetOperationName(Operation operation)
    {
        return IsOperationWasPurchase(operation)
            ? _operationsCreator.PurchaseCategories.options[_operationsCreator.PurchaseCategories.value].text
            : _operationsCreator.AddCategories.options[_operationsCreator.AddCategories.value].text;
    }
}