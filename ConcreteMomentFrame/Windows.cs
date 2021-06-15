using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Windows
    {
        public Item quantity;
        public Item height;
        public Item width;

        public Item material;

        public Item singleWeight;

        public Item totalWeight;
        public Item singleArea;
        public Item totalArea;

        public List<Item> viewList1;


        public Windows(int quantity, double height, double width, Item material)//kN/m2
        {
            this.quantity = new Item("Quantity", quantity, "", "");
            this.height = new Item("Height", height, "m", "");
            this.width = new Item("Width", width, "m", "");
            this.material = material;

            string materialData = this.material.name + " " + this.material.number + this.material.unit;

            Item ans = ItemMath.MultiplyMultiply(this.height, this.width, this.material);
            this.singleWeight = new Class.Item("Weight of Single Window", ans.number, "kN", ans.note + "  " + materialData );

            Item ans2 = ItemMath.Multiply(singleWeight, this.quantity);
            this.totalWeight = new Item("Total Weight of " + this.quantity.number + " Windows", ans2.number, "kN", ans2.note);

            Item ans3 = ItemMath.Multiply(this.height, this.width);
            this.singleArea = new Item("Area of Single Window", ans3.number, "m2", ans3.note);

            Item ans4 = ItemMath.MultiplyMultiply(this.height, this.width, this.quantity);
            this.totalArea = new Item("Total Area of " + this.quantity.number + " Windows", ans4.number, "m2", ans4.note);

            CreateList();
        }

        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Window Data", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(height);
            viewList1.Add(width);
            viewList1.Add(quantity);
            viewList1.Add(material);
            viewList1.Add(divider);
            viewList1.Add(title2);
            viewList1.Add(singleArea);
            viewList1.Add(totalArea);
            viewList1.Add(divider);
            viewList1.Add(title3);
            viewList1.Add(singleWeight);
            viewList1.Add(totalWeight);
        }
    }
}