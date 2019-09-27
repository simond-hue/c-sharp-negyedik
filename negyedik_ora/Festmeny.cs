using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negyedik_ora
{
    class Festmeny
    {
        private string cim;
        private string festo;
        private string stilus;
        private int licitekSzama;
        private double legmagasabbLicit;
        private DateTime legutolsoLicitIdeje;
        private bool elkelt;

        public Festmeny(string cim, string festo, string stilus)
        {
            this.cim = cim;
            this.festo = festo;
            this.stilus = stilus;
            this.legmagasabbLicit = 0;
            this.licitekSzama = 0;
            this.elkelt = false;
            this.legutolsoLicitIdeje = DateTime.Now;
        }

        public string Cim { get => cim; set => cim = value; }
        public string Festo { get => festo; }
        public string Stilus { get => stilus; }
        public int LicitekSzama { get => licitekSzama; }
        public double LegmagasabbLicit { get => legmagasabbLicit; }
        public DateTime LegutolsoLicitIdeje { get => legutolsoLicitIdeje; }
        public bool Elkelt { get => elkelt; set => elkelt = value; }

        private double Nullazas(string ertek)
        {
            char[] charArray = ertek.ToCharArray();
            for(int i = 2; i < charArray.Length; i++)
            {
                charArray[i] = '0';
            }
            string s = "";
            for(int i = 0; i < charArray.Length; i++)
            {
                s += charArray[i];
            }
            return Convert.ToDouble(s);
        }

        public void Licit()
        {
            if (this.elkelt)
            {
                Console.WriteLine("A festmény elkelt!");
            }
            else if(this.licitekSzama == 0)
            {
                this.legmagasabbLicit += 100;
                this.licitekSzama++;
                this.legutolsoLicitIdeje = DateTime.Now;
            }
            else
            {
                this.Licit(10);
            }
        }

        public void Licit(int mertek)
        {
            if(this.licitekSzama == 0)
            {
                this.legmagasabbLicit += 100;
                this.licitekSzama++;
                this.legutolsoLicitIdeje = DateTime.Now;
            }
            if (mertek < 10 || mertek > 100)
            {
                Console.WriteLine("Rossz paraméter mértékként!");
            }
            else
            {
                this.legmagasabbLicit = this.legmagasabbLicit + this.legmagasabbLicit * mertek;
                this.legmagasabbLicit = Nullazas(Convert.ToString(this.legmagasabbLicit));
                this.licitekSzama++;
                this.legutolsoLicitIdeje = DateTime.Now;
            }
        }

        private string ElkeltString()
        {
            return this.elkelt ? "elkelt" : "nem kelt el";
        }

        public override string ToString()
        {
            if (this.elkelt)
            {
                return String.Format("{0}: {1}({2})\n{3}$-{4} (összesen: {5} db)",
                    this.festo,
                    this.cim,
                    this.stilus,
                    this.legmagasabbLicit,
                    this.legutolsoLicitIdeje,
                    this.licitekSzama);
            }
            else
            {
                return String.Format("{0}: {1}({2})\n{3}\n{4}$-{5} (összesen: {6} db)",
                    this.festo,
                    this.cim,
                    this.stilus,
                    this.ElkeltString(),
                    this.legmagasabbLicit,
                    this.legutolsoLicitIdeje,
                    this.licitekSzama);
            }
        }
    }
}
