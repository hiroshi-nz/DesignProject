using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.Prestressed
{
    class Prestressed
    {

        public Item width;
        public Item height;
        public Item length;
        public Item e;
        public Item c;
        public Item UDL;

        public Item momentOfInertia;

        public Item As;
        public Item fpu;
        public Item fps;
        public Item pi;


        public Item axialPressure;
        public Item stressFromStrand;
        public Item momentFromUDL;
        public Item stressFromUDL;

        public Item stressTop;
        public Item stressBottom;

        public List<Item> viewList1;

        public Prestressed(double height, double width, double length, double As, double fpu)
        {
            this.length = new Item("Length", length, "mm", "");
            this.height = new Item("Height", height, "mm", "");
            this.width = new Item("Width", width, "mm", "");

            this.As = new Item("Steel Cross Sectional Area", As, "mm²", "");
            this.fpu = new Item("Ultimate Tensile Strength", fpu, "MPa", "");

            MomentOfInertia();
            AxialPressure();

            CreateList();

        }

        public Prestressed(double height, double width, double length, double As, double fpu, double e)
        {
            this.length = new Item("Length", length, "mm", "");
            this.height = new Item("Height", height, "mm", "");
            this.width = new Item("Width", width, "mm", "");

            c = new Item("Centroid Distance", height/2, "mm", "");
            this.e = new Item("Strand Distance from Centroid", e, "mm", "");

            this.As = new Item("Steel Cross Sectional Area", As, "mm²", "");
            this.fpu = new Item("Ultimate Tensile Strength", fpu, "MPa", "");

            MomentOfInertia();
            AxialPressure();
            MomentFromStrand();

            CreateList();

        }

        public Prestressed(double height, double width, double length, double As, double fpu, double e, double UDL)
        {
            this.length = new Item("Length", length, "mm", "");
            this.height = new Item("Height", height, "mm", "");
            this.width = new Item("Width", width, "mm", "");

            c = new Item("Centroid Distance", height / 2, "mm", "");
            this.e = new Item("Strand Distance from Centroid", e, "mm", "");

            this.As = new Item("Steel Cross Sectional Area", As, "mm²", "");
            this.fpu = new Item("Ultimate Tensile Strength", fpu, "MPa", "");

            this.UDL = new Item("UDL", UDL, "kN/m", "");

            MomentOfInertia();
            AxialPressure();
            MomentFromStrand();
            MomentFromUDL();
            TotalStressTopAndBottom();

            CreateList();

        }

        private void TotalStressTopAndBottom()
        {
            //axialpressure, stressFromStrand, stressFromUDL
            Item ans = ItemMath.AddSubtract(axialPressure, stressFromStrand, stressFromUDL);
            stressTop = new Item("Stress on Top", ans.number, "MPa", ans.note);

            ans = ItemMath.SubtractAdd(axialPressure, stressFromStrand, stressFromUDL);
            stressBottom = new Item("Stress on Bottom", ans.number, "MPa", ans.note);
        }

        private void MomentFromUDL()
        {
            //wl2/8
            Item lengthInMetre = new Item("Length", length.number / 1000, "m", "");
            Item ans = ItemMath.SquareWithBrackets(lengthInMetre);
            ans = ItemMath.MultiplyEquationNoBracket(UDL, ans);
            ans = ItemMath.EquationDivideNoBracket(ans, new Item(8));
            momentFromUDL = new Class.Item("Moment from UDL", ans.number, "kN⋅m", ans.note);

            ans = ItemMath.MultiplyDivide(momentFromUDL, c, momentOfInertia);
            stressFromUDL = new Item("Stress from UDL Moment", ans.number * 1000000, "MPa", ans.note);
        }


        private void AxialPressure()
        {
            Item ans = ItemMath.Multiply(this.fpu, new Item(0.7));
            this.fps = new Item("Reduced Ultimate Tensile Strength", ans.number, "MPa", ans.note);

            ans = ItemMath.Multiply(this.As, fps);
            this.pi = new Item("Axial Force", ans.number, "N", ans.note);

            ans = ItemMath.Multiply(this.width, this.height);
            ans = ItemMath.DivideEquation(pi, ans);
            axialPressure = new Item("Axial Pressure", -ans.number, "MPa", ans.note);
        }

        private void MomentFromStrand()
        {
            Item ans = ItemMath.MultiplyMultiplyDivide(pi, e, c, momentOfInertia);
            stressFromStrand = new Class.Item("Stress from Moment of Strand", ans.number, "MPa", ans.note);

        }

        private void MomentOfInertia()
        {
            Item ans = ItemMath.CubeWithBrackets(height);
            ans = ItemMath.EquationMultiplyNoBracket(ans, width);
            ans = ItemMath.EquationDivideNoBracket(ans, new Item(12));
            momentOfInertia = new Item("Moment of Inertia X", ans.number, "mm⁴", ans.note);

        }


        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Prestressed Concrete Calculation", 0, "", "TITLE");
            Item title2 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(height);
            viewList1.Add(width);
            viewList1.Add(length);
            viewList1.Add(momentOfInertia);
            viewList1.Add(fpu);
            viewList1.Add(fps);
            viewList1.Add(pi);
            viewList1.Add(axialPressure);
            viewList1.Add(stressFromStrand);
            viewList1.Add(UDL);
            viewList1.Add(momentFromUDL);
            viewList1.Add(stressFromUDL);

            viewList1.Add(stressTop);
            viewList1.Add(stressBottom);
        }

    }
}
