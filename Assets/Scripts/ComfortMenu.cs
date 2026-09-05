using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

// Menüdeki ayarları sahneye uygulayan script.
// Oyuncu hareket şeklini, dönüş şeklini, vinyeti ve hızı buradan seçiyor.
// Seçtikleri PlayerPrefs'e kaydediliyor, oyunu tekrar açınca aynı ayarlarla başlıyor.
public class ComfortMenu : MonoBehaviour
{
    [Header("UI")]
    public Toggle teleportToggle;   // açıksa teleport, kapalıysa normal yürüme
    public Toggle snapTurnToggle;   // açıksa snap dönüş, kapalıysa yumuşak dönüş
    public Toggle vignetteToggle;   // açıksa kenar karartması var
    public Slider speedSlider;      // yürüme hızı

    [Header("Sahnedeki hazır componentler")]
    public TeleportationProvider teleportProvider;
    public ContinuousMoveProvider moveProvider;
    public SnapTurnProvider snapTurnProvider;
    public ContinuousTurnProvider smoothTurnProvider;
    public Image vignetteImage;     // ekranı kaplayan vinyet resmi

    void Start()
    {
        // Daha önce kaydettiğimiz ayarları geri yüklüyoruz.
        // GetInt'in ikinci sayısı: kayıt yoksa kullanılacak varsayılan değer.
        teleportToggle.isOn = PlayerPrefs.GetInt("teleport", 0) == 1;
        snapTurnToggle.isOn = PlayerPrefs.GetInt("snapTurn", 1) == 1;
        vignetteToggle.isOn = PlayerPrefs.GetInt("vignette", 1) == 1;
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1f);

        // Oyuncu bir toggle veya slider'a dokununca ilgili fonksiyon çalışsın diye bağlıyoruz.
        teleportToggle.onValueChanged.AddListener(SetMovement);
        snapTurnToggle.onValueChanged.AddListener(SetTurn);
        vignetteToggle.onValueChanged.AddListener(SetVignette);
        speedSlider.onValueChanged.AddListener(SetSpeed);

        // Açılışta kayıtlı ayarları bir kere sahneye uyguluyoruz ki her şey doğru başlasın.
        SetMovement(teleportToggle.isOn);
        SetTurn(snapTurnToggle.isOn);
        SetVignette(vignetteToggle.isOn);
        SetSpeed(speedSlider.value);
    }

    // Teleport açıksa ışınlanmayı aç, yürümeyi kapat. Kapalıysa tam tersi.
    void SetMovement(bool teleport)
    {
        if (teleportProvider != null) teleportProvider.enabled = teleport;
        if (moveProvider != null) moveProvider.enabled = !teleport;
        PlayerPrefs.SetInt("teleport", teleport ? 1 : 0);
    }

    // Aynı mantık dönüş için: ya snap ya yumuşak, ikisi aynı anda açık kalmasın.
    void SetTurn(bool snap)
    {
        if (snapTurnProvider != null) snapTurnProvider.enabled = snap;
        if (smoothTurnProvider != null) smoothTurnProvider.enabled = !snap;
        PlayerPrefs.SetInt("snapTurn", snap ? 1 : 0);
    }

    // Vinyet resminin saydamlığını ayarlıyoruz.
    // Açıkken kenarlar hafif kararır, kapalıyken tamamen saydam yani hiç görünmez.
    void SetVignette(bool on)
    {
        if (vignetteImage != null)
            vignetteImage.color = new Color(0f, 0f, 0f, on ? 0.55f : 0f);
        PlayerPrefs.SetInt("vignette", on ? 1 : 0);
    }

    // Slider'ın değerini direkt yürüme hızına yazıyoruz.
    void SetSpeed(float value)
    {
        if (moveProvider != null) moveProvider.moveSpeed = value;
        PlayerPrefs.SetFloat("speed", value);
    }
}
