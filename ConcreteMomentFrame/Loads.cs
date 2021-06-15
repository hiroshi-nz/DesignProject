using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Loads
    {
        public List<Item> QList;
        public List<Item> GList;

        public Loads()
        {
            QList = new List<Item>();
            GList = new List<Item>();
        }

        public void AddQ(Item Q)
        {
            QList.Add(Q);
        }

        public void AddG(Item G)
        {
            GList.Add(G);
        }


        public double FindTotal(List<Item> itemList)
        {
            double total = 0;
            foreach (Item item in itemList)
            {
                total += item.number;
            }

            return total;
        }

        public List<Item> GetGList(double factor)
        {
            List<Item> newGList = new List<Item>();
            foreach(Item item in GList)
            {
                item.number = item.number * factor;
                newGList.Add(item);
            }

            return newGList;
        }

        public List<Item> GetQList(double factor)
        {
            List<Item> newQList = new List<Item>();
            foreach (Item item in QList)
            {
                item.number = item.number * factor;
                newQList.Add(item);
            }

            return newQList;
        }

        public Item TotalG(double factor)
        {
            Item item = new Item("Total G", FindTotal(GList) * factor, "kN", "factor:" + factor);
            return item;
        }

        public Item TotalQ(double factor)
        {
            Item item = new Item("Total Q", FindTotal(QList) * factor, "kN", "factor:" + factor);
            return item;
        }

    }
}
