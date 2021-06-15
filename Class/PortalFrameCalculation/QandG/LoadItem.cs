using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.QandG
{
    class LoadItem
    {
        public List<Item> P { get; set; }
        public Item Q { get; set; }
        public Item W { get; set; }
        public Item totalP;
        public string unit;

        public List<Item> itemList;

        public LoadItem(string unit)
        {
            this.unit = unit;
            P = new List<Item>();
        }

        public void CalculateTotalLoad()
        {
            totalP = new Item("Total P", Sum(P), unit, "");
            CreateList();
        }

        private double Sum(List<Item> itemList)
        {
            double sum = 0;
            foreach (Item item in itemList)
            {
                sum += item.number;
            }
            return sum;
        }

        public void CreateList()
        {
            itemList = new List<Item>();

            foreach (Item item in P)
            {
                itemList.Add(item);
            }
            itemList.Add(totalP);
            itemList.Add(Q);
            itemList.Add(W);
        }
    }
}
