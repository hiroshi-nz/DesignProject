using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class
{
    class Item
    {
        public string name;
        public double number;
        public string unit;
        public string note;

        int decimalPlace = 3;

        public Item(string name, double number, string unit, string note)
        {
            this.name = name;
            this.number = number;
            this.unit = unit;
            this.note = note;
        }

        public Item(double number)
        {
            this.name = "";
            this.number = number;
            this.unit = "";
            this.note = "";
        }

        public Item(double number, string unit)
        {
            this.name = "";
            this.number = number;
            this.unit = unit;
            this.note = "";
        }

        public string NumberWithUnit()
        {
            return MathHelper.RoundDec(number, decimalPlace) + unit;
        }

        public string Print()
        {
            string txt = name + " : " + MathHelper.RoundDec(number, decimalPlace) + unit + " : " + note;
            return txt;
        }
    }
}
