using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.ConcreteMomentFrame;

namespace CivilApp.Class.Earthquake
{
    class EquivalentStaticForces
    {
        public List<Floor> floorList;
        public Floor roof;

        public List<Item> whList;
        public Item totalwh;
        public List<Item> eq2List;
        public List<Item> equivalentForceList;

        public Item totalSeismicWeight;
        public Item horizontalSeismicShear;

        public List<Item> viewList1;

        Item CdT1;

        public EquivalentStaticForces(Item CdT1)
        {
            this.CdT1 = CdT1;
            floorList = new List<Floor>();

        }

        public void TotalSeismicWeight()
        {
            totalSeismicWeight = new Item("Total Seismic Weight", 0, "kN", "");
            foreach (Floor floor in floorList)
            {
                totalSeismicWeight.number += floor.seismicWeight.seismicWeight.number;
            }
            totalSeismicWeight.number += roof.seismicWeight.seismicWeight.number;
        }

        public void HorizontalSeismicShear()
        {
            Item ans = ItemMath.Multiply(CdT1, totalSeismicWeight);
            horizontalSeismicShear = new Item("Horizontal Seismic Shear V", ans.number, "kN", "V = Cd(T1)Wt  " + ans.note);
        }


        public void Solve()
        {
            CalculateWh();
            CalculateEq2();

            TotalSeismicWeight();
            HorizontalSeismicShear();

            EquivalentStaticHorizontalForce();

            CreateList();
        }

        public void AddFloor(Floor floor)
        {
            floorList.Add(floor);
        }

        public void AddRoof (Floor roof)
        {
            this.roof = roof;
        }

        public void CalculateWh()
        {
            whList = new List<Item>();
            Item wh = new Class.Item("", 0, "", "");
            Item ans = new Class.Item("", 0, "", "");

            int counter = 1;
            foreach (Floor floor in floorList)
            {
                ans = ItemMath.Multiply(floor.seismicWeight.seismicWeight, floor.seismicHeight);
                wh = new Class.Item("Floor " + counter + " Weight x Height", ans.number, "kN.m", ans.note);

                whList.Add(wh);
                counter++;
            }

            ans = ItemMath.Multiply(roof.seismicWeight.seismicWeight, roof.seismicHeight);
            wh = new Class.Item("Roof Weight x Height", ans.number, "kN.m", ans.note);
            whList.Add(wh);

            totalwh = new Item("Total Weight x Height", 0, "kN.m", "");
            foreach (Item item in whList)
            {
                totalwh.number += item.number;
            }
        }

        public void CalculateEq2()
        {
            eq2List = new List<Item>();
            foreach (Item item in whList)
            {
                Item ans = ItemMath.Divide(item, totalwh);
                Item eq2 = new Item("wh/sum(wh)", ans.number, "", ans.note);
                eq2List.Add(eq2);
            }

        }

        public void EquivalentStaticHorizontalForce()
        {
            equivalentForceList = new List<Item>();

            foreach (Item item in eq2List)
            {
                Item item1 = new Item("", 0.92, "", "");
                Item ans = ItemMath.MultiplyMultiply(item1, horizontalSeismicShear, item);
                Item equivalentForce = new Item("Equivalent Force", ans.number, "kN", ans.note);
                equivalentForceList.Add(equivalentForce);
            }

            Item roof = equivalentForceList.Last();

            Item item2 = new Item("", 0.08, "", "");
            Item ans2 = ItemMath.AddMultiply(roof, item2, horizontalSeismicShear);
            
            roof.note = ans2.note + "  " + MathHelper.RoundDec(roof.number, 2) + " = " + roof.note;
            roof.number = ans2.number;
        }

        public void CreateList()
        {
            Item title1 = new Item("Equitalent Statics Forces Calculation", 0, "", "TITLE");
            Item title2 = new Item("Weight x Height Calculation", 0, "", "TITLE");
            Item title3 = new Item("Wh/sum(wh) Calculation", 0, "", "TITLE");
            //Item title4 = new Item("Wh/sum(wh) Calculation", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1 = new List<Item>();
            viewList1.Add(title1);
            viewList1.Add(divider);

            int counter = 1;
            foreach(Floor floor in floorList)
            {
                Item item = new Item("Floor " + counter + " Seismic Weight", floor.seismicWeight.seismicWeight.number, floor.seismicWeight.seismicWeight.unit, floor.seismicWeight.seismicWeight.note);
                viewList1.Add(item);
                counter++;
            }

            viewList1.Add(roof.seismicWeight.seismicWeight);

            viewList1.Add(totalSeismicWeight);
            viewList1.Add(horizontalSeismicShear);

            viewList1.Add(divider);
            viewList1.Add(title2);

            foreach (Item item in whList)
            {
                viewList1.Add(item);
            }
            viewList1.Add(totalwh);
            viewList1.Add(divider);

            viewList1.Add(title3);

            foreach (Item item in eq2List)
            {
                viewList1.Add(item);
            }

            viewList1.Add(divider);

            foreach (Item item in equivalentForceList)
            {
                viewList1.Add(item);
            }
        }
    }
}
