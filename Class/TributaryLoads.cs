using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.ConcreteMomentFrame;

namespace CivilApp.Class
{
    class TributaryLoads
    {
        public Item totalWeight;
        public Floor floor;
        public Loads additionalLoads;

        public List<Item> GList;
        public List<Item> QList;
        public Item totalG;
        public Item totalQ;

        public Item Gfactor;
        public Item Qfactor;

        public List<Item> viewList1;


        public TributaryLoads(Floor floor, Loads additionalLoads, Item Gfactor, Item Qfactor)
        {
            GList = new List<Item>();
            QList = new List<Item>();

            this.Gfactor = Gfactor;
            this.Qfactor = Qfactor;

            this.floor = floor;
            this.additionalLoads = additionalLoads;

            CreateGandQLists();
            CalculateTotalWeight();

            CreateList1();
        }

        private void CreateGandQLists()
        {
            foreach (Item G in floor.loads.GList)
            {
                Item ans = ItemMath.Multiply(Gfactor, G);
                Item newG = new Item(G.name, ans.number, G.unit, G.note + "  " + ans.note);
                GList.Add(newG);
            }

            foreach (Item G in additionalLoads.GList)
            {
                Item ans = ItemMath.Multiply(Gfactor, G);
                Item newG = new Item(G.name, ans.number, G.unit, G.note + "  " + ans.note);
                GList.Add(newG);
            }

            foreach (Item Q in floor.loads.QList)
            {
                Item ans = ItemMath.Multiply(Qfactor, Q);
                Item newQ = new Item(Q.name, ans.number, Q.unit, Q.note + "  " + ans.note);
                QList.Add(newQ);
            }

            foreach (Item Q in additionalLoads.QList)
            {
                Item ans = ItemMath.Multiply(Qfactor, Q);
                Q.number = ans.number;
                Item newQ = new Item(Q.name, ans.number, Q.unit, Q.note + "  " + ans.note);
                QList.Add(newQ);
            }

        }

        private void CalculateTotalWeight()
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
            Item ans = ItemMath.Add(totalG, totalQ);
            totalWeight = new Item("Total Weight", ans.number, "kN", ans.note);
        }

        private void CreateList1()
        {
            viewList1 = new List<Item>();

            string loadCombinationText = "Load Combination " + Gfactor.number + "G + " + Qfactor.number + "Q";


            Item title1 = new Item("Tributary Area Data", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item title4 = new Item(loadCombinationText, 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(divider);
            viewList1.Add(title4);
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
            viewList1.Add(totalWeight);
        }
    }
}
