using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class SideWalls
    {
        public List<double> horizontalDistanceList;
        public List<Item> coefficientList;

        public SideWalls(double height)
        {
            horizontalDistanceList = new List<double>() { 0, MathHelper.Round2dec(1 * height), MathHelper.Round2dec(2 * height), MathHelper.Round2dec(3 * height) };

            coefficientList = new List<Item>();
            Item item = new Item(horizontalDistanceList[0] + "m to " + horizontalDistanceList[1] + "m", -0.65, "", "");
            coefficientList.Add(item);

            item = new Item(horizontalDistanceList[1] + "m to " + horizontalDistanceList[2] + "m", -0.5, "", "");
            coefficientList.Add(item);

            item = new Item(horizontalDistanceList[2] + "m to " + horizontalDistanceList[3] + "m", -0.3, "", "");
            coefficientList.Add(item);

            item = new Item("Greater than " + horizontalDistanceList[3] + "m", -0.2, "", "");
            coefficientList.Add(item);
        }

        public void Print()
        {
            foreach (Item item in coefficientList)
            {
                Console.WriteLine(item.name + ": " + item.number);
            }
        }
    }
}
