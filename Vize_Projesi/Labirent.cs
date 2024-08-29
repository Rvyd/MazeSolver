using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Vize_Projesi
{
    class Labirent
    {
        public static int[,] labirentOkuma()
        {

            int[,] matris = new int[30, 30];
            string[] elemanlar = new string[30];

            string text = File.ReadAllText("yazilan.txt");
            text = text.Remove(0, 1);                                //baştaki iki tane olan { lardan birini kaldırdık ve text'e o şekilde atadık
            text = text.Remove(text.Length - 1, 1);                  //sondaki satirdaki iki tane olan { lardan birini kaldırdık ve text'e o şekilde atadık

            elemanlar = text.Split('{', StringSplitOptions.RemoveEmptyEntries);         //Tüm { ları kaldırdık komut ile boşlukmatamadan direkt kaldırdık ,boşluk atamadan

            int satirNo = 0;

            foreach (string str in elemanlar)     //30 satırda satır satır işlem yapmak için
            {

                string[] sonmatris;

                string parcalar = str.Split('}', StringSplitOptions.RemoveEmptyEntries)[0];   //30 tane satirdaki her bir elemanın sonunda  }var ve biz bunu ikiye ayırıyoruz. [1] kısımı zaten boşluklardan oluşuyor

                sonmatris = parcalar.Split(',', StringSplitOptions.RemoveEmptyEntries);     // virgüllerin kaldırılmış halini sonmatris dizisine atıyoruz

                for (int k = 0; k < 30; k++)
                {

                    matris[satirNo, k] = Int32.Parse(sonmatris[k]);

                }

                satirNo++;

            }

            return matris;
        }

        public static string[,] stringlabirent()       //Bombalar için string labirent
        {

            string[,] matris = new string[30, 30];
            string[] elemanlar = new string[30];

            string text = File.ReadAllText("yazilan.txt");
            text = text.Remove(0, 1);
            text = text.Remove(text.Length - 1, 1);

            elemanlar = text.Split('{', StringSplitOptions.RemoveEmptyEntries);

            int satirNo = 0;

            foreach (string str in elemanlar)
            {

                string[] sonmatris;

                string parcalar = str.Split('}', StringSplitOptions.RemoveEmptyEntries)[0];

                sonmatris = parcalar.Split(',', StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < 30; k++)
                {

                    matris[satirNo, k] = sonmatris[k];

                }

                satirNo++;

            }

            return matris;
        }

   

        public void Yollarigoster(string [,] matris)       //Gidilen yolları X karakteri ile gösterme kısımı
        {

            string[,] Xmatris = new string[30, 30];
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Xmatris[i, j] = matris[i, j];
                }
              
            }
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (Xmatris[i, j] == "0")
                        Xmatris[i, j] = "X";
                    
                }
                
            }
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Console.Write($" {Xmatris[i, j]} ");
                }

                Console.Write("\n");
            }

        }
        public bool mayinVarmi(int [,]matris,int x ,int y)
        {
             
            return (x < 30 && y < 30 && y >= 0 && x >= 0 && matris[x,y]==2);  //Gidiş yolu üzerinde bomba var mı kontrolu
        }
        public bool guvenliYol(int[,] matris, int x, int y)
        {
         
            return (x < 30 && y < 30 && y >= 0 && x >= 0 && matris[x, y] == 0); //Verilen değerin geçiş yolu olup olmadığını kontrol ediyoruz
        }

        public bool Bul(int[,] labirent,int a,string b)
        {

            for (int i = 0; i < 30; i++)
            {
                int[,] yol = new int[30, 30];
                string[,] yol2 = new string[30, 30];
                for (int j = 0; j < 30; j++)
                {
                    for (int k = 0; k < 30; k++)
                    {
                        yol[j, k] = 1; //Gittiği yolları kotrol etmek için sadece duvarlardan oluşan bir matris oluşturuyoruz.
                        yol2[j, k] = "1";
                    }
                }

                if (yolBulma(labirent, yol,yol2, 0, i) == true)
                {
                    if(a==2)
                    MatrisiGoster(yol);
                    if(b=="X")
                    Yollarigoster(yol2);
                    return true;
                }
            }
            Console.WriteLine("Çözüm bulunamadı!");
            return false;
        }
        public void MatrisiGoster(int[,] matris)       //Oluşan yol labirentini yazdırma
        {
           for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Console.Write($" {matris[i, j]} ");
                }

                Console.Write("\n");
            }

             for(int i = 0; i < 30; i++)  //koordinatları yazdırma deneme
              {
                  for(int j = 0; j < 30; j++)
                  {
                      if (matris[j, i] == 0)
                    {
                         Console.WriteLine(j + " \t" + i);
                    }
                         
                  }
              }

        }
        public bool yolBulma(int[,] labirent, int[,] yol,string[,] yol2, int sutun, int satir)
        {
            if (sutun == 29 && labirent[satir, sutun] == 0 && sutun==int.Parse("29") && labirent[satir,sutun]==int.Parse("0"))
            {
                yol[satir, sutun] = 0;   // 29.satırdaki her 0 değerinin çıkış olacağını göstermek için
                yol2[satir, sutun] = "0";
                return true;
            }

            if (mayinVarmi(labirent, satir, sutun) == true)    //Bomba olunca programı bitirme durumu
            {
                Console.Beep(500, 500);
                Console.WriteLine("Bombaya yakalandın!!!");
                Environment.Exit(0);
            }
          
            if (guvenliYol(labirent, satir, sutun) == true)
            {

                if (yol[satir, sutun] == 0 && yol2[satir,sutun]=="0")
                    return false;   //Geçilen yollar 0 ile değiştirildiğinden ,Eğer o yol değeri 0 ise gittiği yoldan gidemeyeceğinden false döndürür.

                yol[satir, sutun] = 0;
                yol2[satir, sutun] = "0";

                if (yolBulma(labirent, yol,yol2, sutun, satir + 1) == true)  //Aşağı kısıma haraketini kontrol ediyoruz.
                    return true;
                if (yolBulma(labirent, yol,yol2, sutun + 1, satir) == true)  //Sağ tarafına hareketini kontrol ediyoruz.
                    return true;
                if (yolBulma(labirent, yol,yol2 ,sutun, satir - 1) == true)   //Yukarı kısıma haraketini kontrol ediyoruz.
                    return true;
                if (yolBulma(labirent, yol,yol2, sutun - 1, satir) == true)   //Sol kısıma haraketini kontrol ediyoruz.
                    return true;

                yol[satir, sutun] = 1;
                yol2[satir, sutun] = "1";
                return false;
               
            }
         
            return false;
        }

        Random rnd = new Random();
        int[,] matris = new int[30, 30];

        string yazilacakyer = @"yazilan.txt";

        public void labirentolustur()
        {

            int[,] labirent2 = new int[30, 30];
            int[] girisler = new int[5];     //5 tane giriş , 4 tane çıkış tanımladık
            int[] cikislar = new int[3];
            for (int j = 0; j < 30; j++)
            {
                for (int p = 0; p < 30; p++)   // İlk başta sadece duvardan oluşan bir labirent 
                {
                    labirent2[j, p] = 1; 
                    
                }
            }
            Random rnd = new Random();
            int giris = 0 , cikis=0;
             for(int i = 0; i < 5; i++)
            {
                giris = rnd.Next(9, 20);      //Sağ sol yukarı aşağı hareket yaparken matristen çıkmasın diye random aralığını küçük tuttuk.
                labirent2[giris, 0] = 0;
                girisler[i] = giris;
            }
           
             for(int y = 0; y < 2; y++)      // 2 çıkışı rastgele ürettik
            {
                cikis = rnd.Next(9, 20);
                labirent2[cikis, 29] = 0;
                cikislar[y] = cikis;
            }
            
            int str = girisler[2];
            for(int i = 0; i < 5; i++)
            {
                labirent2[str, i+1] = 0;     // 5 sağa hareket ettik
            }

            for ( int b = 0; b < 3; b++)
            {
                labirent2[str - b, 5] = 0;   // 3 hamle yaptık ancak i 0 dan başladığı için 2 yukarıya hareket ettik 
                
             
            }
            str -= 2; 
            int stn = 5;                       // 5 sağa hareket edildiği için sütun 0 dan 5 olur
            int l;
            for (l = 0; l< 8; l++)
            {
                labirent2[str, stn+(l+1)] = 0;      // 8 sutun sağa hareket ettik  (5 ti 13 oldu)
            }
            stn += l;
           
            for (int k = 0; k < 10; k++)            // 10 hareket yapıyoruz ama 0 dan başladığı için 9 aşağı iner
            {
                labirent2[str + k, stn]=0;
            }
            str += 9;
            int c;
            for (c = 0; c < 7; c++)
            {
                labirent2[str, stn + (c + 1)] = 0;   // 7 sağa hareket ettik
            }
            stn += c;
            for(int f = 0; f < 2; f++)
            {
                labirent2[str - (f + 1), stn] = 0;    // 2 yukarıya hareket ettik
            }
            str -= 2;
            int r;
            for (r = 0; r < 2; r++)
            {
                labirent2[str, stn + (1 + r)] = 0;   // 2 sağa hareket ettik
            }
            stn += 2;
            for(int g = 0; g < 2; g++)
            {
                labirent2[str + (1 + g), stn] = 0;  //2 aşağıya hareket ettik
            }
            str += 2;
            for(int b = 0; b < 7; b++)
            {
                labirent2[str, stn + (1 + b)] = 0;     // 7 sağa hareket ettik   , Fark edilirse sutun sayısı 29 a ulaştı
            }

            labirent2[str, 29] = 0;       // Bulduğumuz yeri çıkış yaptık garanti çıkış 

            for (int j = 0; j < 30; j++)    // Yolun dışanda kalan kısıma rastgele 0 veya 1 atadık
            {
                for (int p = 0; p < 30; p++)
                {
                    if (labirent2[j, p] != 0)
                        labirent2[j, p] = rnd.Next(0, 2);

                }
            }
            StreamWriter Yaz = new StreamWriter(yazilacakyer, false);  // Dosyaya yazdırma kısımı
           for (int i = 0; i < 30; i++)
           {
               for (int j = 0; j < 30; j++)
               {
                   if (j == 0 && i == 0)
                       Yaz.Write("{");
                   if (j == 0)
                       Yaz.Write("{");

                   Yaz.Write(labirent2[i, j]);

                   if (j != 29)
                       Yaz.Write(",");

                   if (j == 29)
                       Yaz.Write("}");
                   if (j == 29 && i != 29)
                       Yaz.Write(",");
                   if (j == 29 && i == 29)
                       Yaz.Write("}");

               }

               Yaz.Write("\n");
           }
           Yaz.Close();

         

        }
    }
}
