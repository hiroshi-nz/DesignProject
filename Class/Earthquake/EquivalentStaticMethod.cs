using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.Earthquake
{
    class EquivalentStaticMethod
    {
        public Item kt;
        public Item hn;
        public Item T1ULS;
        public Item T1SLS;
        List<Item> itemList;

        public Item CdT1;
        public Item Sp;
        public Item ku;
        public Item u;

        public Item eq1;
        public Item eq2;

        public List<Item> viewList1;

        public EquivalentStaticMethod(Item kt, Item hn)
        {
            //itemList = new List<Item>();

            this.kt = kt;
            //itemList.Add(kt);
            this.hn = hn;
            //itemList.Add(hn);

            Item T1SLS = new Item("T1(SLS)", 1.0 * kt.number * Math.Pow(hn.number, 0.75), "s", "NZS1170.5 Supp 1:2004 C4.1.2.1");
            this.T1SLS = T1SLS;
            //itemList.Add(T1SLS);

            Item T1ULS = new Item("T1(ULS)", 1.25 * kt.number * Math.Pow(hn.number, 0.75), "s", "NZS1170.5 Supp 1:2004 C4.1.2.1");
            this.T1ULS = T1ULS;
            //itemList.Add(T1ULS);
            //Print(itemList);
        }


        public void HorizontalDesignActionCoefficient(ElasticSiteSpectra elasticSiteSpectra, string subSoilClass, Item u)// only for ULS right now
        {
            ku = Calculateku(T1ULS, subSoilClass, u);
            Sp = StructuralPerformanceFactor(u);

            string note1 = "(" + elasticSiteSpectra.Z.number + "/20 + 0.02) x " + elasticSiteSpectra.Ru.number;
            eq1 = new Item("(Z/20 + 0.02)Ru", (elasticSiteSpectra.Z.number / 20 + 0.02) * elasticSiteSpectra.Ru.number, "", note1);
            string note2 = "0.03 x " + elasticSiteSpectra.Ru.number;
            eq2 = new Item("0.03Ru", 0.03 * elasticSiteSpectra.Ru.number, "", "NZS1170.5:2004 5.2.1.1  " + note2);
            Item ans = ItemMath.MultiplyDivide(elasticSiteSpectra.CTULS, Sp, ku);


            if(ans.number >= eq1.number && ans.number> eq2.number)//tested once, working OK
            {
                CdT1 = new Item("Horizontal Design Action Coefficient Cd(T1)", ans.number, "", "NZS1170.5:2004 5.2.1.1  " + ans.note + " >= " + note1 + " & " + note2);
            }
            else
            {
                if(eq1.number > eq2.number)
                {
                    CdT1 = new Item("Horizontal Design Action Coefficient Cd(T1)", eq1.number, "", "NZS1170.5:2004 5.2.1.1  " + ans.note + " & " + note2 + " <= " + note1);
                }
                else
                {
                    CdT1 = new Item("Horizontal Design Action Coefficient Cd(T1)", eq2.number, "", "NZS1170.5:2004 5.2.1.1  " + ans.note + " & " + note1 + " <= " + note2);
                }    
            }
            CreateList2();
        }

        public Item Calculateku(Item T1, string subSoilClass, Item u)
        {
            Item ku = new Item("ku NOTSET", 0, "", "NOTSET");
            this.u = u;

            if(subSoilClass == "A" || subSoilClass == "B" || subSoilClass == "C" || subSoilClass == "D")
            {
                if (T1.number >= 0.7)
                {
                    ku = new Item("ku", u.number, "", "NZS1170.5:2004 5.2.1.1  T1 => 0.7s");
                }
                else
                {
                    double ans = (((u.number - 1) * T1.number) / 0.7) + 1;
                    ku = new Item("ku", ans, "", "NZS1170.5:2004 5.2.1.1  T1 < 0.7s");
                }

            }
            else
            {
                ku = new Item("ku ERROR", u.number, "", "ERROR Subsoil class E not implemented");
            }

            return ku;
        }

        public Item StructuralPerformanceFactor(Item u)// 0.7 for SLS
        {
            Item Sp = new Item("NOTSET", 0, "", "NOT SET");
            if(u.number > 1 && u.number < 2)
            {
                Item item1 = new Item("", 1.3, "", "");
                Item item2 = new Item("", 0.3, "", "");
                Item ans = ItemMath.SubtractMultiply(item1, item2, u);
                Sp = new Item("Structural Performance Factor(Sp)", ans.number, "", "For ULS  NZS1170.5:2004 4.4.2  " + ans.note);
            }
            else if(u.number >=  2)
            {
                Sp = new Item("Structural Performance Factor(Sp)", 0.7, "", "For ULS  NZS1170.5:2004 4.4.2  u >= 2.0");
            }
            else if(u.number <= 1)
            {
                Sp = new Item("Structural Performance Factor(Sp)", 0.7, "", "For ULS  NZS1170.5:2004 4.4.2  u <= 1.0");
            }

            return Sp;
        }


        public List<Item> CreateList()
        {
            itemList = new List<Item>();
            itemList.Add(kt);
            itemList.Add(hn);
            itemList.Add(T1SLS);
            itemList.Add(T1ULS);

            return itemList;
        }

        private void CreateList2()
        {
            Item title1 = new Item("Horizontal Design Action Coefficient Calculation", 0, "", "TITLE");
            Item title2 = new Item("Permanent Load", 0, "", "TITLE");
            Item title3 = new Item("Imposed Load", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1 = new List<Item>();
            viewList1.Add(title1);
            viewList1.Add(divider);
            viewList1.Add(u);
            viewList1.Add(ku);
            viewList1.Add(Sp);
            viewList1.Add(eq1);
            viewList1.Add(eq2);
            viewList1.Add(CdT1);

        }



        private static void Print(List<Item> list)
        {
            foreach (Item item in list)
            {
                Console.WriteLine(item.name + ":" + MathHelper.Round3dec(item.number) + item.unit + ":" + item.note);
            }
        }
    }
}
