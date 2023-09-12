using UnityEngine;

public class KeyboardOpener : MonoBehaviour
{
    private TouchScreenKeyboard _operationKeyboard;

    public void OpenKeyboard()
    {
        _operationKeyboard = TouchScreenKeyboard.Open("");
        Invoke(nameof(HideInputField), 0.1f);   
    }

    private void HideInputField()
    {
        TouchScreenKeyboard.hideInput = true;
    }
}