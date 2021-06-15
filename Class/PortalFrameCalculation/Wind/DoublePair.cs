using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class DoublePair
    {
        public string name;
        public double max;
        public double min;
        public string note;

        public DoublePair(string name, double firstValue, double SecondValue, string note)
        {
            this.name = name;
            this.note = note;

            if (firstValue > SecondValue)
            {
                max = firstValue;
                min = SecondValue;
            }
            else
            {
                max = SecondValue;
                min = firstValue;
            }
        }
    }
}
