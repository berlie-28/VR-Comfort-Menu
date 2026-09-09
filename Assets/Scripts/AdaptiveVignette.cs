using UnityEngine;
using UnityEngine.UI;

// XR Origin ne kadar hızlı dönüyor/hareket ediyorsa vinyeti o kadar koyulaştırır, durunca açılır
public class AdaptiveVignette : MonoBehaviour
{
    public Transform xrOrigin;        // hareketi ölçtüğümüz obje
    public Image vignetteImage;

    [Header("Ayarlar")]
    public float maxAlpha = 0.6f;
    public float turnFactor = 0.01f;   // dönüş hızının etkisi
    public float moveFactor = 0.12f;   // yürüme hızının etkisi
    public float easeIn = 5f;          // koyulaşma hızı
    public float easeOut = 2.5f;       // açılma hızı

    Vector3 lastPos;
    Quaternion lastRot;
    float alpha;

    void OnEnable()
    {
        // referans anını sıfırla ki ilk karede yanlış hız çıkmasın
        if (xrOrigin != null)
        {
            lastPos = xrOrigin.position;
            lastRot = xrOrigin.rotation;
        }
    }

    void OnDisable()
    {
        // kapatılınca vinyet gitsin
        alpha = 0f;
        Apply();
    }

    void Update()
    {
        if (xrOrigin == null || vignetteImage == null) return;

        float turnSpeed = Quaternion.Angle(lastRot, xrOrigin.rotation) / Time.deltaTime;
        float moveSpeed = Vector3.Distance(lastPos, xrOrigin.position) / Time.deltaTime;
        lastPos = xrOrigin.position;
        lastRot = xrOrigin.rotation;

        float target = Mathf.Clamp01(turnSpeed * turnFactor + moveSpeed * moveFactor) * maxAlpha;

        // koyulaşırken hızlı, açılırken yavaş
        float speed = (target > alpha) ? easeIn : easeOut;
        alpha = Mathf.MoveTowards(alpha, target, speed * Time.deltaTime);

        Apply();
    }

    void Apply()
    {
        if (vignetteImage != null)
            vignetteImage.color = new Color(0f, 0f, 0f, alpha);
    }
}