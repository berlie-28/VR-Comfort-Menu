# VR Comfort System

Unity'de yaptığım bir VR konfor ayarları sistemi. Amaç sadece aç/kapa düğmeleri koymak değildi, gerçekten baş dönmesini azaltan bir şey kurmaktı: teleport/dönüş seçenekleri, hıza göre kendini ayarlayan bir vinyet, hız kontrolü.

*(For the English description: [README.md](README.md))*

## Proje hakkında

[VR Escape Room](https://github.com/berlie-28/VR-Escape-Room)'dan sonraki ikinci portföy projem. Amacım sadece bir ayar menüsü yapmak değildi. VR'da insanların çoğu mide bulanması yaşıyor, buna "vection" deniyor: göz hareketi görüyor ama iç kulak hiçbir şey hissetmiyor, ikisi uyuşmayınca beyin bulanıyor. Ben de buna gerçekten çözüm olan küçük bir sistem kurmak istedim, farkı hissedilebilsin diye küçük bir demo koridoru da ekledim.

## Özellikler

- Hareket şekli: teleport ya da sürekli yürüme, oyun içinde değiştirilebiliyor (ikisi asla aynı anda açık olmuyor)
- Dönüş şekli: snap turn (anlık kademeli dönüş) ya da smooth turn
- Adaptif konfor vinyeti: ne kadar hızlı dönüyorsan/yürüyorsan ekranın kenarları o kadar kararıyor, durunca yumuşakça açılıyor — sabit aç/kapa değil
- Hareket hızı slider'ı
- Ayarlar PlayerPrefs'e kaydediliyor, oyunu tekrar açınca aynı kalıyor
- Ekrana yapışık değil, tuşa basınca önüne "çağrılan" bir menü, bıraktığın yerde duruyor
- 90 derece dönüşlü, L şeklinde küçük bir demo koridoru

## Bu ayarlar neden işe yarıyor

- **Teleport** sürekli hareketi tamamen kaldırıyor, yani "gözüm hareket görüyor ama vücudum hissetmiyor" çelişkisi (vection) hiç oluşmuyor.
- **Snap turn** aynısını dönüş için yapıyor: görüş açın kaymıyor, anlık değişiyor, göz ile iç kulak arasında uyuşmazlık çıkmıyor.
- **Vinyet** hareket sırasında çevresel görüşü daraltıyor, çünkü vection en çok orada hissediliyor.
- **Düşük hız** basitçe daha az görsel hareket demek, o da daha az vection demek.

## Kontroller (Editor testi)

VR gözlüğüm yok, macOS kullanıyorum. O yüzden gerçek controller yerine XR Interaction Toolkit'in hazır **XR Device Simulator**'ıyla test ettim:

- **T**: sol controller'ı kontrol et (hareket / ışınlanma)
- **Y**: sağ controller'ı kontrol et (dönüş)
- **WASD**: hangi controller aktifse ona göre hareket/dönüş
- **M**: konfor menüsünü aç/kapat
- **F1**: simülatörün kendi kontrol rehberi

Gerçek XR Interaction Toolkit + OpenXR kurulumu projede hazır, sadece test edecek bir gözlüğüm olmadı.

## Kullanılan teknolojiler

- Unity 6 (6000.3.10f1)
- Universal Render Pipeline (URP)
- XR Interaction Toolkit 3.3.2
- OpenXR
- Unity Input System
- TextMeshPro

## Yapay zeka kullanımı hakkında not

Script yazımında ve XR Interaction Toolkit'in kendi başıma çözemeyeceğim bazı iç mantığına inmekte yapay zekadan yardım aldım. Unity Editor tarafı ise çoğunlukla bendim: component ekleme, referans bağlama, obje yerleştirme bana aitti. Her özelliği Play modunda kendim test ettim, bir şey çalışmayınca ne gördüğümü anlatıp geri bildirim verdim. Projenin kapsamı ve bu ayarların neden seçildiği de tek seferlik bir öneriden değil, karşılıklı konuşmadan çıktı.

## Karşılaştığım bir sorun

Menüde dönüş ayarını değiştirince bazen hiçbir şey olmuyordu, üstelik hata da vermiyordu. Sonra anladım ki XR Interaction Toolkit'in örnek script'lerinden biri hangi kolun hareket hangi kolun dönüş yapacağını kendisi ayarlıyor, benim değiştirdiğim şeyi tekrar eski haline getiriyor. Bunu paketin kaynak koduna bakınca anladım, Inspector'dan görünmüyordu.

## Bilinen kısıtlamalar

- Gerçek VR donanımında hiç test edilmedi, sadece yukarıdaki simülatörle.
- macOS'ta yapıldı ve test edildi.
- Demo koridoru bilerek minimal, sadece ayarların farkını hissettirecek kadar, tam bir seviye değil.

## Projeyi çalıştırma

1. Projeyi Unity **6000.3.10f1** (ya da yakın bir sürüm) ile aç.
2. `Assets/Scenes/SampleScene.unity`'yi aç.
3. Play'e bas. Yukarıdaki kontrollerle hareket et, dön, menüyü aç.
