using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Wall
    {
        
        public Item height;
        public Item thickness;
        public Item length;
        public Item weightPerMetre;
        public Item totalWeight;
        public Item material;
        public Windows windows;

        public Item grossArea;
        public Item actualArea;

        public List<Item> viewList1;

        public Wall(double height, double length, double thickness, Item material, Windows windows)
        {
            this.height = new Item("Height", height, "m", ""); 
            this.length = new Item("Length", length, "m", "");
            this.thickness = new Item("Thickness", thickness, "m", "");
            this.material = material;
            this.windows = windows;

            Item ans = ItemMath.Multiply(this.height, this.length);
            this.grossArea= new Item("Wall Total Area", ans.number, "m2", ans.note);
            Item ans2 = ItemMath.Subtract(grossArea, windows.totalArea);
            this.actualArea = new Item("Wall Actual Area", ans2.number, "m2", ans2.note);
            Item ans3 = ItemMath.MultiplyMultiply(actualArea, this.thickness, material);
            totalWeight = new Item("Wall Total Weight", ans3.number, "kN", ans3.note);
            Item ans4 = ItemMath.Divide(totalWeight, this.length);
            weightPerMetre = new Item("Wall Weight per Metre", ans4.number, "kN/m", ans4.note);

            CreateList();
        }

        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Wall Data", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(height);
            viewList1.Add(thickness);
            viewList1.Add(length);
            viewList1.Add(material);
            viewList1.Add(divider);
            viewList1.Add(title2);
            viewList1.Add(grossArea);
            viewList1.Add(windows.totalArea);
            viewList1.Add(actualArea);
            viewList1.Add(divider);
            viewList1.Add(title3);
            viewList1.Add(totalWeight);
            viewList1.Add(weightPerMetre);

        }
    }
}
