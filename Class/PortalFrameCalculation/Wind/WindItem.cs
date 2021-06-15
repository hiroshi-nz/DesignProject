using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class WindItem
    {
        public Item windwardWall { get; set; }
        public Item leewardWall { get; set; }

        public Item upwindSlope { get; set; }
        public Item downwindSlope { get; set; }

        public List<Item> roofList { get; set; }
        public List<Item> sideWallList { get; set; }

        public List<Item> itemList;

        public WindItem(Item windwardWall, Item leewardWall, List<Item> roofList, List<Item> sideWallList)
        {
            this.windwardWall = windwardWall;
            this.leewardWall = leewardWall;

            this.roofList = roofList;
            this.sideWallList = sideWallList;
        }

        public WindItem()
        {

        }

        public void Print()
        {
            Console.WriteLine("Windward Wall " + windwardWall.name + ":" + MathHelper.Round2dec(windwardWall.number) + windwardWall.unit + " " + windwardWall.note);
            Console.WriteLine("Leeward Wall " + leewardWall.name + ":" + MathHelper.Round2dec(leewardWall.number) + leewardWall.unit + " " + leewardWall.note);

            foreach(Item item in roofList)
            {
                Console.WriteLine("Roof " + item.name + ":" + MathHelper.Round2dec(item.number) + item.unit + " " + item.note);
            }

            foreach (Item item in sideWallList)
            {
                Console.WriteLine("Side Wall " + item.name + ":" + MathHelper.Round2dec(item.number) + item.unit + " " + item.note);
            }

            Console.WriteLine("================================================");
        }

        public void CreateList()
        {
            itemList = new List<Item>();
            Item newWindwardWall = new Item("Windward Wall " + windwardWall.name, windwardWall.number, windwardWall.unit, windwardWall.note);
            itemList.Add(newWindwardWall);
            Item newLeewardWall = new Item("Leeward Wall " + leewardWall.name, leewardWall.number, leewardWall.unit, leewardWall.note);
            itemList.Add(newLeewardWall);

            foreach (Item item in roofList)
            {
                Item newItem = new Item("Roof " + item.name, item.number, item.unit, item.note);
                itemList.Add(newItem);
            }

            foreach (Item item in sideWallList)
            {
                Item newItem = new Item("Side Wall " + item.name, item.number, item.unit, item.note);
                itemList.Add(newItem);
            }
        }
    }
}
