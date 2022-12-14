# Artificial-Neuron




Makine Öğrenmesi alanında kullanılan ve derin öğrenme alanının temelini de oluşturan Yapay
Sinir Ağları (Artificial Neural Networks - ANN) konusundaki en temel yapılar Yapay Sinir
Hücreleridir (Artificial Neuron). ANN’ler sınıflandırma, kümeleme ve tahminleme gibi birçok
problemin çözümünde kullanılırlar.

Yapay sinir hücresinin yapısı ve örnek bir hesaplama işlemi Şekil 3’te gösterilmektedir.
Şekildeki Algılayıcı (Perceptron) modelinin veya nöronun 4 adet girdisi (x) ve 1 adet çıktısı (y)
bulunmaktadır. 

![image](https://user-images.githubusercontent.com/109876399/193882134-e30fb12d-c670-4384-9f21-00a8a7f47f61.png)

Toplama İşlevi, girdilerle ağırlıkların çarpımları toplamının alınması şeklinde gerçekleştirilir:

![image](https://user-images.githubusercontent.com/109876399/193882300-c4045a5e-1210-4981-941c-0f05ccd21c63.png)

Gözetimli Öğrenmede (Supervised Learning), girdilerle beraber olması gereken çıktı değerleri
(target) verilir / sistem tarafından sağlanır. Elimizde, toplamları pozitif olan iki adet sayıyı 1,
negatif olanları ise -1 olarak sınıflandırmamızı gerektiren bir problemimiz olsun. Toplamları 0
olan sayıları hariç tutuyoruz. Ağın eğitiminde kullanılmak üzere Eğitim (training) verileri
üretelim (Tablo 2): 

![image](https://user-images.githubusercontent.com/109876399/193882402-3c394068-9a37-4cd5-a22b-56736e80df2b.png)

**Ağın eğitimi işleminde kullanılan Öğrenme Kuralının aşağıdaki gibi verildiğini varsayın:**
- Ağın ürettiği çıktı (output), olması gereken değerden (beklenen) yani target değerinden
farklı ise ağırlıkları λ*(t-o)*xi kadar arttır (t:target, o:output, λ:öğrenme katsayısı olmak
üzere): Yani wi = wi + λ*(t-o)*xi .
- Output ve target değerleri aynı ise ağırlıklarda değişiklik yapma


**a) Bir Neuron (Sinir Hücresi) sınıfı** oluşturunuz. Girdiler ve ağırlıkları tutmak için
uygun veri yapılarını tercih ediniz. Hesaplamaları ve gerekli işlemleri yapan
metotları yazınız. Ağırlıklar için en başta [-1, 1] aralığında rastgele double değerler
üretiniz.

**b) Eğitim:** Tablo 1’deki tüm girdi değerlerini 10’a bölerek oluşan veri seti üzerinde eğitim
işleminizi gerçekleştiriniz. λ = 0.05 olarak alabilirsiniz. 10 epok ve 100 epok (epoch)
sonunda yönteminizin veri seti üzerindeki doğruluk (accuracy) değerini hesaplayıp
yazdırınız. Bir epok, tüm eğitim verilerinin sisteme bir kere sıra ile verilerek ağırlıkların
değiştirilmesi işlemidir. Doğruluk değeri Doğru olarak sınıflandırılan örnek (veri) sayısı
/ toplam örnek sayısıdır. Elinizdeki 8 verinin 5 tanesi doğru olarak sınıflandırıldıysa
doğruluk değeri acc = 5/8 = %62.5’tir

![image](https://user-images.githubusercontent.com/109876399/193884503-7b906333-ce60-4493-9dba-dab7a75f682c.png)

![image](https://user-images.githubusercontent.com/109876399/193883989-9a3a8121-999f-4777-a423-ebc1373f141b.png)
----------------------------
**c) Test:** Eğitim verileri dışında farklı 5 tane test verisi oluşturarak yönteminizin
doğruluğunu test ediniz. Test verilerini ve başarı değerini rapora ekleyiniz. Başarı
değerini artırabilmek için neler yapabileceğinizi araştırınız. 

![image](https://user-images.githubusercontent.com/109876399/193885312-ba997196-cdb1-457d-8e19-d6f0e3314095.png)

Yapay sinir ağlarının temel özelliği ve görevi, veri setindeki yapıyı öğrenmek ve istenilen göreve göre genelleştirmeler yapmasıdır. Bunu yapabilmesi için eğitim veri setinin üstünde çalışarak ağırlıkları belirlemesidir. Böylece her bir gözlemle ağırlıklar değişerek zamanla daha güclü ve sapması azalan bir yapıya dönüşecektir. Böylelikle çıktı olarak verdiği tahminde ya da yapıda genelleştirme yeteneği daha güclü olacaktır. Sonuç olarak epok sayısının arttırılması ve verileri arttırmak başarı değerini arttırır.
