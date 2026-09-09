using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

// menüdeki ayarları oyuna bağlayan script, seçimler PlayerPrefs'e kaydediliyor
public class ComfortMenu : MonoBehaviour
{
    [Header("UI")]
    public Toggle teleportToggle;
    public Toggle snapTurnToggle;
    public Toggle vignetteToggle;
    public Slider speedSlider;

    [Header("Sahnedeki hazır componentler")]
    // bu ikisi starter assets'ten, kolların ne yaptığını yönetiyor
    public ControllerInputActionManager leftHand;
    public ControllerInputActionManager rightHand;
    public ContinuousMoveProvider moveProvider;
    public TeleportationProvider teleportProvider;
    public AdaptiveVignette adaptiveVignette;   // dönerken/yürürken vinyeti ayarlayan script

    void Start()
    {
        // kayıtlı ayarları oku
        teleportToggle.isOn = PlayerPrefs.GetInt("teleport", 0) == 1;
        snapTurnToggle.isOn = PlayerPrefs.GetInt("snapTurn", 1) == 1;
        vignetteToggle.isOn = PlayerPrefs.GetInt("vignette", 1) == 1;
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1f);

        teleportToggle.onValueChanged.AddListener(SetMovement);
        snapTurnToggle.onValueChanged.AddListener(SetTurn);
        vignetteToggle.onValueChanged.AddListener(SetVignette);
        speedSlider.onValueChanged.AddListener(SetSpeed);

        SetMovement(teleportToggle.isOn);
        SetTurn(snapTurnToggle.isOn);
        SetVignette(vignetteToggle.isOn);
        SetSpeed(speedSlider.value);
    }

    // teleport açıksa ışınlanma açık ve yürüme kapalı, kapalıysa tam tersi
    void SetMovement(bool teleport)
    {
        if (leftHand != null) leftHand.smoothMotionEnabled = !teleport;
        if (teleportProvider != null) teleportProvider.enabled = teleport;
        PlayerPrefs.SetInt("teleport", teleport ? 1 : 0);
    }

    // snap açıksa kademeli dönüş, kapalıysa akıcı. sol kola da veriyoruz
    void SetTurn(bool snap)
    {
        if (rightHand != null) rightHand.smoothTurnEnabled = !snap;
        if (leftHand != null) leftHand.smoothTurnEnabled = !snap;
        PlayerPrefs.SetInt("snapTurn", snap ? 1 : 0);
    }

    void SetVignette(bool on)
    {
        if (adaptiveVignette != null) adaptiveVignette.enabled = on;
        PlayerPrefs.SetInt("vignette", on ? 1 : 0);
    }

    void SetSpeed(float value)
    {
        if (moveProvider != null) moveProvider.moveSpeed = value;
        PlayerPrefs.SetFloat("speed", value);
    }
}
