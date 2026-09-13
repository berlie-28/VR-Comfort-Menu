using UnityEngine;
using UnityEngine.InputSystem;

// M opens and closes the menu, places it in front of you when opened
public class MenuToggle : MonoBehaviour
{
    public GameObject panel;      // opens and closes this (MenuCanvas)
    public Transform head;        // Main Camera, used to place the menu
    public float distance = 1.4f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            bool opening = !panel.activeSelf;
            panel.SetActive(opening);
            if (opening) PlaceInFrontOfHead();
        }
    }

    void PlaceInFrontOfHead()
    {
        if (head == null) return;

        // horizontal only, so the menu stays upright
        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();

        panel.transform.position = head.position + forward * distance;
        panel.transform.rotation = Quaternion.LookRotation(forward);
    }
}
