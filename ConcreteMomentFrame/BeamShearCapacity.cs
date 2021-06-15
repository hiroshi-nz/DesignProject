using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;


namespace CivilApp.ConcreteMomentFrame
{
    class BeamShearCapacity
    {
        public Item fy;
        public Item fc;
        public Item b;
        public Item d;
        public Item ka;
        public Item kd;
        public Item pw;
        public Item vb;
        public Item vb1;
        public Item vb2;
        public Item vb3;
        public Item vc;
        public Item Vc;

        public Item VDemand;
        public Item VsDemand;
        public Item phi;

        public Item As;
        public Item Av;
        public Item s;
        public Item Vs;

        public Item smax;
        public Item smaxCriteria;
        public Item smax1;
        public Item smax2;
        public Item smax3;

        public Stirrups stirrups;

        public List<Item> viewList1;
        public List<Item> viewList2;
        public List<Item> viewList3;

        public Item squareRootConcrete;


        private void Calculatepw()
        {
            Item ans = ItemMath.Multiply(b, d);
            ans = ItemMath.DivideEquation(As, ans);
            this.pw = new Item("Tension Reinforcement Ratio in Web pw", ans.number, "", "As/(bwd) " + ans.note);
        }

        private void CalculateAv()
        {

            this.Av = new Item("Av", stirrups.totalLegArea, "mm2", "");
        }

        public void AssignStirrups(double diameter, int numberOfLegs)
        {
            stirrups = new Stirrups("", diameter, numberOfLegs);

        }

        public void ShearCalculation(double VDemand, double fy, double fc, double b, double d, double ka, double As)
        {
            this.fy = new Item("Steel Yield Strength", fy, "MPa", "");
            this.fc = new Item("Concrete Crushing Strength", fc, "MPa", "");
            this.b = new Item("Breadth", b, "mm", "");
            this.d = new Item("Depth d", d, "mm", "");
            
            this.VDemand = new Item("Shear Demand V*", VDemand, "kN", "");

            squareRootConcrete = new Item("√f'c", Math.Sqrt(fc), "MPa", "√" + fc);

            this.ka = new Item("ka", ka, "", "NZS3101:Part1:2006 9.3.9.3.4   Maximum Aggregate Size = 10mm");
            this.kd = new Item("kd'", 1, "", "NZS3101:Part1:2006 9.3.9.3.4");

            Item ans = new Item("", 0, "", "");

            phi = new Item("Reduction Factor φ", 0.75, "", "NZS3101:Part1:2006 2.3.2.2  Shear and torsion");

            this.As = new Item("Longitudinal Reinforcement Area", As, "mm2", "");

            Calculatepw();


            Calculatevb();
            CalculateAv();

            ans = ItemMath.MultiplyMultiply(this.ka, kd, vb);
            this.vc = new Item("vc", ans.number, "MPa", "NZS3101:Part1:2006 9.3.9.3.4 Eq.9-5  " + ans.note);

            ans = ItemMath.MultiplyMultiply(vc, this.b, this.d);
            Vc = new Item("Vc", ans.number/1000, "kN", "NZS3101:Part1:2006 9.3.9.3.4 Eq.9-4  " + ans.note);

            ans = ItemMath.DivideSubtract(this.VDemand, phi, Vc);
            VsDemand = new Item("Steel Shear Demand V*/φ - Vc", ans.number, "kN", ans.note);

            ans = ItemMath.MultiplyMultiply(this.Av, this.fy, this.d);
            ans = ItemMath.EquationDivide(ans, VsDemand);
            s = new Item("Stirrup Spacing", ans.number/1000, "mm", ans.note);


            ans = ItemMath.MultiplyMultiplyDivide(this.Av, this.fy, this.d, s);
            Vs = new Item("Steel Shear Capacity", ans.number/1000, "kN", ans.note);

            FindMaximumSpacing();

            CreateList();
        }

        public void FindMaximumSpacing()
        {
            Item ans = ItemMath.EquationMultiplyEquationNoBracket(new Item("", 0.33333, "", "1/3"), squareRootConcrete);
            ans = ItemMath.EquationMultiplyNoBracket(ans, this.b);
            ans = ItemMath.EquationMultiplyNoBracket(ans, this.d);
            smaxCriteria = new Item("Maximum Spacing Criteria", ans.number/1000, "kN", ans.note);
            if(Vs.number <= smaxCriteria.number)
            {
                smax1 = new Item("smax1", 600, "mm", "600");

                ans = ItemMath.Multiply(new Item(0.5), d);
                smax2 = new Item("smax2", ans.number, "mm", ans.note);

                ans = ItemMath.MultiplyMultiply(new Item(16), Av, fy);
                Item ans2 = ItemMath.MultiplyEquation(b, squareRootConcrete);
                ans = ItemMath.EquationDivideEquation(ans, ans2);
                smax3 = new Item("smax3", ans.number, "mm", ans.note);

                ans = ItemMath.Min(smax1, smax2, smax3);
                smax = new Item("Maximum Stirrup Spacing smax", ans.number, "mm", Vs.NumberWithUnit() + "≤" + smaxCriteria.NumberWithUnit());

            }
            else
            {
                smax1 = new Item("smax1", 300, "mm", "300");

                ans = ItemMath.Multiply(new Item(0.25), d);
                smax2 = new Item("smax2", ans.number, "mm", ans.note);

                ans = ItemMath.MultiplyMultiply(new Item(16), Av, fy);
                Item ans2 = ItemMath.MultiplyEquation(b, squareRootConcrete);
                ans = ItemMath.EquationDivideEquation(ans, ans2);
                smax3 = new Item("smax3", ans.number, "mm", ans.note);

                ans = ItemMath.Min(smax1, smax2, smax3);
                smax = new Item("Maximum Stirrup Spacing smax", ans.number, "mm", Vs.NumberWithUnit() + " > " + smaxCriteria.NumberWithUnit());
            }


   

        }

        private void Calculatevb()
        {
            int decimalPlace = 3;

            Item ans = ItemMath.Multiply(new Item(0.2), squareRootConcrete);
            vb1 = new Item("vb1", ans.number, "MPa", ans.note);

            ans = ItemMath.EquationMultiply(ItemMath.AddMultiply(new Item(0.07), new Item(10), pw), squareRootConcrete);
            vb2 = new Item("vb2", ans.number, "MPa", ans.note);

            ans = ItemMath.Multiply(new Item(0.08), squareRootConcrete);
            vb3 = new Item("vb3", ans.number, "MPa", ans.note);

            if (vb1.number >= vb3.number || vb2.number >= vb3.number)
            {
                if (vb1.number < vb2.number)
                {
                    vb = new Item("vb", vb1.number, "MPa", MathHelper.RoundDec(vb1.number, decimalPlace) + " < " + MathHelper.RoundDec(vb2.number, decimalPlace));
                }
                else
                {
                    vb = new Item("vb", vb2.number, "MPa", MathHelper.RoundDec(vb2.number, decimalPlace) + " < " + MathHelper.RoundDec(vb1.number, decimalPlace));
                }
            }
            else//one of them is less than vb3
            {
                if (vb1.number >= vb3.number)
                {
                    vb = new Item("vb", vb1.number, "MPa", MathHelper.RoundDec(vb1.number, decimalPlace) + " >= " + MathHelper.RoundDec(vb3.number, decimalPlace));
                }
                else if (vb2.number >= vb3.number)
                {
                    vb = new Item("vb", vb2.number, "MPa", MathHelper.RoundDec(vb2.number, decimalPlace) + " >= " + MathHelper.RoundDec(vb3.number, decimalPlace));
                }
                else
                {
                    vb = new Item("vb", vb3.number, "MPa", vb3.note);
                }
            }
        }

        public static double CalculateDepth(double height, double cover, double stirrupDiameter, double tensionRebarDiameter)
        {
            double depth = height - cover - stirrupDiameter - tensionRebarDiameter / 2;
            return depth;
        }

        private void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Shear Capacity", 0, "", "TITLE");
            Item title2 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(fy);
            viewList1.Add(fc);
            viewList1.Add(b);
            viewList1.Add(d);
            viewList1.Add(ka);
            viewList1.Add(kd);
            viewList1.Add(pw);
            viewList1.Add(vb);
            viewList1.Add(vb1);
            viewList1.Add(vb2);
            viewList1.Add(vb3);
            viewList1.Add(vc);
            viewList1.Add(Vc);
            viewList1.Add(VDemand);
            viewList1.Add(phi);
            viewList1.Add(VsDemand);
            viewList1.Add(Av);
            viewList1.Add(s);
            viewList1.Add(Vs);
            viewList1.Add(smaxCriteria);
            viewList1.Add(smax1);
            viewList1.Add(smax2);
            viewList1.Add(smax3);
            viewList1.Add(smax);
        }
    }
}
