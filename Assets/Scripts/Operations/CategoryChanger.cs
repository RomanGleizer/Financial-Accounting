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
            new TMP_Dropdown.OptionData("��������"),
            new TMP_Dropdown.OptionData("������"),
            new TMP_Dropdown.OptionData("��������"),
            new TMP_Dropdown.OptionData("�����"),
            new TMP_Dropdown.OptionData("�������"),
            new TMP_Dropdown.OptionData("�����������"),
            new TMP_Dropdown.OptionData("����"),
        };
        _addList = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("�������� �� �����"),
            new TMP_Dropdown.OptionData("�������"),
            new TMP_Dropdown.OptionData("��������"),
            new TMP_Dropdown.OptionData("����� �� �������")
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