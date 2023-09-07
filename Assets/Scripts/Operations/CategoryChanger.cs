using TMPro;
using UnityEngine;

public class CategoryChanger : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _purchaseCategories;
    [SerializeField] private TMP_Dropdown _addCategories;

    private TMP_Dropdown _operationType;

    private void Start()
    {
        _operationType = GetComponent<TMP_Dropdown>();
    }

    public void ChangeCategory()
    {
        switch ((OperationType)_operationType.value)
        {
            case OperationType.Purchase: 
                SwitchCategories(true, false);
                break;
            case OperationType.Add:
                SwitchCategories(false, true);
                break;
        }
    }

    private void SwitchCategories(bool firstStatus, bool secondStatus)
    {
        _purchaseCategories.gameObject.SetActive(firstStatus);
        _addCategories.gameObject.SetActive(secondStatus);
    }
}