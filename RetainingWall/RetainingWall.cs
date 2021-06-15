using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.RetainingWall
{
    class RetainingWall
    {
       // public Item wallHeight;
       // public Item wallStemThickness;
       // public Item baseThickness;
       // public Item soilFrictionAngle;
       // public Item soilUnitWeight;
        //public Item activePressureCoefficient;

        public Item rebarArea;
        public Item crs;
        public Item steelArea;

        public List<Item> viewList1;

        public RetainingWall()
        {
            
        }

        public void SteelAreaCalculation(double rebarArea, double crs)
        {           
            this.rebarArea = new Item("Rebar Cross Sectional Area", rebarArea, "mm²", "");
            this.crs = new Item("Centre to Centre Distance", crs, "mm", "");

            Item ans = ItemMath.MultiplyDivide(new Item(1000, "mm"), this.rebarArea, this.crs);

            steelArea = new Item("Steel Area", ans.number, "mm²", ans.note);

            CreateList();

        }


        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Steel Area Calculation", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(rebarArea);
            viewList1.Add(crs);
            viewList1.Add(steelArea);

        }


    }
}
