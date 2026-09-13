using UnityEngine;
using UnityEngine.UI;

// vignette gets darker the faster you move and turn, fades out when still
public class AdaptiveVignette : MonoBehaviour
{
    public Transform xrOrigin;        // what we measure movement on
    public Image vignetteImage;

    [Header("Settings")]
    public float maxAlpha = 0.7f;
    public float turnFactor = 0.014f;   // how much turn speed affects it
    public float moveFactor = 0.16f;    // how much walk speed affects it
    public float easeIn = 5f;          // how fast it darkens
    public float easeOut = 2.5f;       // how fast it fades

    Vector3 lastPos;
    Quaternion lastRot;
    float alpha;

    void OnEnable()
    {
        // reset so the first frame doesn't get a wrong speed
        if (xrOrigin != null)
        {
            lastPos = xrOrigin.position;
            lastRot = xrOrigin.rotation;
        }
    }

    void OnDisable()
    {
        // turn off vignette
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

        // darkens fast, fades slowly
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
