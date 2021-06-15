using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Columns
    {

        public Item width;
        public Item height;
        public Item length;
        public Item quantity;
        public Item material;
        public Item singleWeight;
        public Item totalWeight;
        public List<Item> viewList1;

        public Columns(int quantity, double height, double width, double length, Item material)
        {
            double weight = height * width * length * material.number;

            string txt = "Depth:" + height + "m  Width:" + width + "m  Length:" + length + "m  Material:" + material.name;

            this.singleWeight = new Item("Column", weight, "kN", txt);
            this.totalWeight = new Item("Column", weight * quantity, "kN", "Quantity:" + quantity + "  " + txt);


            this.quantity = new Item("Quantity", quantity, "", "");
            this.length = new Item("Length", length, "m", "");
            this.height = new Item("Height", height, "m", "");
            this.width = new Item("Width", width, "m", "");
            this.material = material;

            string materialData = this.material.name + " " + this.material.number + this.material.unit;

            Item ans = ItemMath.MultiplyMultiplyMultiply(this.height, this.width, this.length, this.material);
            this.singleWeight = new Class.Item("Weight of Single Column", ans.number, "kN", ans.note + "  " + materialData);

            Item ans2 = ItemMath.Multiply(singleWeight, this.quantity);
            this.totalWeight = new Item("Total Weight of " + this.quantity.number + " Columns", ans2.number, "kN", ans2.note);

            CreateList();
        }


        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Column Data", 0, "", "TITLE");
            Item title2 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(height);
            viewList1.Add(width);
            viewList1.Add(length);
            viewList1.Add(quantity);
            viewList1.Add(material);
            viewList1.Add(divider);
            viewList1.Add(title2);
            viewList1.Add(singleWeight);
            viewList1.Add(totalWeight);
        }
    }

}
