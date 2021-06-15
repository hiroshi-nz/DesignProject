using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class WindLoad
    {
        private WindItem windPressureItem;
        private WindItem areaItem;
        private WindItem windLoadItem;

        public WindLoad(WindItem windPressureItem, WindItem areaItem)
        {
            this.windPressureItem = windPressureItem;
            this.areaItem = areaItem;

            WindItem windLoadItem = new WindItem();

            windLoadItem.windwardWall = new Item("Wind Load", windPressureItem.windwardWall.number * areaItem.windwardWall.number, "N", "");
            windLoadItem.leewardWall = new Item("Wind Load", windPressureItem.leewardWall.number * areaItem.leewardWall.number, "N", "");

            List<Item> roofList = new List<Item>();

            for(int i = 0; i < windPressureItem.roofList.Count; i++)
            {
                Item item = new Item(windPressureItem.roofList[i].name, windPressureItem.roofList[i].number * areaItem.roofList[i].number, "N", "");
                roofList.Add(item);
            }
            windLoadItem.roofList = roofList;

            List<Item> sideWallList = new List<Item>();

            for (int i = 0; i < windPressureItem.sideWallList.Count; i++)
            {
                Item item = new Item(windPressureItem.sideWallList[i].name, windPressureItem.sideWallList[i].number * areaItem.sideWallList[i].number, "N", "");
                sideWallList.Add(item);
            }
            windLoadItem.sideWallList = sideWallList;

            this.windLoadItem = windLoadItem;
        }


        public WindItem Solve()
        {
            return windLoadItem;
        }
    }
}
