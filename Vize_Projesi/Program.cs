using System;
using System.IO;

namespace Vize_Projesi
{
    class Program
    {
        static void Main(string[] args)
        {
            Labirent lab = new Labirent();
            int[,] labirent = new int[30, 30];
            string[,] slabirent = new string[30, 30];
            labirent = Labirent.labirentOkuma();
            slabirent = Labirent.stringlabirent();
            int[] mXler = new int[3];
            int[] mYler = new int[3];
            int k = 0;
            Random rnd = new Random();
            for (int m = 0; m < 3; m++)
            {
                int mayinX = rnd.Next(0, 30);
                int mayinY = rnd.Next(0, 30);
                mXler[k] = mayinX;
                mYler[k] = mayinY;
                k++;
            }
            labirent[mXler[0], mYler[0]] = 2;
            labirent[mXler[1], mYler[1]] = 2;
            labirent[mXler[2], mYler[2]] = 2;

            slabirent[mXler[0], mYler[0]] = "2";
            slabirent[mXler[1], mYler[1]] = "2";
            slabirent[mXler[2], mYler[2]] = "2";


           
            Console.WriteLine("Rastgele labirentler üretmek için 1 \nOkunan labirentin yolunu bulmak için 2 \nBombaların yerlerini görmek için B e basın. \nLabirentin orijinal halini görmek için L e basın. \nÇıkış yolunu görmek için X e basın. \nEkranı temizlemek için C e basın. \nProgram sonu için Q tuşuna basın.");
            string basilan;
            do
            {
                 basilan = Console.ReadLine();
                switch (basilan)
                {
                    case "1":
                        lab.labirentolustur();
                        Console.WriteLine("Labirent üretildi.");
                        break;

                    case "2":
                        lab.Bul(labirent, 2, " ");
                        break;

                    case "B":
                        for (int i = 0; i < 30; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                if (slabirent[i, j] == "2")
                                    slabirent[i, j] = "B";
                            }
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                Console.Write($" {slabirent[i, j]} ");
                            }
                            Console.Write("\n");
                        }
                        break;

                    case "L":
                        labirent = Labirent.labirentOkuma();
                        for (int i = 0; i < 30; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                Console.Write($" {labirent[i, j]} ");
                            }
                            Console.Write("\n");
                        }break;


                    case "X": lab.Bul(labirent, 0, "X");
                            break;

                    case "C":  Console.Clear();
                                break;
       
                }
            } while ( basilan != "Q");





        }

        
    }
}
