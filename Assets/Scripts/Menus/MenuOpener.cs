using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public void OpenMenu(GameObject menu) => SwitchMenu(menu, true);

    public void CloseMenu(GameObject menu) => SwitchMenu(menu, false);

    private void SwitchMenu(GameObject menu, bool state) => menu.SetActive(state);
}