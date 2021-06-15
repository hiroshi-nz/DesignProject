using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class ConcreteSlab
    {
        Item thickness;
        public Item weight;
        Item material;
        public List<Item> viewList1;

        public ConcreteSlab(Item thickness, Item material)
        {
            this.thickness = thickness;
            this.material = material;
            Item ans = ItemMath.Multiply(thickness, material);
            weight = new Item(thickness.number + "m Thick Concrete Slab Weight", ans.number, "kN/m2", ans.note);
            CreateList();
        }

        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Concrete Slab Data", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(thickness);
            viewList1.Add(material);
            viewList1.Add(weight);

        }
    }
}
