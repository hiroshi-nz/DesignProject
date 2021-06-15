using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class GableRoof
    {
        public double height;

        public List<double> horizontalDistanceList;
        private List<DoublePair> lessThanPointFiveList;
        private List<DoublePair> moreThanOneList;

        public List<DoublePair> coefficientList;

        public GableRoof(double height, double heightToDepth)
        {
            this.height = height;
            horizontalDistanceList = new List<double>() { 0, MathHelper.Round2dec(0.5 * height), MathHelper.Round2dec(1 * height), MathHelper.Round2dec(2 * height), MathHelper.Round2dec(3 * height) };

            if(heightToDepth <= 0.5)
            {
                LessThanPointFive();
                //Print(lessThanPointFiveList);
            }
            else if(heightToDepth >= 1.0)
            {
                MoreThanOne();
            }
            else
            {
                Console.WriteLine("Roof coefficient not implemented yet.");
            }
        }

        public void LessThanPointFive()
        {

            List<DoublePair> pressurePairList = new List<DoublePair>();

            DoublePair pressurePair = new DoublePair(horizontalDistanceList[0] + "m to " + horizontalDistanceList[1] + "m", -0.9, -0.4, "h/d<=0.5");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[1] + "m to " + horizontalDistanceList[2] + "m", -0.9, -0.4, "h/d<=0.5");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[2] + "m to " + horizontalDistanceList[3] + "m", -0.5, 0, "h/d<=0.5");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[3] + "m to " + horizontalDistanceList[4] + "m", -0.3, 0.1, "h/d<=0.5");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair("Greater than " + horizontalDistanceList[4] + "m", -0.2, 0.2, "h/d<=0.5");
            pressurePairList.Add(pressurePair);

            this.lessThanPointFiveList = pressurePairList;
            this.coefficientList = pressurePairList;
        }

        public void MoreThanOne()
        {
            List<DoublePair> pressurePairList = new List<DoublePair>();

            DoublePair pressurePair = new DoublePair(horizontalDistanceList[0] + "m to " + horizontalDistanceList[1] + "m", -1.3, -0.6, "h/d>=1.0");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[1] + "m to " + horizontalDistanceList[2] + "m", -0.7, -0.3, "h/d>=1.0");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[2] + "m to " + horizontalDistanceList[3] + "m", -0.7, -0.3, "h/d>=1.0, the value given is only for interpolation purpose");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair(horizontalDistanceList[3] + "m to " + horizontalDistanceList[4] + "m", -0.7, -0.3, "h/d>=1.0, the value given is only for interpolation purpose");
            pressurePairList.Add(pressurePair);

            pressurePair = new DoublePair("Greater than " + horizontalDistanceList[4] + "m", -0.7, -0.3, "h/d>=1.0, the value given is only for interpolation purpose");
            pressurePairList.Add(pressurePair);

            this.moreThanOneList = pressurePairList;
            this.coefficientList = pressurePairList;
        }

        public void Print()
        {
            foreach (DoublePair pair in coefficientList)
            {
                Console.WriteLine(pair.name + " min:" + pair.min + " max:" + pair.max + " note:" + pair.note);
            }
        }

        public void Print(List<DoublePair> pressurePairList)
        {
            foreach(DoublePair pair in pressurePairList)
            {
                Console.WriteLine(pair.name  + " min:" + pair.min + " max:" + pair.max + " note:" + pair.note);
            }
        }
    }
}
