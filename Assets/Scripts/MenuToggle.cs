using UnityEngine;
using UnityEngine.InputSystem;

// M tuşuyla menüyü açar/kapatır, açılırken kafanın baktığı yöne göre önüne koyar
public class MenuToggle : MonoBehaviour
{
    public GameObject panel;      // açılıp kapanan obje (MenuCanvas)
    public Transform head;        // Main Camera, önüne koymak için lazım
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

        // sadece yatay yönü al, menü yatık değil dik dursun
        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();

        panel.transform.position = head.position + forward * distance;
        panel.transform.rotation = Quaternion.LookRotation(forward);
    }
}
