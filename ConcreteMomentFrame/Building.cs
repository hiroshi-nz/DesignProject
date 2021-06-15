using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivilApp.Class;
using CivilApp.Class.Earthquake;

namespace CivilApp.ConcreteMomentFrame
{
    class Building
    {

        public List<Item> itemList;
        public List<Item> materialList;


        public List<Item> basicInfoList;

        public List<Item> earthquakeList;

        public List<Item> elasticSiteSpectraSLSList;
        public List<Item> elasticSiteSpectraULSList;

        public List<Item> equivalentStaticMethodList;

        public EquivalentStaticMethod equivalentStaticMethod;
        public EquivalentStaticForces horizontalSeismicShear;

        public Item totalHeight;
        public Item totalDepth;
        public Item totalWidth;

        public Wall wall;
        public Wall halfWall;

        public Floor firstFloor;
        public Floor secondFloor;
        public Floor thirdFloor;
        public Floor roof;

        public Floor tributaryAreaFloor;
        public Floor tributaryAreaRoof;
        public TributaryLoads tributaryLoadsFloor;
        public TributaryLoads tributaryLoadsRoof;

        public ConcreteSlab slab;

        public Columns columns;
        public Columns halfColumns;
        public Beams beamsS;
        public Beams beamsL;

        public Building()
        {
            InitializeLists();
            InitializeMaterials();
            Dimensions();
            EarthquakeCalculation();
        }

        public void InitializeLists()
        {
            itemList = new List<Item>();
            earthquakeList = new List<Item>();
            equivalentStaticMethodList = new List<Item>();
            basicInfoList = new List<Item>();
        }


        public void Dimensions()
        {

            Item bayWidthS = new Item("Bay Width Short", 2.777, "m", "2.777m as specified");
            Item bayWidthL = new Item("Bay Width Long", 7, "m", "");

            Item bayNumberS = new Item("Number of Bay Short", 2, "", "");
            Item bayNumberL = new Item("Number of bay Long", 5, "", "");

            totalDepth = new Item("Total Depth", bayWidthS.number * bayNumberS.number, "m", bayWidthS.number + "m * " + bayNumberS.number);
            totalWidth = new Item("Total Width", bayWidthL.number * bayNumberL.number, "m", bayWidthL.number + "m * " + bayNumberL.number);

            itemList.Add(bayWidthS);
            itemList.Add(bayWidthL);
            itemList.Add(bayNumberS);
            itemList.Add(bayNumberL);
            itemList.Add(totalDepth);
            itemList.Add(totalWidth);

            Item floorToFloorHeight = new Item("Floor to Floor Height", 3.2, "m", "");
            Item floorNum = new Item("Total Number of Floors", 3, "", "");
            totalHeight = new Item("totalHeight", floorToFloorHeight.number * floorNum.number, "m", "");

            itemList.Add(floorToFloorHeight);
            itemList.Add(floorNum);
            itemList.Add(totalHeight);

            //=======================Floor G and Q====================================
            int matIndex = materialList.FindIndex(x => x.name == "Reinforced Concrete");
            Item material = materialList[matIndex];
            Item slabThickness = new Item("Slab Thickness", 0.2, "m", "");
            slab = new ConcreteSlab(slabThickness, material);

            firstFloor = CreateFloor();
            secondFloor = CreateFloor();
            thirdFloor = CreateFloor();
            roof = CreateRoof();

            //=========================Windows=================================
            Item doubleGrazedWindow = new Item("Double Glazed Window", 0.5, "kN/m2", "");
            Windows windows = new Windows(10, 1.8, 3, doubleGrazedWindow);
            Windows halfWindows = new Windows(10, 0.9, 3, doubleGrazedWindow);
            //=========================Wall==========================================
            double wallThickness = 0.22;
            double wallLength = totalWidth.number * 2 + totalDepth.number * 2 - wallThickness * 4;
            Item wallPerimeterLength = new Item("Wall Perimeter Length", wallLength, "m", "");

            Item brickMasonry = new Item("Brick Masonry", 18, "kN/m3", "");
            wall = new Wall(floorToFloorHeight.number, wallPerimeterLength.number, wallThickness, brickMasonry, windows);
            halfWall = new Wall(floorToFloorHeight.number / 2, wallPerimeterLength.number, wallThickness, brickMasonry, halfWindows);
            //=========================Columns and Beams==========================================
            matIndex = materialList.FindIndex(x => x.name == "Reinforced Concrete");
            Item reinforcedConcrete = materialList[matIndex];

            columns = new Columns(18, 0.4, 0.4, floorToFloorHeight.number, reinforcedConcrete);
            halfColumns = new Columns(18, 0.4, 0.4, floorToFloorHeight.number/ 2, reinforcedConcrete);
            beamsS = new Beams(12, 0.5, 0.3, bayWidthS.number, reinforcedConcrete);
            beamsL = new Beams(15, 0.5, 0.3, bayWidthL.number, reinforcedConcrete);

            //=========================Seismic Weight==============================================
            Loads floorAdditionalLoads = new Loads();
            
            floorAdditionalLoads.AddG(wall.totalWeight);
            floorAdditionalLoads.AddG(windows.totalWeight);
            floorAdditionalLoads.AddG(columns.totalWeight);
            floorAdditionalLoads.AddG(beamsL.totalWeight);
            floorAdditionalLoads.AddG(beamsS.totalWeight);

            Item psiE = new Item("Earthquake Imposed Action Combination Factor(PsiE)", 0.3, "", "All other except for roofs without public access");
            
            secondFloor.seismicWeight = new SeismicWeight(secondFloor, floorAdditionalLoads, psiE);
            thirdFloor.seismicWeight = new SeismicWeight(thirdFloor, floorAdditionalLoads, psiE);

            firstFloor.seismicHeight = new Item("Seismic Height", floorToFloorHeight.number * 0, "m", "");
            secondFloor.seismicHeight = new Item("Seismic Height", floorToFloorHeight.number * 1, "m", "");
            thirdFloor.seismicHeight = new Item("Seismic Height", floorToFloorHeight.number * 2, "m", "");


            //=======================Seismic Weight===================================
            Loads roofAdditionalLoads = new Loads();
            
            roofAdditionalLoads.AddG(halfWall.totalWeight);
            roofAdditionalLoads.AddG(halfWindows.totalWeight);
            roofAdditionalLoads.AddG(halfColumns.totalWeight);
            roofAdditionalLoads.AddG(beamsL.totalWeight);
            roofAdditionalLoads.AddG(beamsS.totalWeight);

            firstFloor.seismicWeight = new SeismicWeight(firstFloor, roofAdditionalLoads, psiE);
            roof.seismicWeight = new SeismicWeight(roof, roofAdditionalLoads, psiE);
            roof.seismicHeight = new Item("Seismic Height", floorToFloorHeight.number * 3, "m", "");

            //=========================Tributary Area==================================
            Columns tributaryAreaColumns = new Columns(3, 0.4, 0.4, floorToFloorHeight.number, reinforcedConcrete);
            Beams tributaryAreaBeamsS = new Beams(2, 0.5, 0.3, bayWidthS.number, reinforcedConcrete);
            Beams tributaryAreaBeamsL = new Beams(3, 0.5, 0.3, bayWidthL.number, reinforcedConcrete);

            Loads taFloorAdditionalLoads = new Loads();
            taFloorAdditionalLoads.AddG(tributaryAreaColumns.totalWeight);
            taFloorAdditionalLoads.AddG(tributaryAreaBeamsS.totalWeight);
            taFloorAdditionalLoads.AddG(tributaryAreaBeamsL.totalWeight);


            tributaryAreaFloor = firstFloor.CreateTributaryArea(bayWidthS.number, bayWidthL.number);
            tributaryAreaRoof = roof.CreateTributaryArea(bayWidthS.number, bayWidthL.number);
            Item Gfactor = new Item("1.2G", 1.2, "", "ULS");
            Item Qfactor = new Item("1.5Q", 1.5, "", "ULS");

            

            tributaryLoadsFloor = new TributaryLoads(tributaryAreaFloor, taFloorAdditionalLoads, Gfactor, Qfactor);

            Loads taRoofAdditionalLoads = new Loads();
            taRoofAdditionalLoads.AddG(tributaryAreaBeamsS.totalWeight);
            taRoofAdditionalLoads.AddG(tributaryAreaBeamsL.totalWeight);

            tributaryLoadsRoof = new TributaryLoads(tributaryAreaRoof, taRoofAdditionalLoads, Gfactor, Qfactor);
        }

        public void EarthquakeCalculation()
        {
            //========================Basic Info==================================
            Item location = new Item("Location:Petone", 0, "", "TITLE");

            Item IL = new Item("Importance Level", 2, "", "AS/NZS 1170.0:2002 Table 3.1");
            Item DL = new Item("Design Life", 50, "years", "");
            Item subSoilClass = new Item("Subsoil Class:C", 0, "", "TITLE");
            Item probULS = new Item("Annual Probability of Exceedance(Earthquake)(ULS)", 500, "1/500", "AS/NZS 1170.0:2002 Table 3.3");
            Item probSLS = new Item("Annual Probability of Exceedance(Earthquake)(SLS)", 25, "1/25", "AS/NZS 1170.0:2002 Table 3.3");

            basicInfoList.Add(location);
            basicInfoList.Add(IL);
            basicInfoList.Add(DL);
            basicInfoList.Add(subSoilClass);
            basicInfoList.Add(probULS);
            basicInfoList.Add(probSLS);
            //==========================================================
            Item kt = new Item("kt", 0.075, "", "For a moment-resisting concrete frame NZS1170.5 Supp 1:2004 C4.1.2.1");
            Item hn = new Item("hn", totalHeight.number, "m", "");

            equivalentStaticMethod = new EquivalentStaticMethod(kt, hn);
            equivalentStaticMethodList = equivalentStaticMethod.CreateList();
            //=======================Elastic Site Spectra===================================

            ElasticSiteSpectra elasticSiteSpectra = new ElasticSiteSpectra();

            elasticSiteSpectra.ChTSLS = new Item("Spectral Shape Factor(SLS)", MathHelper.Interpolate(equivalentStaticMethod.T1SLS.number, 0.4, 0.5, 2.36, 2.00), "", "Shallow Soil(C) AS/NZS 1170.5:2004 Table 3.1");
            elasticSiteSpectra.ChTULS = new Item("Spectral Shape Factor(ULS)", MathHelper.Interpolate(equivalentStaticMethod.T1ULS.number, 0.5, 0.6, 2.00, 1.74), "", "Shallow Soil(C) AS/NZS 1170.5:2004 Table 3.1");


            elasticSiteSpectra.Z = new Item("Hazard Factor(Z) Hutt Valley", 0.40, "", "AS/NZS 1170.5:2004 Table 3.3");
            elasticSiteSpectra.D = new Item("Major Fault Distance(D) Hutt Valley", 2, "km", "0-4km AS/NZS 1170.5:2004 Table 3.3");

            elasticSiteSpectra.Ru = new Item("Return Period Factor Ru(ULS)", 1.0, "", "1/500 AS/NZS 1170.5:2004 Table 3.5");
            elasticSiteSpectra.Rs = new Item("Return Period Factor Rs(SLS)", 0.25, "", "1/25 AS/NZS 1170.5:2004 Table 3.5");


            elasticSiteSpectra.NmaxT = new Item("Nmax(T)", 1.0, "", "Period <= 1.5s AS/NZS 1170.5:2004 Table 3.7");
            elasticSiteSpectra.Ntdu = new Item("Near-Fault Factor N(T,D)(ULS)", elasticSiteSpectra.NmaxT.number, "", "1/500 AS/NZS 1170.5:2004 3.1.6");
            elasticSiteSpectra.Ntds = new Item("Near-Fault Factor N(T,D)(SLS)", 1.0, "", "1/25 AS/NZS 1170.5:2004 3.1.6");

            elasticSiteSpectra.Solve();

            elasticSiteSpectraSLSList = elasticSiteSpectra.calculationSLSList;
            elasticSiteSpectraULSList = elasticSiteSpectra.calculationULSList;


            //===================Horizontal Design Action Coefficent=========================
            string subsoil = "C";
            Item u = new Item("Ductility Factor u", 3, "", "Moment Resisting Frame");
            equivalentStaticMethod.HorizontalDesignActionCoefficient(elasticSiteSpectra, subsoil, u);

            horizontalSeismicShear = new EquivalentStaticForces(equivalentStaticMethod.CdT1);
            horizontalSeismicShear.AddFloor(firstFloor);
            horizontalSeismicShear.AddFloor(secondFloor);
            horizontalSeismicShear.AddFloor(thirdFloor);
            horizontalSeismicShear.AddRoof(roof);

            horizontalSeismicShear.Solve();
                
        }

        public Floor CreateFloor()
        {
            Floor floor = new Floor(totalWidth.number, totalDepth.number);

            Item floorFinishing = new Item("Floor Finishing", 2.0, "kN/m2", "");
            floor.pressures.AddG(floorFinishing);

            int matIndex = materialList.FindIndex(x => x.name == "Reinforced Concrete");
            Item reinforcedConcrete = materialList[matIndex];

            floor.pressures.AddG(slab.weight);

            Item officeQ = new Item("Offices for General Use", 3.0, "kN/m2", "AS/NZS 1170.1:2002 Table 3.1");
            floor.pressures.AddQ(officeQ);

            floor.CalculateLoads();

            return floor;
        }

        public Floor CreateRoof()
        {
            Floor roof = new Floor(totalWidth.number, totalDepth.number);
            Item item = new Item("Roof finishing over the RC slab", 4.5, "kN/m2", "Complete with insulation screed and clay tiles");
            roof.pressures.AddG(item);
            roof.pressures.AddG(slab.weight);
            item = new Item("Accesible Roof", 4.0, "kN/m2", "Same as areas providing access but not less than 4.0 AS/NZS 1170.1:2002 Table 3.1");
            roof.pressures.AddQ(item);
            roof.CalculateLoads();

            return roof;
        }


        public void InitializeMaterials()
        {
            materialList = new List<Item>();
            Item item = new Item("Reinforced Concrete", 25, "kN/m3", "");
            materialList.Add(item);
            item = new Item("Plain Concrete", 24, "kN/m3", "");
            materialList.Add(item);
            item = new Item("Brick Masonry", 18, "kN/m3", "");
            materialList.Add(item);
            item = new Item("Granite", 27.3, "kN/m3", "");
            materialList.Add(item);
            item = new Item("Double Glazed Window", 0.5, "kN/m2", "");
            materialList.Add(item);
        }

        
    }
}
