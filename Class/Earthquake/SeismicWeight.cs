using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.ConcreteMomentFrame;

namespace CivilApp.Class.Earthquake
{
    class SeismicWeight
    {
        public Item psiE;
        public Item seismicWeight;
        public Floor floor;
        public Loads additionalLoads;

        public List<Item> GList;
        public List<Item> QList;
        public Item totalG;
        public Item totalQ;

        public List<Item> viewList1;
        

        public SeismicWeight(Floor floor,Loads additionalLoads, Item psiE)
        {
            GList = new List<Item>();
            QList = new List<Item>();

            this.floor = floor;
            this.additionalLoads = additionalLoads;
            this.psiE = psiE;


            CreateGandQLists();
            CalculateSeismicWeight();

            CreateList1();
        }

        private void CreateGandQLists()
        {
            foreach(Item G in floor.loads.GList)
            {
                GList.Add(G);
            }

            foreach (Item G in additionalLoads.GList)
            {
                GList.Add(G);
            }

            foreach (Item Q in floor.loads.QList)
            {
                QList.Add(Q);
            }

            foreach (Item Q in additionalLoads.QList)
            {
                QList.Add(Q);
            }

        }

        private void CalculateSeismicWeight()
        {
            totalG = new Item("Total G", 0, "kN", "");
            totalQ = new Item("Total Q", 0, "kN", "");

            foreach (Item G in GList)
            {
                totalG.number += G.number;
            }

            foreach (Item Q in QList)
            {
                totalQ.number += Q.number;
            }
            Item ans = ItemMath.AddMultiply(totalG, totalQ, psiE);
            seismicWeight = new Item("Seismic Weight", ans.number, "kN", ans.note);
        }

        private void CreateList1()
        {
            viewList1 = new List<Item>();

            Item title1 = new Item("Seismic Weight Data", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(divider);
            viewList1.Add(title2);

            foreach (Item item in GList)
            {
                viewList1.Add(item);
            }
                viewList1.Add(totalG);
                viewList1.Add(divider);
                viewList1.Add(title3);

            foreach (Item item in QList)
            {
                viewList1.Add(item);
            }
                viewList1.Add(totalQ);
                viewList1.Add(divider);
                viewList1.Add(seismicWeight);
        }
    }
}
