using UnityEngine;

public class KeyboardOpener : MonoBehaviour
{
    private TouchScreenKeyboard _keyboard;

    public TouchScreenKeyboard Keyboard => _keyboard;

    public void OpenKeyboard()
    {
        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        Invoke(nameof(HideInputField), 0.1f);   
    }

    private void HideInputField()
    {
        TouchScreenKeyboard.hideInput = true;
    }
}