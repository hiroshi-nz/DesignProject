using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class.PortalFrameCalculation.Wind;

namespace CivilApp.Class.PortalFrameCalculation
{
    class WindCalculation
    {
        public PortalFrameArea portalFrameArea;
        public double OpeningAreaRatio;
        public double HeightToDepth;
        public double depthToBreadth;

        public List<Item> itemList;

        public WindItem areaItem;
        public WindItem pressureCoefficientItem;

        public WindItem windpressureItemSLS;
        public WindItem windpressureItemULS;

        public WindItem windLoadItemSLS;
        public WindItem windLoadItemULS;

        public SiteWindSpeed siteWindSpeed;

        public WindCalculation(PortalFrameArea portalFrameArea)
        {
            this.portalFrameArea = portalFrameArea;
            FindOpeningArea();
            HeightToDepth = portalFrameArea.totalHeight / portalFrameArea.depth;
            depthToBreadth = portalFrameArea.depth / portalFrameArea.span;

            SolveAlong();

            CreateList();

            //Print();
        }

        public void SolveAlong()
        {
            double depth = portalFrameArea.depth;
            double breadth = portalFrameArea.span;
            double height = portalFrameArea.totalHeight;

            siteWindSpeed = new SiteWindSpeed();

            

            Item windwardCoefficient = new Item("Windward Wall Cp,e", 0.7, "", "z = h");
            Item leewardCoefficient = new Item("Leeward Wall Cp,e", -0.5, "", "Roof Pitch < 10 and d/b = 1");
            GableRoof roofCoefficient = new GableRoof(portalFrameArea.totalHeight, HeightToDepth);
            SideWalls sideWallCoefficient = new SideWalls(portalFrameArea.totalHeight);
            Item internalCoefficient = new Item("Internal Pressure Cp,i", 0.7 * windwardCoefficient.number, "", "Opening area ratio 2 or less");


            PressureCoefficients pressureCoef = new PressureCoefficients(windwardCoefficient, leewardCoefficient, roofCoefficient, sideWallCoefficient, internalCoefficient);


            pressureCoefficientItem = pressureCoef.Solve();



            WindPressure windPressureSLS = new WindPressure(pressureCoefficientItem, siteWindSpeed.VsitBetaSLS);
            WindPressure windPressureULS = new WindPressure(pressureCoefficientItem, siteWindSpeed.VsitBetaULS);

            windpressureItemSLS = windPressureSLS.Solve();
            windpressureItemULS = windPressureULS.Solve();
            areaItem = portalFrameArea.areaItemAlong;

            WindLoad windLoadSLS = new WindLoad(windpressureItemSLS, portalFrameArea.areaItemAlong);
            WindLoad windLoadULS = new WindLoad(windpressureItemULS, areaItem);

            windLoadItemSLS = windLoadSLS.Solve();
            windLoadItemULS = windLoadULS.Solve();

            pressureCoefficientItem.Print();
            windpressureItemSLS.Print();
            windpressureItemULS.Print();
            areaItem.Print();

            windLoadItemSLS.Print();
            windLoadItemULS.Print();
        }




        public void AreaConversion()
        {
            double dockHeight = 4.5;//maximum truck height is 4.3m https://www.drivingtests.co.nz/resources/heavy-vehicle-dimensions-and-measurements/
            double dockWidth = 3;//maximum truck width is 2.55m

            double dockOpeningArea = dockHeight * dockWidth;

            WindItem areaItem = new WindItem();

            Item item = new Item("", portalFrameArea.singleGableEndWallArea - dockOpeningArea, "m2", "Gable end wall with a dock opening.");
            areaItem.windwardWall = item;

            item = new Item("", portalFrameArea.singleGableEndWallArea, "m2", "Gable end wall.");
            areaItem.leewardWall = item;

        }

        public void FindOpeningArea()
        {
            double dockHeight = 4.5;//maximum truck height is 4.3m https://www.drivingtests.co.nz/resources/heavy-vehicle-dimensions-and-measurements/
            double dockWidth = 3;//maximum truck width is 2.55m

            double dockOpening = dockHeight * dockWidth;

            double doorHeight = 3;
            double doorWidth = 10;

            double doorArea = doorHeight * doorWidth;

            if (dockOpening > doorArea)
            {
                double largestOpening = dockOpening;
                OpeningAreaRatio = largestOpening / doorArea;
            }
            else
            {
                double largestOpening = doorArea;
                OpeningAreaRatio = largestOpening / dockOpening;
            }
        }

        public void CreateList()
        {
            itemList = new List<Item>();
            itemList.Add(new Item("Opening Area Ratio", OpeningAreaRatio, "", ""));
            itemList.Add(new Item("h/d", HeightToDepth, "", ""));
            itemList.Add(new Item("d/b", depthToBreadth, "", ""));

            foreach(Item item in siteWindSpeed.itemList)
            {
                itemList.Add(item);
            }

            CreateListFromWindItem(areaItem);
            CreateListFromWindItem(pressureCoefficientItem);
            itemList.Add(new Item("SLS", 0, "", "================"));
            CreateListFromWindItem(windpressureItemSLS);
            itemList.Add(new Item("ULS", 0, "", "================"));
            CreateListFromWindItem(windpressureItemULS);
            itemList.Add(new Item("SLS", 0, "", "================"));
            CreateListFromWindItem(windLoadItemSLS);
            itemList.Add(new Item("ULS", 0, "", "================"));
            CreateListFromWindItem(windLoadItemULS);
        }

        private void CreateListFromWindItem(WindItem windItem)
        {
            windItem.CreateList();
            foreach (Item item in windItem.itemList)
            {
                itemList.Add(item);
            }
        }
    }
}
