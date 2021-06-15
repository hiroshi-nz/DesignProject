using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class PressureCoefficients
    {
        public Item windwardWall;
        public Item leewardWall;

        public Item upwindSlope;
        public Item downwindSlope;
        public GableRoof roof;
        public SideWalls sideWalls;

        public Item internalCoefficient;

        public WindItem pressureCoefficients;

        public PressureCoefficients(Item windwardWall, Item leewardWall, GableRoof roof, SideWalls sideWalls, Item internalCoefficient)
        {
            this.windwardWall = windwardWall;
            this.leewardWall = leewardWall;
            this.roof = roof;
            this.sideWalls = sideWalls;
            this.internalCoefficient = internalCoefficient;

            pressureCoefficients = new WindItem();

            //Item item = new Item(windwardWall.name, windwardWall.number - internalCoefficient.number, "", "Not conservative");
            Item item = new Item("Pressure Coefficient", windwardWall.number, "", "Conservative approach, treat it as internal and external not cancelling out.");
            pressureCoefficients.windwardWall = item;

            item = new Item("Pressure Coefficient", leewardWall.number - internalCoefficient.number, "", "");
            pressureCoefficients.leewardWall = item;

            pressureCoefficients.roofList = FindRoofCoefficients(roof.coefficientList);

            List<Item> sideWallList = new List<Item>();

            foreach (Item coefficient in sideWalls.coefficientList)
            {
                item = new Item(coefficient.name, coefficient.number - internalCoefficient.number, coefficient.unit, coefficient.note);
                sideWallList.Add(item);
            }
            pressureCoefficients.sideWallList = sideWallList;

        }

        public List<Item> FindRoofCoefficients(List<DoublePair> coefficientList)
        {
            List<Item> roofList = new List<Item>();

            foreach (DoublePair pair in coefficientList)
            {
                double value1 = pair.max - internalCoefficient.number;
                double value2 = pair.min - internalCoefficient.number;

                if (Math.Abs(value1) > Math.Abs(value2))
                {
                    Item item = new Item(pair.name, value1, "", pair.note);
                    roofList.Add(item);
                }
                else if (Math.Abs(value1) == Math.Abs(value2))
                {
                    Item item = new Item(pair.name, value1, "", pair.note + ". Same magnitude" + value1 + ":" + value2);
                    roofList.Add(item);
                }
                else
                {
                    Item item = new Item(pair.name, value2, "", pair.note);
                    roofList.Add(item);
                }
            }
            return roofList;
        }





        public WindItem Solve()
        {
            return pressureCoefficients;
        }
    }
}
