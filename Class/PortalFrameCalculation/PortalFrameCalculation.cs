using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CivilApp.Class.PortalFrameCalculation;
using CivilApp.Class.PortalFrameCalculation.QandG;

namespace CivilApp.Class.PortalFrameCalculation
{

    class PortalFrameCalculation
    {
        private PortalFrameArea portalFrameArea;

        public List<Item> itemList;

        public PortalFrameCalculation(PortalFrameArea portalFrameArea)
        {
            itemList = new List<Item>();
            
            this.portalFrameArea = portalFrameArea;

            double purlinSpacing = 0.6;//up to 1.8m spacing

            PurlinCalculation(purlinSpacing);
            RafterCalculation(purlinSpacing);

            for (double d = 0.6; d < 1.8; d+=0.2)
            {
                PurlinCalculation(d);
                //RafterCalculation(d);
            }
        }

        public void PurlinCalculation(double purlinSpacing)
        {
            double g = 9.81;
            
            Item tributaryAreaPurlin = new Item("Purlin Tributary Area", purlinSpacing * portalFrameArea.spacing, "m2", "");
            
            LoadItem loadItem = new LoadItem("N/m");
            
            Item ThermoPanel70 = new Item("ThermoPanel EPS 70mm", 11.60 * g, "Pa", "");
            Item MSSPurlin18 = new Item("MSS 200/18", 5.62 * g, "N/m", "typ. span 7.5-10.5m from Metalcraft Roofing");
            Item QRoof = new Item("Roof", RoofActions.StructuralElements(tributaryAreaPurlin.number), "Pa", "");
            Item windPressure = new Item("Wind Pressure", -1039.99, "Pa", "- 1163.45Pa * 0.63 - 828.64Pa *0.37"); 

            loadItem.Q = new Item("Q UDL", purlinSpacing * QRoof.number, "N/m", "");

            loadItem.P.Add(new Item("ThermoPanel UDL", purlinSpacing * ThermoPanel70.number, "N/m", "ThermoPanel EPS 70mm"));
            loadItem.P.Add(new Item("MSS 200/18", 5.62 * g, "N/m", "typ. span 7.5-10.5m from Metalcraft Roofing"));
            loadItem.W = new Item("Wind Load", purlinSpacing * windPressure.number, "N/m", "");

            loadItem.CalculateTotalLoad();

            itemList.Add(new Item("*Purlin Spacing", purlinSpacing, "m", "==========Purlin Calculation=========="));

            foreach (Item item in loadItem.itemList)
            {
                itemList.Add(item);
            }
        }

        public void RafterCalculation(double purlinSpacing)
        {
            double g = 9.81;

            double NumberOfPurlin = portalFrameArea.halfSpan / purlinSpacing;//Everything is for half Rafter
            
            LoadItem loadItem = new LoadItem("N/m");

            Item ThermoPanel70 = new Item("ThermoPanel EPS 70mm", 11.60 * g, "Pa", "");
            //Metalcraft Roofing
            Item MSSPurlin18 = new Item("MSS 200/18", 5.62 * g, "N/m", "typ. span 7.5-10.5m from Metalcraft Roofing");

            Item tributaryAreaRafter = new Item("Rafter Tributary Area", portalFrameArea.halfSpan * portalFrameArea.spacing, "m2", "Single Rafter");
            Item QRoof = new Item("Rafter", RoofActions.StructuralElements(tributaryAreaRafter.number), "Pa", "Single Rafter");
            Item windPressure = new Item("Wind Pressure", -1039.99, "Pa", "- 1163.45Pa * 0.63 - 828.64Pa *0.37");

            loadItem.Q = new Item("Q UDL", portalFrameArea.spacing * QRoof.number, "N/m", "");

            loadItem.P.Add(new Item("ThermoPanel UDL", portalFrameArea.spacing * ThermoPanel70.number, "N/m", "ThermoPanel EPS 70mm"));
            loadItem.P.Add(new Item("MSS Purlin 200/18", 5.62 * g * portalFrameArea.spacing * NumberOfPurlin, "N/m", "For" + NumberOfPurlin + " " + portalFrameArea.spacing + "m purlins"));

            loadItem.W = new Item("Wind Load", portalFrameArea.spacing * windPressure.number, "N/m", "");

            loadItem.CalculateTotalLoad();


            itemList.Add(new Item("*Purlin Spacing", purlinSpacing, "m", "=========Rafter Calculation========="));
            foreach (Item item in loadItem.itemList)
            {
                itemList.Add(item);
            }
        }

        public void Data()
        {
            double g = 9.81;

            Item steelCorrugatedSheeting = new Item("Steel Corrugated Sheeting 1mm", 0.12, "kPa", "");

            double glassUnitWeight = 25.5;//kN/m3
            double thickness = 0.006;//m
            Item glass6mm = new Item("Glass 6mm", glassUnitWeight * thickness, "kPa", "");

            Item ThermoPanel50 = new Item("ThermoPanel EPS 50mm", 11.30 * g / 1000, "kPa", "");
            Item ThermoPanel70 = new Item("ThermoPanel EPS 70mm", 11.60 * g / 1000, "kPa", "");
            Item ThermoPanel100 = new Item("ThermoPanel EPS 100mm", 12.00 * g / 1000, "kPa", "");

            Item MSSPurlin18 = new Item("MSS 200/18", 5.62 * g / 1000, "kN/m", "typ. span 7.5-10.5m");
            Item MSSPurlin23 = new Item("MSS 200/23", 7.16 * g / 1000, "kN/m", "typ. span 8.5-11.5m");

            Item QShopFloor = new Item("Shop Floor", 4.0, "kPa", "");
        }
    }
}
