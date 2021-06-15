using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class WindPressure
    {
        private double designWindSpeed;
        private WindItem coeffItem;
        private WindItem windPressureItem;

        public WindPressure(WindItem coeffItem, double designWindSpeed)
        {
            this.coeffItem = coeffItem;
            this.designWindSpeed = designWindSpeed;

            WindItem windPressureItem = new WindItem();

            windPressureItem.windwardWall = new Item("Wind Pressure", FindPressure(coeffItem.windwardWall.number), "Pa", "");
            windPressureItem.leewardWall = new Item("Wind Pressure", FindPressure(coeffItem.leewardWall.number), "Pa", "");

            List<Item> roofList = new List<Item>();

            foreach(Item roof in coeffItem.roofList)
            {
                Item item = new Item(roof.name, FindPressure(roof.number), "Pa", "");
                roofList.Add(item);
            }

            windPressureItem.roofList = roofList;

            List<Item> sideWallList = new List<Item>();

            foreach (Item sideWall in coeffItem.sideWallList)
            {
                Item item = new Item(sideWall.name, FindPressure(sideWall.number), "Pa", "");
                sideWallList.Add(item);
            }
            windPressureItem.sideWallList = sideWallList;

            this.windPressureItem = windPressureItem;
        }

        public double FindPressure(double pressureCoefficient)
        {
            double densityOfAir = 1.2;
            double ans = 0.5 * densityOfAir * designWindSpeed * designWindSpeed * pressureCoefficient;
            return ans;
        }

        public WindItem Solve()
        {
            return windPressureItem;
        }
    }
}
