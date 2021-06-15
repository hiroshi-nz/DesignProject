using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;

namespace CivilApp.RetainingWall
{
    class ActiveThrust
    {
        public Item height;

        public Item activePressureCoefficient;
        public Item soilUnitWeight;
        public Item surchargePressure;

        public Item backfillActiveThrust;
        public Item surchargeActiveThrust;
        public Item totalActiveThrust;

        public Item loadFactor;
        public Item factoredBackfillActiveThrust;
        public Item factoredSurchargeActiveThrust;
        public Item factoredTotalActiveThrust;

        public Item backfillActiveThrustMoment;
        public Item surchargeActiveThrustMoment;
        public Item activeThrustLocation;

        public Item factoredBackfillActiveThrustMoment;
        public Item factoredSurchargeActiveThrustMoment;
        public Item factoredTotalActiveThrustMoment;

        public List<Item> viewList1;
        public List<Item> viewList2;

        public ActiveThrust(double height, double soilUnitWeight, double activePressureCoefficient, double surchargePressure)
        {
            this.height = new Item("Height", height, "m", "");
            this.soilUnitWeight = new Class.Item("Unit Weight of Soil", soilUnitWeight, "kN/m³", "");
            this.activePressureCoefficient = new Item("Active Pressure Coefficient", activePressureCoefficient, "", "");
            this.surchargePressure = new Class.Item("Surcharge Pressure", surchargePressure, "kPa", "");

            BackfillActiveThrust();
            SurchargeActiveThrust();
            Item ans = ItemMath.Add(backfillActiveThrust, surchargeActiveThrust);
            totalActiveThrust = new Item("Total Active Thrust per Metre of Wall", ans.number, "kN/m", ans.note);
            ActiveThrustLocation();
            FactoredActiveThrust();
            FactoredActiveThrustMoment();
            CreateList();
            CreateList2();

        }
        private void FactoredActiveThrustMoment()
        {

            loadFactor = new Item("Load Factor for Static Active Earth Thrusts", 1.5, "", "");
            //loadFactor = new Item("Load Factor for Static Active Earth Thrusts", 1.6, "", "");
            Item ans = ItemMath.Multiply(backfillActiveThrustMoment, loadFactor);
            factoredBackfillActiveThrustMoment = new Item("Factored Active Thrust Monent from Backfill", ans.number, "kN⋅m", ans.note);

            ans = ItemMath.Multiply(surchargeActiveThrustMoment, loadFactor);
            factoredSurchargeActiveThrustMoment = new Item("Factored Active Thrust Moment from Surcharge", ans.number, "kN⋅m", ans.note);

            ans = ItemMath.Add(factoredBackfillActiveThrustMoment, factoredSurchargeActiveThrustMoment);

            factoredTotalActiveThrustMoment = new Item("Factored Total Active Thrust Moment per Metre of Wall", ans.number, "kN⋅m", ans.note);

        }

        private void ActiveThrustLocation()
        {
            Item ans = ItemMath.MultiplyDivide(backfillActiveThrust, height, new Class.Item(3));
            backfillActiveThrustMoment = new Class.Item("Backfill Active Thrust Moment", ans.number, "kN⋅m", ans.note);

            ans = ItemMath.MultiplyDivide(surchargeActiveThrust, height, new Class.Item(2));
            surchargeActiveThrustMoment = new Item("Surcharge Active Thrust Moment", ans.number, "kN⋅m", ans.note);

            ans = ItemMath.EquationAddEquationNoBracket(backfillActiveThrustMoment, surchargeActiveThrustMoment);
            ans = ItemMath.EquationDivide(ans, totalActiveThrust);
            activeThrustLocation = new Item("Location of Active Thrust", ans.number, "m", ans.note);

        }

        private void BackfillActiveThrust()
        {
            //Ka x gamma x h^2/2

            Item ans = ItemMath.Square(height);
            ans = ItemMath.EquationDivideNoBracket(ans, new Item(2));
            Item ans2 = ItemMath.Multiply(activePressureCoefficient, soilUnitWeight);
            ans = ItemMath.EquationMultiplyEquationNoBracket(ans2, ans);

            backfillActiveThrust = new Item("Active Thrust per Metre from Backfill", ans.number, "kN/m", ans.note);
        }

        private void SurchargeActiveThrust()
        {
            //Ka x kPa x h
            Item ans = ItemMath.MultiplyMultiply(activePressureCoefficient, surchargePressure, height);
            surchargeActiveThrust = new Item("Active Thrust per Metre from Surcharge", ans.number, "kN/m", ans.note);

        }

        private void FactoredActiveThrust()
        {

            loadFactor = new Item("Load Factor for Static Active Earth Thrusts", 1.5, "", "");
            //loadFactor = new Item("Load Factor for Static Active Earth Thrusts", 1.6, "", "");
            Item ans = ItemMath.Multiply(backfillActiveThrust, loadFactor);
            factoredBackfillActiveThrust = new Item("Factored Active Thrust from Backfill", ans.number, "kN/m", ans.note);

            ans = ItemMath.Multiply(surchargeActiveThrust, loadFactor);
            factoredSurchargeActiveThrust = new Item("Factored Active Thrust from Surcharge", ans.number, "kN/m", ans.note);

            ans = ItemMath.Add(factoredBackfillActiveThrust, factoredSurchargeActiveThrust);

            factoredTotalActiveThrust = new Item("Factored Total Active Thrust per Metre of Wall", ans.number, "kN/m", ans.note);
            
        }



        public void CreateList()
        {
            viewList1 = new List<Item>();
            Item title1 = new Item("Active Thrust", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList1.Add(title1);
            viewList1.Add(height);
            viewList1.Add(soilUnitWeight);
            viewList1.Add(activePressureCoefficient);
            viewList1.Add(surchargePressure);
            viewList1.Add(backfillActiveThrust);
            viewList1.Add(surchargeActiveThrust);
            viewList1.Add(totalActiveThrust);
            viewList1.Add(backfillActiveThrustMoment);
            viewList1.Add(surchargeActiveThrustMoment);
            viewList1.Add(activeThrustLocation);

            viewList1.Add(loadFactor);
            viewList1.Add(factoredBackfillActiveThrust);
            viewList1.Add(factoredSurchargeActiveThrust);
            viewList1.Add(factoredTotalActiveThrust);
        }

        public void CreateList2()
        {
            viewList2 = new List<Item>();
            Item title1 = new Item("Total Active Thrust Moment", 0, "", "TITLE");
            Item title2 = new Item("Area", 0, "", "TITLE");
            Item title3 = new Item("Weight", 0, "", "TITLE");
            Item divider = new Item("", 0, "", "IGNORE");

            viewList2.Add(title1);
            //viewList2.Add(height);
            //viewList2.Add(soilUnitWeight);
            //viewList2.Add(activePressureCoefficient);
            //viewList2.Add(surchargePressure);
            viewList2.Add(backfillActiveThrust);
            viewList2.Add(surchargeActiveThrust);
            viewList2.Add(totalActiveThrust);
            viewList2.Add(backfillActiveThrustMoment);
            viewList2.Add(surchargeActiveThrustMoment);

            viewList2.Add(loadFactor);
            viewList2.Add(factoredBackfillActiveThrustMoment);
            viewList2.Add(factoredSurchargeActiveThrustMoment);
            viewList2.Add(factoredTotalActiveThrustMoment);
        }
    }
}
