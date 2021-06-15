using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;
using CivilApp.Class.Earthquake;

namespace CivilApp.ConcreteMomentFrame
{
    class Floor
    {
        public Item width;
        public Item length;
        public Item area;
        public Pressures pressures;
        public Loads loads;
        public Item TotalG;
        public Item TotalQ;

        public SeismicWeight seismicWeight;
        public Item seismicHeight;

        public List<Item> viewList1;
        public List<Item> viewList2;

        public Floor(double length, double width)
        {
            pressures = new Pressures();
            this.width = new Class.Item("Width", width, "m", "");
            this.length = new Class.Item("Length", length, "m", "");
            FindArea();
        }

        public void CalculateLoads()
        {
            loads = pressures.CalculateLoads(area);
            TotalG = loads.TotalG(1);
            TotalQ = loads.TotalQ(1);
            CreateList();
            CreateList2();
        }

        public Floor CreateTributaryArea(double length, double width)
        {
            Floor tributaryArea = new Floor(length, width);

            tributaryArea.pressures = pressures;
            tributaryArea.CalculateLoads();
            return tributaryArea;
        }


        public void FindArea()
        {
            Item ans = ItemMath.Multiply(length, width);
            area = new Item("Floor Area", ans.number, "m2", ans.note + " Without Subtracting Columns and Walls");
        }

        public void CreateList()
        {
            Item title1 = new Item("Load Data", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1 = new List<Item>();
            viewList1.Add(title1);
            viewList1.Add(area);

            viewList1.Add(divider);
            viewList1.Add(title2);

            foreach (Item item in loads.GList)
            {
                viewList1.Add(item);
            }
            viewList1.Add(TotalG);

            viewList1.Add(divider);
            viewList1.Add(title3);

            foreach (Item item in loads.QList)
            {
                viewList1.Add(item);
            }
            viewList1.Add(TotalQ);
        }

        public void CreateList2()
        {
            Item title1 = new Item("Pressure Data", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList2 = new List<Item>();
            viewList2.Add(title1);
            //viewList2.Add(area);

            viewList2.Add(divider);
            viewList2.Add(title2);

            foreach (Item item in pressures.GList)
            {
                viewList2.Add(item);
            }
            viewList2.Add(pressures.TotalG(1));

            viewList2.Add(divider);
            viewList2.Add(title3);

            foreach (Item item in pressures.QList)
            {
                viewList2.Add(item);
            }
            viewList2.Add(pressures.TotalQ(1));
        }

        public void CreateList3()
        {
            Item title1 = new Item("Load Data", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1 = new List<Item>();
            viewList1.Add(title1);
            viewList1.Add(area);

            viewList1.Add(divider);
            viewList1.Add(title2);

            foreach (Item item in loads.GList)
            {
                viewList1.Add(item);
            }
            viewList1.Add(TotalG);

            viewList1.Add(divider);
            viewList1.Add(title3);

            foreach (Item item in loads.QList)
            {
                viewList1.Add(item);
            }
            viewList1.Add(TotalQ);
        }

    }
}
