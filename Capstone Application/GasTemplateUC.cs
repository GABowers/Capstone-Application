using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class GasTemplateUC : UserControl
    {
        Dictionary<string, Tuple<double, double>> elements = new Dictionary<string, Tuple<double, double>>()
        {
            {"H", new Tuple<double, double>(1.00794, 1.1) },
            {"He", new Tuple<double, double>(4.002602, 1.4) },
            {"Li", new Tuple<double, double>(6.941, 1.81) },
            {"Be", new Tuple<double, double>(9.01218, 1.53) },
            {"B", new Tuple<double, double>(10.811, 1.92) },
            {"C", new Tuple<double, double>(12.0107, 1.7) },
            {"N", new Tuple<double, double>(14.0067, 1.55) },
            {"O", new Tuple<double, double>(15.9994, 1.52) },
            {"F", new Tuple<double, double>(18.998403, 1.47) },
            {"Ne", new Tuple<double, double>(20.1797, 1.54) },
            {"Na", new Tuple<double, double>(22.989770, 2.27) },
            {"Mg", new Tuple<double, double>(24.3050, 1.73) },
            {"Al", new Tuple<double, double>(26.981538, 1.84) },
            {"Si", new Tuple<double, double>(28.0855, 2.1) },
            {"P", new Tuple<double, double>(30.973761, 1.8) },
            {"S", new Tuple<double, double>(32.065, 1.8) },
            {"Cl", new Tuple<double, double>(35.453, 1.75) },
            {"Ar", new Tuple<double, double>(39.948, 1.88) },
            {"K", new Tuple<double, double>(39.098, 2.75) },
            {"Ca", new Tuple<double, double>(40.078, 2.31) },
            {"Ga", new Tuple<double, double>(69.723, 1.87) },
            {"Ge", new Tuple<double, double>(72.63, 2.11) },
            {"As", new Tuple<double, double>(74.922, 1.85) },
            {"Se", new Tuple<double, double>(78.971, 1.9) },
            {"Br", new Tuple<double, double>(79.904, 1.83) },
            {"Kr", new Tuple<double, double>(83.798, 2.02) },
            {"Rb", new Tuple<double, double>(85.468, 3.03) },
            {"Sr", new Tuple<double, double>(87.62, 2.49) },
            {"In", new Tuple<double, double>(114.82, 1.93) },
            {"Sn", new Tuple<double, double>(118.71, 2.17) },
            {"Sb", new Tuple<double, double>(121.76, 2.06) },
            {"Te", new Tuple<double, double>(127.6, 2.06) },
            {"I", new Tuple<double, double>(126.9, 1.98) },
            {"Xe", new Tuple<double, double>(131.29, 2.16) },
            {"Cs", new Tuple<double, double>(132.91, 3.43) },
            {"Ba", new Tuple<double, double>(137.33, 2.68) },
            {"Tl", new Tuple<double, double>(204.38, 1.96) },
            {"Pb", new Tuple<double, double>(207.2, 2.02) },
            {"Bi", new Tuple<double, double>(208.98, 2.07) },
            {"Po", new Tuple<double, double>(209, 1.97) },
            {"At", new Tuple<double, double>(210, 2.02) },
            {"Rn", new Tuple<double, double>(222, 2.2) },
            {"Fr", new Tuple<double, double>(223, 3.48) },
            {"Ra", new Tuple<double, double>(226, 2.83) },
        };
        List<string> keys;
        public double temperature = 298.15;
        int naRing = 0;
        int aRing = 0;
        int bonds = 0;
        double num_molecules = 0;
        double perc = 0;
        public double k = 1.38064852e-23;
        public double avagodro = 6.022e23;
        public string name = "";
        public int resolution = 5;
        public GasTemplateUC()
        {
            InitializeComponent();
        }

        private void moleculeInput_TextChanged(object sender, EventArgs e)
        {
            keys = new List<string>();
            string cur_text = moleculeInput.Text;
            name = moleculeInput.Text;
            string temp = "";
            while(cur_text.Length > 0)
            {
                temp = temp += cur_text.Substring(0, 1);
                if(elements.ContainsKey(temp))
                {
                    keys.Add(temp);
                    temp = "";
                    cur_text = cur_text.Remove(0, 1);
                }
                else if(int.TryParse(temp, out int compound))
                {
                    keys.Add(keys.Last());
                    temp = "";
                    cur_text = cur_text.Remove(0, 1);
                }
                else
                {
                    cur_text = cur_text.Remove(0, 1);
                }
            }
            SetPressureText();
        }

        public double GetVRMS()
        {
            return Math.Sqrt((3*8.3145*temperature) / (GetMM()/1000.0));
        }

        public double GetVolume()
        {
            double vdWV = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                vdWV += ((double)4*Math.PI*(Math.Pow(elements[keys[i]].Item2, 3)))/3;
            }
            vdWV -= (bonds * 5.92);
            vdWV -= (aRing * 14.7);
            vdWV -= (naRing * 3.8);

            return vdWV/1e30;
        }

        public void Find1ATM()
        {
            if (keys != null)
            {
                //PV=nRT -> P = 101325 Pa, V = vdWV*1000*1000, R = 8.3145, T = temperature
                double p = 101325;
                double v = (GetVolume() * 1000 * 1000);
                double n = (p * v) / (8.3145 * temperature);
                num_molecules = n * 6.022e23;
                perc = (num_molecules / 1000000) * 100;
            }
        }

        public double GetDiffusion(double mm, double t)
        {
            double m = (mm / avagodro) / 1000;
            List<double> values = new List<double>();
            int speed = 0;
            for (int i = 0; i < 250; i++)
            {
                values.Add(GetProb(speed, m, t));
                speed += 1;
            }
            while(values.Last() > (values.Max()/100))
            {
                values.Add(GetProb(speed, m, t));
                speed += 1;
            }
            double avg = values.Average();
            double sum = values.Sum(a => Math.Pow((a - avg), 2.0));
            double variance = sum / ((double)values.Count - 1);
            return variance/2;
        }

        public double GetCross(double volume)
        {
            double di = Math.Pow(volume, (1.0 / 3.0));
            double r2 = Math.Pow((di / 2), 2);
            return 4 * Math.PI * r2;
        }

        double GetProb(int speed, double m, double t)
        {
            return (4 * Math.PI * Math.Pow(speed, 2)) * (Math.Pow((m / (2 * Math.PI * k * t)), (3.0 / 2.0))) * (Math.Pow(Math.E, ((-m * Math.Pow(speed, 2)) / (2 * k * t))));
        }

        public double GetMM()
        {
            double molar_mass = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                molar_mass += elements[keys[i]].Item1;
            }
            return molar_mass;
        }

        //public double GetViscosity()
        //{
        //    double rankine =; 
        //}

        private void tempInput_TextChanged(object sender, EventArgs e)
        {
            if(double.TryParse(tempInput.Text, out double result))
            {
                temperature = 273.15 + result;
            }
            else
            {
                temperature = 298.15;
            }
            SetPressureText();
        }

        private void naRingInput_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(naRingInput.Text, out int result))
            {
                naRing = result;
            }
            else
            {
                naRing = 0;
            }
            SetPressureText();
        }

        private void aRingInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(aRingInput.Text, out int result))
            {
                aRing = result;
            }
            else
            {
                aRing = 0;
            }
            SetPressureText();
        }

        private void bondInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(bondInput.Text, out int result))
            {
                bonds = result;
            }
            else
            {
                bonds = 0;
            }
            SetPressureText();
        }

        void SetPressureText()
        {
            Find1ATM();
            pressureLabel.Text = "With these settings, one would expect 1 atm of pressure at " + Math.Ceiling(num_molecules) + " out of 1,000,000 filled cells, or " + Math.Round(perc, 3) + "% of the grid.";
        }

        private void resInput_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(resInput.Text, out int result))
            {
                resolution = result;
            }
        }
    }
}
