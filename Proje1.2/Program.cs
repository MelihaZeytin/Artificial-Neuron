using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje1._2
{
    class SinirHucresi
    {
        public static Random random = new Random();  // random nesnesinin oluşturulması
        public const double LAMBDA = 0.05;  //  lambda sabiti

        public List<double> OgrenmeKurali(double[,] matris, int epok, int epok2)
        {
            List<double> agirlikListesi = new List<double>();  // x1,x2 girdisinin ağırlığını tutan liste
            int dogruSayac10 = 0;
            int dogruSayac100 = 0;
            // test aşamasında kullanılmak üzere verilen epok sonunda son verilerin(x1 ve x2) ağırlıklarının tutulduğu liste
            List<double> testteKullanilacakAgirliklar = new List<double>();

            // Ağırlıklar için [-1, 1] aralığında rastgele double değerler üreten döngü
            for (int i = 0; i < matris.GetLength(1); i++)
            {
                double randomAgirlik = random.NextDouble() * 2 - 1;
                agirlikListesi.Add(Math.Round(randomAgirlik, 2));  // ağırlık listesine ekleme yapma
            }

            girdileriBölme(matris); // method çağrılması

            //ağın eğitilmesini sağlayan döngü
            for (int i = 0; i < epok; i++) // epok kadar döngü döner
            {
                for (int m = 0; m < matris.GetLength(0); m++)  // matrisin genişliğinde dönülür
                {
                    bool esitMi = false;

                    int outputSonuc = outputHesaplama(matris, m, agirlikListesi, 0);
                    int targetSonuc = targetHesaplama(matris, m);

                    // Ağın eğitimi işleminde kullanılan Öğrenme Kuralının uygulanması
                    for (int j = 0; j < agirlikListesi.Count; j++)
                    {
                        /*Ağın ürettiği çıktı(output), olması gereken değerden(beklenen) yani target değerinden
                        farklı ise ağırlıkları λ * (t - o) * xi kadar arttıran if bloğu(t:target, o: output, λ: öğrenme katsayısı olmak
                        üzere): Yani wi = wi + λ * (t - o) * xi.*/
                        if (outputSonuc != targetSonuc)
                        {
                            agirlikListesi[j] += LAMBDA * (targetSonuc - outputSonuc) * matris[m, j];
                        }

                        // Output ve target değerleri aynı ise ağırlıklarda değişiklik yapmayan else if bloğu
                        else if (outputSonuc.Equals(targetSonuc) && esitMi == false)
                        {
                            // doğru olarak sınıflandırılan veri sayısının bulunmasını sağlayan if-else bloğu
                            if (i == epok2 - 1)  // epok2 turuna geldiğinde doğru sayacı 1 arttıran if bloğu
                                dogruSayac10++;

                            else if (i == epok - 1)  // epok turuna geldiğinde doğru sayacı 1 arttıran if bloğu
                                dogruSayac100++;

                            esitMi = true;  // else if bloğuna 2 kere girilmesini önleyen boolean değişken
                        }
                    }

                    // oluşturulan örnek test verilerinde kullanılacak ağırlıkların bulunması
                    if ((i == epok2 - 1 || i == epok - 1) & m == matris.GetLength(0) - 1)
                    {
                        testteKullanilacakAgirliklar.Add(agirlikListesi[0]);
                        testteKullanilacakAgirliklar.Add(agirlikListesi[1]);
                    }
                }
            }

            //Eğitim verilerinin doğruluk değerinin hesaplanması ve yazdırılması
            double dogrulukDegeri10 = (double)dogruSayac10 / matris.GetLength(0) * 100;
            Console.WriteLine(epok2 + " epok sonundaki veri seti üzerindeki doğruluk değeri:  %" + Math.Round(dogrulukDegeri10, 2));
            double dogrulukDegeri100 = (double)dogruSayac100 / matris.GetLength(0) * 100;
            Console.WriteLine(epok + " epok sonundaki veri seti üzerindeki doğruluk değeri:  %" + Math.Round(dogrulukDegeri100, 2));

            return testteKullanilacakAgirliklar;  // testte kullanılacak ağırlık listesinin döndürülmesi
        }

        public void testAsamasi(List<double> testAgirlikListe, double[,] matris)
        {
            // değişkenlerin tanımlanması
            int dogruTestSayac10 = 0;
            int dogruTestSayac100 = 0;

            girdileriBölme(matris);  // method çağrılması

            for (int i = 0; i < 2; i++)
            {
                for (int m = 0; m < matris.GetLength(0); m++)  // matrisin genişliğinde dönülür
                {
                    int outputSonuc;

                    if (i == 0)
                        outputSonuc = outputHesaplama(matris, m, testAgirlikListe, 0);
                    else
                        // parametre olarak 2 yollanmasının sebebi testAgirlikListesinde 4 eleman var ve 100.epokta ağırlık için son 2 elemanı kullanıyoruz.
                        outputSonuc = outputHesaplama(matris, m, testAgirlikListe, 2);
                    int targetSonuc = targetHesaplama(matris, m);

                    // doğru olarak sınıflandırılan veri sayısının bulunmasını sağlayan if bloğu
                    if (outputSonuc.Equals(targetSonuc))
                    {
                        if (i == 0)  // i 0 olduğunda doğru sayacı 1 arttıran if bloğu
                            dogruTestSayac10++;

                        else if (i == 1)  // i 1 olduğunda doğru sayacı 1 arttıran else-if bloğu
                            dogruTestSayac100++;
                    }
                }
            }

            // Test verilerinin doğruluk değerinin hesaplanması ve yazdırılması
            Console.WriteLine();
            double dogrulukDegeri10 = (double)dogruTestSayac10 / matris.GetLength(0) * 100;
            Console.WriteLine("10 tur sonundaki test sonucu: %" + Math.Round(dogrulukDegeri10, 2));
            double dogrulukDegeri100 = (double)dogruTestSayac100 / matris.GetLength(0) * 100;
            Console.WriteLine("100 tur sonundaki test sonucu: %" + Math.Round(dogrulukDegeri100, 2));
            Console.WriteLine();
        }

        public double[,] girdileriBölme(double[,] matris)
        {
            // tüm girdi değerlerini 10’a bölerek oluşan veri seti matrisi
            for (int i = 0; i < matris.GetLength(0); i++)
            {
                for (int k = 0; k < matris.GetLength(1); k++)
                {
                    matris[i, k] = matris[i, k] / 10;
                }
            }
            return matris;
        }

        public int outputHesaplama(double[,] matris, int m, List<double> agirlikList, int a)
        {
            // değişkenlerin tanımlanması
            double sonuc = 0;
            int outputSonuc;

            // sonuc değişkeninin bulunmasını sağlayan döngü
            int j = 0;
            for (int k = a; k < agirlikList.Count; k++)  // matrisin yüksekliğinde dönülür
            {
                // 10.epoktaki test girdileri için kullanılacak ağırlıklar listenin ilk 2 elemanındadır bu durumdayken for a 2 kere girmesi gerekir.
                // Bunu kontrol eden if bloğu
                if (a == 0 & j == 2 & agirlikList.Count == 4)
                    break;  // döngüden çıkılıyor
                sonuc += (matris[m, j] * agirlikList[k]);  // girdilerle ağırlıkların çarpımlarının toplanması ve sonuç değişkenine atanması
                ++j;
            }

            // Eğer sonuç 0.5'ten küçükse if bloğuna girer değilse else bloğuna giren kod.
            if (sonuc < 0.5)
            {
                outputSonuc = -1;
            }
            else
            {
                outputSonuc = 1;
            }

            return outputSonuc;
        }

        public int targetHesaplama(double[,] matris, int m)
        {
            // değişkenlerin tanımlanması
            double target = 0;
            int targetSonuc;

            // target değişkeninin bulunmasını sağlayan döngü
            for (int k = 0; k < matris.GetLength(1); k++)  // matrisin yüksekliğinde dönülür
            {
                target += matris[m, k];  // girdilerin toplanması ve target değişkenine atanması
            }

            // toplamları pozitif olan iki adet sayıyı 1 negatif olanları ise - 1 olarak sınıflandırmamız gerekir
            // buna göre target>0'dan büyükse if bloğuna girer değilse else bloğuna giren kod.
            if (target > 0)
            {
                targetSonuc = 1;
            }
            else
            {
                targetSonuc = -1;
            }
            return targetSonuc;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            SinirHucresi sinirHucresi = new SinirHucresi();
            //Ağın eğitiminde kullanılmak üzere Eğitim (training) verileri için oluşturulan matris
            double[,] veriSetiMatrisi = { { 6, 5 }, { 2, 4 }, { -3, -5 }, { -1, -1 }, { 1, 1 }, { -2, 7 }, { -4, -2 }, { -6, 3 }};
            List<double> testteKullanilacakAgirliklar = sinirHucresi.OgrenmeKurali(veriSetiMatrisi, 100, 10);

            // eğitim verileri dışında 1 tane test verisi örneği ve oluşturulan test verileriyle testAsamasi metodunun çağrılması
            double[,] testVeri1 = { { 3, -2 }, { -8, 4 }, { 7, 5 }, { -5, -1 }, { -3, 6 }};
            sinirHucresi.testAsamasi(testteKullanilacakAgirliklar, testVeri1);

            Console.ReadLine();
        }
    }
    
}