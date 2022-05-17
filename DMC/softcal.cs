using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC
{
    class softcal
    {
        //x1:1420AI1206_CH4.PV
        //x2:1420AI1206_CO2.PV
        //x3:1420FFI1054_1.DACA.PV

        public static double CalTem(double x1, double x2, double x3)
        {
            double T;
            T = 1221.26268 - 0.01935 * x1 - 1.84103 * x2 + 0.22743 * x3;
            T = Math.Round(T, 1);
            return T;

        }
        public static double Calmed(double a, double b, double c, double d)
        {
            double max = a > b ? a : b;
            max = max > c ? max : c;
            max = max > d ? max : d;

            double min = a < b ? a : b;
            min = min < c ? min : c;
            min = min < d ? min : d;

            double med;
            med = (a + b + c + d - max - min) / 2;
            med = Math.Round(med, 1);
            return med;
        }
        public static double Calmed3(double a, double b, double c)
        {

            double max = a > b ? a : b;
            max = max > c ? max : c;

            double min = a < b ? a : b;
            min = min < c ? min : c;
            double med;
            med = a + b + c - max - min;
            med = Math.Round(med, 1);
            return med;
        }
    }
}
