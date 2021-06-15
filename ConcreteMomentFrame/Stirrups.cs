using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Stirrups
    {
        public string name;
        public double diameter;
        public int numberOfLegs;
        public double SingleLegArea;
        public double totalLegArea;
        public string note;      

        public Stirrups(string name, double diameter, int numberOfLegs)
        {
            this.name = name;
            this.diameter = diameter;
            this.numberOfLegs = numberOfLegs;
            this.SingleLegArea = (diameter / 2) * (diameter / 2) * Math.PI;
            this.totalLegArea = SingleLegArea * numberOfLegs;
            note = "";
        }
    }
}
