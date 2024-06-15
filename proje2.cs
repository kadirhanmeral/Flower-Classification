using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using static proje2.Program;

namespace proje2
{
    internal class Program
    {

        public class Sinirhucresi
        {
            public void dosyaokuma(double[,] veridizi)  //dosya okuma işlemi
            {
                string[] data = System.IO.File.ReadAllLines(@"C:\Users\kadir\Desktop\iris.txt");
                for (int i = 0; i < data.Length; i++)
                {
                    string veri = data[i];
                    string[] sozcukler = veri.Split(',');
                    for (int j = 0; j < 4; j++)
                    {
                        veridizi[i, j] = double.Parse(sozcukler[j]);
                    }
                }
            }
            public void randomağırlıkolusturma(double[] n1ağırlık, double[] n2ağırlık, double[] n3ağırlık)  //random ağırlık oluşturma
            {
                Random random = new Random();
                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (i == 0)
                        {
                            n1ağırlık[k] = random.NextDouble();
                        }
                        else if (i == 1)
                        {
                            n2ağırlık[k] = random.NextDouble();
                        }
                        else
                        {
                            n3ağırlık[k] = random.NextDouble();
                        }
                    }
                }
            }
            public void çıktıhesaplama(double[] n1ağırlık, double[] n2ağırlık, double[] n3ağırlık, double[] n1çıktı, double[] n2çıktı, double[] n3çıktı, double[,] veridizi)  //çıktı hesaplama
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 150; j++)
                    {
                        double çıktı = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            if (i == 0)
                            {
                                çıktı += n1ağırlık[k] * veridizi[j, k] / 10;
                                n1çıktı[j] = çıktı;
                            }
                            else if (i == 1)
                            {
                                çıktı += n2ağırlık[k] * veridizi[j, k] / 10;
                                n2çıktı[j] = çıktı;
                            }
                            else
                            {
                                çıktı += n3ağırlık[k] * veridizi[j, k] / 10;
                                n3çıktı[j] = çıktı;
                            }
                        }
                    }
                }
            }
        }
        public class yapaysinirağı
        {
            Sinirhucresi hesapla = new Sinirhucresi();  //çıktıhesaplamayı kullanmak için oluşturduk
            public void egitim(string[] dogruverisayi, bool agirlikdegistirme, double ogrenmekatsayi, double[] n1çıktı, double[] n2çıktı, double[] n3çıktı, double[,] veridizi, double[] n1ağırlık, double[] n2ağırlık, double[] n3ağırlık)
            {//eğitim methodu
                for (int i = 0; i < 150; i++)
                {
                    hesapla.çıktıhesaplama(n1ağırlık, n2ağırlık, n3ağırlık, n1çıktı, n2çıktı, n3çıktı, veridizi);
                    if (i < 50)   // Iris-setosa çiçekleri için buraya giriyor
                    {
                        if (n1çıktı[i] > n2çıktı[i] && n1çıktı[i] > n3çıktı[i]) // n1 çıktısı en büyük ise buraya giriyor
                        {
                            if (agirlikdegistirme == false) //doğru veri sayısını hesaplamak için bool değişken kullanıyoruz
                            {
                                dogruverisayi[i] = "1";
                            }
                            continue;
                        }
                        else if (n2çıktı[i] > n1çıktı[i] && n2çıktı[i] > n3çıktı[i]) // n2 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {   //ağırlık değiştirme
                                    n1ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                    n2ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                        else //n3 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {
                                    n1ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                    n3ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                    }
                    else if (i < 100) //Iris-versicolor çiçekleri için buraya giriyor
                    {
                        if (n2çıktı[i] > n1çıktı[i] && n2çıktı[i] > n3çıktı[i]) // n2 çıktısı en büyük ise buraya giriyor
                        {
                            if (agirlikdegistirme == false)
                            {
                                dogruverisayi[i] = "1";
                            }
                            continue;
                        }
                        else if (n1çıktı[i] > n2çıktı[i] && n1çıktı[i] > n3çıktı[i]) // n1 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {
                                    n1ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                    n2ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                        else   // n1 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {
                                    n2ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                    n3ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                    }
                    else //Iris-virginica çiçekleri için buraya giriyor
                    {
                        if (n3çıktı[i] > n1çıktı[i] && n3çıktı[i] > n2çıktı[i]) // n3 çıktısı en büyük ise buraya giriyor
                        {
                            if (agirlikdegistirme == false)
                            {
                                dogruverisayi[i] = "1";
                            }
                            continue;
                        }
                        else if (n1çıktı[i] > n2çıktı[i] && n1çıktı[i] > n3çıktı[i])  // n1 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {
                                    n1ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                    n3ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                        else // n2 çıktısı en büyük ise buraya giriyor
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (agirlikdegistirme == true)
                                {
                                    n2ağırlık[j] -= ogrenmekatsayi * veridizi[i, j] / 10;
                                    n3ağırlık[j] += ogrenmekatsayi * veridizi[i, j] / 10;
                                }
                            }
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            //uygun yapıda array oluşturuyoruz
            double[,] veridizi = new double[150, 4];
            double[] n1ağırlık = new double[4];
            double[] n2ağırlık = new double[4];
            double[] n3ağırlık = new double[4];
            double[] n1çıktı = new double[150];
            double[] n2çıktı = new double[150];
            double[] n3çıktı = new double[150];

            Sinirhucresi sinirhucresi = new Sinirhucresi();
            sinirhucresi.dosyaokuma(veridizi);   //dosya okuma işlemi
            yapaysinirağı sinirağı = new yapaysinirağı();

            //yazdırma işlemi
            int[] tekrarsayi = { 20, 50, 100 };
            double[] katsayi = { 0.005, 0.01, 0.025 };
            for (int i = 0; i < tekrarsayi.Length; i++)
            {
                for (int j = 0; j < katsayi.Length; j++)
                {

                    sinirhucresi.randomağırlıkolusturma(n1ağırlık, n2ağırlık, n3ağırlık); //random ağırlık oluşturma işlemi
                    Console.Write("Epok sayısı: " + tekrarsayi[i] + " " + "Katsayı değeri: " + katsayi[j] + " Doğruluk Değeri: %");
                    dongu(sinirağı, sinirhucresi, tekrarsayi[i], katsayi[j], veridizi, n1ağırlık, n2ağırlık, n3ağırlık, n1çıktı, n2çıktı, n3çıktı);
                }
            }
            Console.ReadLine();
        }
        public static void dongu(yapaysinirağı sinirağı, Sinirhucresi sinirhucresi, int k, double m, double[,] veridizi, double[] n1ağırlık, double[] n2ağırlık, double[] n3ağırlık, double[] n1çıktı, double[] n2çıktı, double[] n3çıktı)
        {
            //farklı epoklarda işlem tekrarlanması
            string[] dogruverisayi = new string[150];
            for (int i = 0; i < k; i++)
            {
                sinirağı.egitim(dogruverisayi, true, m, n1çıktı, n2çıktı, n3çıktı, veridizi, n1ağırlık, n2ağırlık, n3ağırlık);
            }
            sinirağı.egitim(dogruverisayi, false, m, n1çıktı, n2çıktı, n3çıktı, veridizi, n1ağırlık, n2ağırlık, n3ağırlık);
            double sayac = 0;
            for (int i = 0; i < 150; i++)
            {
                if (dogruverisayi[i] == "1")
                {
                    sayac++;
                }
            }
            double yüzde = (sayac / 150 * 100);
            Console.WriteLine(Math.Round(yüzde, 2));
        }
    }
}
