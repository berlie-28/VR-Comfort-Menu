using UnityEngine;
using UnityEngine.InputSystem;

// M tuşuyla menü panelini açıp kapatır
public class MenuToggle : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            if (panel != null) panel.SetActive(!panel.activeSelf);
        }
    }
}
