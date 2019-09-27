using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace negyedik_ora
{
    class Program
    {
        public static List<Festmeny> festmenyek = new List<Festmeny>();
        public static Festmeny Legdragabb()
        {
            Festmeny legdragabb = festmenyek[0];
            for (int i = 1; i < festmenyek.Count; i++)
            {
                if (festmenyek[i].LegmagasabbLicit > legdragabb.LegmagasabbLicit)
                {
                    legdragabb = festmenyek[i];
                }
            }
            return legdragabb;
        }
        public static string TizAlkalomnalTobb()
        {
            int i = 0;
            while(i < festmenyek.Count)
            {
                if(festmenyek[i].LicitekSzama > 10)
                {
                    break;
                }
                i++;
            }
            return i < festmenyek.Count ? "Volt festmény, amire tíznél több alkalommal licitáltak" : "Nem volt festmény, amire tíznél több alkalommal licitáltak";
        }
        public static void Sort()
        {
            for(int i = 0; i < festmenyek.Count; i++)
            {
                int ind = i;
                for(int j = ind; j < festmenyek.Count; j++)
                {
                    if(festmenyek[ind].LegmagasabbLicit < festmenyek[j].LegmagasabbLicit)
                    {
                        ind = j;
                    }
                }
                if(ind != i)
                {
                    Festmeny tmp = festmenyek[ind];
                    festmenyek[ind] = festmenyek[i];
                    festmenyek[i] = tmp;
                }
            }
        }
        public static int NemKeltEl()
        {
            int db = 0;
            for(int i = 0; i < festmenyek.Count; i++)
            {
                if (!festmenyek[i].Elkelt)
                {
                    db++;
                }
            }
            return db;
        }
        public static void Kiir()
        {
            for (int i = 0; i < festmenyek.Count; i++)
            {
                Console.WriteLine(festmenyek[i]);
                Console.WriteLine();
            }
        }
        public static void FajlbaIras()
        {
            StreamWriter write = new StreamWriter("festmenyek_rendezett.csv");
            for(int i = 0; i < festmenyek.Count; i++)
            {
                write.WriteLine(festmenyek[i].Cim + ";" + festmenyek[i].Festo + ";" + festmenyek[i].Stilus);
            }
            write.Close();
        }
        static void Main(string[] args)
        {
            
            festmenyek.Add(new Festmeny("FAFFHAFH", "AFFHAHFAA", "SHFAHFAHAA"));
            festmenyek.Add(new Festmeny("HFAFHAHA", "WZZZWA", "ZEAWRZZAWRE"));
            Console.Write("Adjon meg egy darabszámot: ");
            int db = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < db; i++)
            {
                Console.Write("Adja meg a(z) {0} festmény nevét: ",(i+1));
                string cim = Console.ReadLine();
                Console.Write("Adja meg a(z) {0} festmény festőjének a nevét: ", (i + 1));
                string festo = Console.ReadLine();
                Console.Write("Adja meg a(z) {0} festmény stílusát: ", (i + 1));
                string stilus = Console.ReadLine();
                festmenyek.Add(new Festmeny(cim, festo, stilus));
            }
            try
            {
                StreamReader read = new StreamReader("festmenyek.csv");
                string sor = read.ReadLine();
                while (!read.EndOfStream)
                {
                    string[] adatok = sor.Split(';');
                    festmenyek.Add(new Festmeny(adatok[0], adatok[1], adatok[2]));
                    sor = read.ReadLine();
                }
                read.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Váratlan hiba történt!\n" + e);
            }

            Random rnd = new Random();

            for(int i = 0; i < 20; i++)
            {
                festmenyek[rnd.Next(0, festmenyek.Count)].Licit(rnd.Next(10,101));
            }

            Kiir();

            Console.WriteLine("Licitálás");
            while (true)
            {
                try
                {
                    Console.Write("Adja meg a sorszámát a festménynek, amire szeretne licitálni: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    
                    if (index == 0)
                    {
                        break;
                    }
                    else
                    {
                        index--;
                    }
                    if(festmenyek[index].LegutolsoLicitIdeje.Minute >= 2)
                    {
                        festmenyek[index].Elkelt = true;
                    }
                    if (festmenyek[index].Elkelt)
                    {
                        Console.WriteLine("A festmény elkelt!");
                    }
                    else
                    {
                        Console.Write("Adja meg mekkora mértékkel szeretne licitálni: ");
                        string mertek = Console.ReadLine();
                        if(mertek == "")
                        {
                            festmenyek[index].Licit(10);
                        }
                        else
                        {
                            festmenyek[index].Licit(Convert.ToInt32(mertek));
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Rossz indexet adott meg!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Rossz típus!");
                }
            }
            for(int i = 0; i < festmenyek.Count; i++)
            {
                if (festmenyek[i].LicitekSzama > 0)
                {
                    festmenyek[i].Elkelt = true;
                }
            }
            Console.WriteLine("A legdrágábban elkelt festmény:\n" + Legdragabb());
            Console.WriteLine(TizAlkalomnalTobb());
            Console.WriteLine("{0} db festmény nem kelt el",NemKeltEl());
            Console.WriteLine();
            Sort();
            Kiir();
            FajlbaIras();
            Console.ReadKey();
        }
    }
}
