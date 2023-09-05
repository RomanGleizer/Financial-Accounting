using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CategoryChanger : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _categories;

    private TMP_Dropdown _operationType;
    private List<TMP_Dropdown.OptionData> _purchaseList;
    private List<TMP_Dropdown.OptionData> _addList;

    private void Start()
    {
        _operationType = GetComponent<TMP_Dropdown>();
        _purchaseList = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Продукты"),
            new TMP_Dropdown.OptionData("Проезд"),
            new TMP_Dropdown.OptionData("Здоровье"),
            new TMP_Dropdown.OptionData("Спорт"),
            new TMP_Dropdown.OptionData("Подарки"),
            new TMP_Dropdown.OptionData("Образование"),
            new TMP_Dropdown.OptionData("Кафе"),
        };
        _addList = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Проценты из банка"),
            new TMP_Dropdown.OptionData("Подарок"),
            new TMP_Dropdown.OptionData("Зарплата"),
            new TMP_Dropdown.OptionData("Доход от брокера")
        };
    }

    public void ChangeCategory()
    {
        switch ((OperationType)_operationType.value)
        {
            case OperationType.Purchase: _categories.options = _purchaseList;
                break;
            case OperationType.Add: _categories.options = _addList;
                break;
        }
    }
}