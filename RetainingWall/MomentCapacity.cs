using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.RetainingWall
{
    class MomentCapacity
    {
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
        public List<Item> viewList2;

        Item num = new Item("0.85", 0.85, "", "");

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


        private void CalculateSinglyNominalMomentCapacity()
        {

            //Mn = Cc * (d1 - a / 2);
            Item ans1 = ItemMath.SubtractDivide(this.d1, this.a, new Item("", 2, "", ""));
            Item ans2 = ItemMath.MultiplyEquation(this.Cc, ans1);

            this.Mn = new Item("Nominal Moment Capacity", (ans2.number) / 1000, "kN", ans2.note);
            Item ans = ItemMath.Multiply(phi, Mn);
            this.phiMn = new Item("Nominal Moment Capacity φMn", ans.number, "kN", ans.note);
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
            //viewList2.Add(ec);
            viewList2.Add(es);
            //viewList2.Add(ey);
            viewList2.Add(Cc);
            viewList2.Add(Mn);
            //viewList2.Add(phi);
            viewList2.Add(phiMn);

        }


    }
}
