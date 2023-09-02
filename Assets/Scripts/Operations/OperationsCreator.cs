using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OperationsCreator : MonoBehaviour
{
    [Header("Operation data")]
    [SerializeField] private TMP_Dropdown _operationType;
    [SerializeField] private TMP_InputField _sum;
    [SerializeField] private TMP_Dropdown _categories;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _addOperationMenu;

    private Dictionary<Operation, bool> _operations;
    private MenuOpener _opener;

    public IDictionary<Operation, bool> Operations => _operations;

    private void Start()
    {
        _operations = new Dictionary<Operation, bool>();
        _opener = GetComponent<MenuOpener>();
    }

    public void CreateOperation()
    {
        if (!int.TryParse(_sum.text, out _)) return;

        var operation = new Operation(int.Parse(_sum.text), (OperationType)_operationType.value);
        _operations[operation] = false;

        _opener.OpenMenu(_mainMenu);
        _opener.CloseMenu(_addOperationMenu);
    }
}