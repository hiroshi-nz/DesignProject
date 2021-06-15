using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.ConcreteMomentFrame
{
    class Beams
    {

        public Item width;
        public Item height;
        public Item length;
        public Item quantity;
        public Item material;
        public Item singleWeight;
        public Item totalWeight;

        //public Item depth;
        //public Item c;
        //public Item alpha;
        //public Item beta;

        

        public Item fy;
        public Item fc;
        public Item b;
        public Item d1;
        public Item d2;
        public Item As1;
        public Item As2;
        public Item a;
        public Item c;
        public Item ec;
        public Item es;
        public Item es2;
        public Item ey;

        public Item Cc;
        public Item Cs;
        public Item Mn;
        public Item phi;
        public Item phiMn;

        public Item p;

        public List<Item> viewList1;
        public List<Item> viewList2;
        public List<Item> viewList3;

        public BeamShearCapacity shearCapacity;

        Item num = new Item("0.85", 0.85, "", "");

        public Beams(int quantity, double height, double width, double length, Item material)
        {
            shearCapacity = new BeamShearCapacity();

            this.quantity = new Item("Quantity", quantity, "", "");
            this.length = new Item("Length", length, "m", "");
            this.height = new Item("Height", height, "m", "");
            this.width = new Item("Width", width, "m", "");
            this.material = material;

            string materialData = this.material.name + " " + this.material.number + this.material.unit;

            Item ans = ItemMath.MultiplyMultiplyMultiply(this.height, this.width, this.length, this.material);
            this.singleWeight = new Class.Item("Weight of Single Beam", ans.number, "kN", ans.note + "  " + materialData);

            Item ans2 = ItemMath.Multiply(singleWeight, this.quantity);
            this.totalWeight = new Item("Total Weight of " + this.quantity.number + " Beams", ans2.number, "kN", ans2.note);

            CreateList();
        }

        public static double CalculateDepth(double height, double cover, double stirrupDiameter, double tensionRebarDiameter)
        {
            double depth = height - cover - stirrupDiameter - tensionRebarDiameter / 2;
            return depth;
        }

        public void SinglyReinforced(double fy, double fc, double b, double d1, double As1)
        {
            this.fy = new Item("Steel Yield Strength", fy, "MPa", "");
            this.fc = new Item("Concrete Crushing Strength", fc, "MPa", "");
            this.b = new Item("Breadth", b, "mm", "");
            this.d1 = new Item("Depth d", d1, "mm", "");
            this.As1 = new Item("Tension Steel Cross Sectional Area", As1, "mm2", "");

            this.phi = new Item("Strength Reduction Factor φ", 0.85, "", "NZS3101:Part1:2006 2.3.2.2  Flexure with or without axial compression");

            CalculateSinglyConcreteStressBlockLength();
            CalculateNeutralAxisDistance();

            CalculateStrains();

            CalculateTensionSteelStrain();

            CalculateConcreteCompressiveLoad();

            CalculateSinglyNominalMomentCapacity();
            Calculatep();

            CreateList2();
        }

        

        public void DoublyReinforced(double fy, double fc, double b, double d1, double d2, double As1, double As2)
        {
            this.fy = new Item("Steel Yield Strength", fy, "MPa", "");
            this.fc = new Item("Concrete Crushing Strength", fc, "MPa", "");
            this.b = new Item("Breadth", b, "mm", "");
            this.d1 = new Item("Depth d", d1, "mm", "");
            this.d2 = new Item("Depth d'", d2, "mm", "");
            this.As1 = new Item("Tension Steel Cross Sectional Area", As1, "mm2", "");
            this.As2 = new Item("Compression Steel Cross Sectional Area", As2, "mm2", "");

            CalculateConcreteStressBlockLength();
            CalculateNeutralAxisDistance();
            CalculateStrains();
            CalculateCompressionSteelStrain();
            CalculateTensionSteelStrain();
            CalculateConcreteCompressiveLoad();
            CalculateSteelCompressiveLoad();
            CalculateNominalMomentCapacity();
            //Calculatep();
            
            CreateList3();
        }

        public void Calculatep()
        {
            //p = As/(bd)
            Item ans = ItemMath.Multiply(b, d1);
            ans = ItemMath.DivideEquation(As1, ans);
            p = new Item("Steel Ratio p", ans.number, "", ans.note);
        }

        private void CalculateConcreteStressBlockLength()
        {
            //a = (As1 * fy - As2 * fy) / (0.85 * b * fc);
            Item ans1 = ItemMath.MultiplySubtractMultiply(this.As1, this.fy, this.As2, this.fy);
            Item ans2 = ItemMath.MultiplyMultiply(num, this.b, this.fc);
            this.a = new Item("a", ans1.number / ans2.number, "mm", "(" + ans1.note + ") / (" + ans2.note + ")");
        }

        private void CalculateSinglyConcreteStressBlockLength()
        {
            //a = (As1 * fy) / (0.85 * b * fc);
            Item ans1 = ItemMath.Multiply(this.As1, this.fy);
            Item ans2 = ItemMath.MultiplyMultiply(num, this.b, this.fc);
            this.a = new Item("a", ans1.number / ans2.number, "mm", "(" + ans1.note + ") / (" + ans2.note + ")");
        }

        private void CalculateNeutralAxisDistance()
        {
            //c = a / 0.85;
            Item ans = ItemMath.Divide(a, num);
            this.c = new Item("c", ans.number, "mm", ans.note);
        }

        private void CalculateStrains()
        {
            Item Es = new Item("Steel Yield Strength", 200000, "MPa", "");
            ec = new Item("Concrete Ultimate Crushing Strain", 0.003, "", "");
            //ey = fy / 200000;
            Item ans = ItemMath.Divide(this.fy, Es);
            this.ey = new Item("Steel Yield Strain", ans.number, "", ans.note);

            ec = new Item("Concrete Ultimate Crushing Strain", 0.003, "", "");
        }

        private void CalculateCompressionSteelStrain()
        {
            //es2 = 0.003 * (c - d2) / c;
            Item ans1 = ItemMath.Subtract(this.c, this.d2);
            Item ans2 = ItemMath.MultiplyEquation(ec, ans1);
            Item ans3 = ItemMath.EquationDivide(ans2, c);

            es2 = new Item("Compression Steel Strain", ans3.number, "", ans3.note);
            if (es2.number > ey.number)//es2 yielded
            {
                es2.note = "Yielding " + MathHelper.RoundDec(es2.number, 6) + " > " + ey.number + "  " + es2.note;
            }
            else
            {
                es2.note = "Not Yielding " + MathHelper.RoundDec(es2.number, 6) + " < " + ey.number + "  " + es2.note;
            }
        }

        private void CalculateTensionSteelStrain()
        {
            //es = 0.003 * (d - c) / c;
            Item ans1 = ItemMath.Subtract(this.d1, this.c);
            Item ans2 = ItemMath.MultiplyEquation(ec, ans1);
            Item ans3 = ItemMath.EquationDivide(ans2, c);

            es = new Item("Tension Steel Strain", ans3.number, "", ans3.note);
            if (es.number > ey.number)
            {
                es.note = "Yielding " + MathHelper.RoundDec(es.number, 6) + " > " + ey.number + "  " + es.note;
            }
            else
            {
                es.note = "Not Yielding " + MathHelper.RoundDec(es.number, 6) + " < " + ey.number + "  " + es.note;
            }
        }

        private void CalculateConcreteCompressiveLoad()
        {
            //Cc = 0.85 * fc * a * b;
            Item ans = ItemMath.MultiplyMultiplyMultiply(num, this.fc, this.a, this.b);
            this.Cc = new Item("Concrete Compressive Load", ans.number / 1000, "kN", ans.note);
        }

        private void CalculateSteelCompressiveLoad()
        {
            //Cs2 = fy * As2;

            Item ans = ItemMath.Multiply(this.fy, this.As2);
            this.Cs = new Item("Steel Compressive Load", ans.number / 1000, "kN", ans.note);
        }

        private void CalculateNominalMomentCapacity()
        {

            //Mn = Cc * (d1 - a / 2) + Cs2 * (d1 - d2);
            Item ans1 = ItemMath.SubtractDivide(this.d1, this.a, new Item("", 2, "", ""));
            Item ans2 = ItemMath.MultiplyEquation(this.Cc, ans1);

            Item ans3 = ItemMath.Subtract(this.d1, this.d2);
            Item ans4 = ItemMath.MultiplyEquation(this.Cs, ans3);

            this.Mn = new Item("Nominal Moment Capacity Mn", (ans2.number + ans4.number) / 1000, "kN", ans2.note + " + " + ans4.note);

            Item ans = ItemMath.Multiply(phi, Mn);

            this.phiMn = new Item("Nominal Moment Capacity φMn", ans.number, "kN", ans.note);
        }

        private void CalculateSinglyNominalMomentCapacity()
        {

            //Mn = Cc * (d1 - a / 2);
            Item ans1 = ItemMath.SubtractDivide(this.d1, this.a, new Item("", 2, "", ""));
            Item ans2 = ItemMath.MultiplyEquation(this.Cc, ans1);

            this.Mn = new Item("Nominal Moment Capacity", (ans2.number) / 1000, "kN", ans2.note);
            Item ans = ItemMath.Multiply(phi, Mn);
            this.phiMn = new Item("Nominal Moment Capacity φMn", ans.number, "kN", ans.note);
        }



        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Beam Data", 0, "", "TITLE");
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
        public void CreateList2()
        {
            viewList2 = new List<Item>();
            Item title1 = new Item("Singly Reinforced Beam Calculation", 0, "", "TITLE");
            Item title2 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList2.Add(title1);
            viewList2.Add(fy);
            viewList2.Add(fc);
            viewList2.Add(b);
            viewList2.Add(d1);
            viewList2.Add(As1);
            viewList2.Add(p);
            viewList2.Add(a);
            viewList2.Add(c);
            viewList2.Add(ec);
            viewList2.Add(es);
            viewList2.Add(ey);
            viewList2.Add(Cc);
            viewList2.Add(Mn);
            viewList2.Add(phi);
            viewList2.Add(phiMn);

        }

        public void CreateList3()
        {
            viewList3 = new List<Item>();
            Item title1 = new Item("Doubly Reinforced Beam Calculation", 0, "", "TITLE");
            Item title2 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList3.Add(title1);
            viewList3.Add(fy);
            viewList3.Add(fc);
            viewList3.Add(b);
            viewList3.Add(d1);
            viewList3.Add(d2);
            viewList3.Add(As1);
            viewList3.Add(As2);
            viewList3.Add(a);
            viewList3.Add(c);
            viewList3.Add(ec);
            viewList3.Add(es2);
            viewList3.Add(ey);
            viewList3.Add(Cc);
            viewList3.Add(Cs);
            viewList3.Add(Mn);
        }
    }
}
