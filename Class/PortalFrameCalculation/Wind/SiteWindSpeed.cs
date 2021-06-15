using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation.Wind
{
    class SiteWindSpeed
    {
        public double VRSLS;
        public double VRULS;
        public double Md;
        public double Mzcat;
        public double Ms;
        public double Mt;
        public List<Item> itemList;

        public double VsitBetaSLS;
        public double VsitBetaULS;

        public SiteWindSpeed()
        {
            itemList = new List<Item>();

            Item item = new Item("Importance Level", 2, "", "Less than 100x100m and 300 people");
            itemList.Add(item);

            item = new Item("Design Life", 50, "years", "");
            itemList.Add(item);

            item = new Item("Waikanae(Wind Region A7)", 0, "", "Wind Region A7");
            itemList.Add(item);

            item = new Item("Annual Probability of Exceedance", 25, "", "SLS Wind 1/25");
            itemList.Add(item);

            item = new Item("Annual Probability of Exceedance", 500, "", "ULS Wind 1/500");
            itemList.Add(item);

            item = new Item("Regional Wind Speed(SLS)", 37, "m/s", "");
            itemList.Add(item);

            item = new Item("Regional Wind Speed(ULS)", 45, "m/s", "");
            itemList.Add(item);

            item = new Item("Wind Directional Multiplier", 1.0, "", "Using Md = 1.0 gives conservative result.");
            itemList.Add(item);

            item = new Item("Terrain/Height Multiplier(TC3)", 0.83, "", "For height between 5 to 10m");
            itemList.Add(item);

            item = new Item("Shielding Multiplier", 1.0, "", "No building with the height taller than this building.");
            itemList.Add(item);

            item = new Item("Topographic Multiplier", 1.0, "", "Flat land, no lee zone.");
            itemList.Add(item);

            VRSLS = 37;
            VRULS = 45;
            Md = 1.0;
            Mzcat = 0.83;
            Ms = 1.0;
            Mt = 1.0;

            VsitBetaSLS = VRSLS * Md * Mzcat * Ms * Mt;
            VsitBetaULS = VRULS * Md * Mzcat * Ms * Mt;

            item = new Item("Site Wind Speed(SLS)", VsitBetaSLS, "m/s", "");
            itemList.Add(item);

            item = new Item("Site Wind Speed(ULS)", VsitBetaULS, "m/s", "");
            itemList.Add(item);
        }
    }
}
