using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CivilApp.Class.PortalFrameCalculation.Wind;

namespace CivilApp.Class.PortalFrameCalculation
{
    class PortalFrameArea
    {
        public double roofPitch;
        public double eavesHeight;
        public double totalHeight;
        public double span;
        public double halfSpan;
        public double depth;
        public double spacing;

        public double rafterLength;
        public double roofRise;

        public double singleGableEndWallArea;

        public double singleRoofArea;
        public List<Item> singleRoofAreaList;

        public double singleSideWallArea;
        public List<Item> singleSideWallAreaList;

        public WindItem areaItemAlong;

        public List<Item> itemList;


        public PortalFrameArea(double roofPitch, double eavesHeight, double span, double depth, double spacing)
        {
            this.roofPitch = roofPitch;
            this.eavesHeight = eavesHeight;
            this.span = span;
            this.depth = depth;
            this.spacing = spacing;

            this.halfSpan = span / 2;

            FindRafterLength();
            FindRoofRise();
            FindArea();

            List<double> distanceList = new List<double>() { 0, 0.5 * totalHeight, 1 * totalHeight, 2 * totalHeight, 3 * totalHeight };
            singleRoofAreaList = FindArea(distanceList, rafterLength, depth);//for roof

            distanceList = new List<double>() { 0, 1 * totalHeight, 2 * totalHeight, 3 * totalHeight };
            singleSideWallAreaList = FindArea(distanceList, eavesHeight, depth);//for sidewall

            CreateAreaItemAlong();

            CreateList();
            Print(itemList);
        }

        public void FindRafterLength()
        {
            rafterLength = halfSpan / Math.Cos(roofPitch * Math.PI / 180);
        }

        public void FindRoofRise()
        {
            roofRise = halfSpan * Math.Tan(roofPitch * Math.PI / 180);
            totalHeight = roofRise + eavesHeight;
        }

        public void FindArea()
        {
            singleRoofArea = rafterLength * depth;
            singleSideWallArea = eavesHeight * depth;

            FindGableEndWallArea();
        }

        public void FindGableEndWallArea()
        {
            double halfRoofArea = halfSpan * roofRise / 2;
            double roofArea = halfRoofArea * 2;
            singleGableEndWallArea = eavesHeight * span + roofArea;
        }

   
        public void CreateAreaItemAlong()
        {
            areaItemAlong = new WindItem();

            Item item = new Item("Area", singleGableEndWallArea, "m2", "Openings not subtracted yet.");
            areaItemAlong.windwardWall = item;

            item = new Item("Area", singleGableEndWallArea, "m2", "Openings not subtracted yet.");
            areaItemAlong.leewardWall = item;

            areaItemAlong.roofList = singleRoofAreaList;
            areaItemAlong.sideWallList = singleSideWallAreaList;
        }

        public List<Item> FindArea(List<double> distanceList, double length, double depth)
        { 
            double depthCounter = depth;
            List<Item> areaList = new List<Item>();

            List<double> roundedDistanceList = new List<double>();

            foreach(double distance in distanceList)
            {
                roundedDistanceList.Add(MathHelper.Round2dec(distance));
            }

            Console.WriteLine("Length:" + length + "m depth:" + depth + "m");

            for (int i = 0; i < distanceList.Count - 1; i++)
            {
                double difference = distanceList[i + 1] - distanceList[i];

                if (depth >= distanceList[i + 1])//depth is longer than the longer of distances
                {
                    depthCounter -= difference;
                    Item item = new Item(roundedDistanceList[i] + "m to " + roundedDistanceList[i + 1] + "m", (distanceList[i + 1] - distanceList[i]) * length, "m2", "");
                    areaList.Add(item);
                }
                else if (depth > distanceList[i] && depth < distanceList[i + 1])//depth is shorter than longer distance but longer than shorter distance
                {
                    Item item = new Item(roundedDistanceList[i] + "m to " + roundedDistanceList[i + 1] + "m", (depthCounter) * length, "m2", MathHelper.Round2dec(depthCounter) + " x " + MathHelper.Round2dec(length));
                    areaList.Add(item);

                    depthCounter = 0;
                }
            }
                if (depthCounter > 0)
                {
                    Item item = new Item("Greater than " + roundedDistanceList[distanceList.Count - 1] + "m", depthCounter * length, "m2", MathHelper.Round2dec(depthCounter) + " x " + MathHelper.Round2dec(length));
                    areaList.Add(item);
                }
                Print(areaList);
            return areaList;
        }


        public void CreateList()
        {
            itemList = new List<Item>();
            itemList.Add(new Item("Gable End Wall Area", singleGableEndWallArea, "m2", "Single wall area"));
            itemList.Add(new Item("Roof Area", singleRoofArea, "m2", "Single roof area"));
            itemList.Add(new Item("Side Wall Area", singleSideWallArea, "m2", "Single wall area"));

            itemList.Add(new Item("Total Height", totalHeight, "m", ""));
            itemList.Add(new Item("Eaves Height", eavesHeight, "m", ""));
            itemList.Add(new Item("Roof Rise Height", roofRise, "m", "Without eaves height"));
            itemList.Add(new Item("Rafter Length", rafterLength, "m", "Single rafter length"));
            itemList.Add(new Item("Portal Frame Span", span, "m", ""));
            itemList.Add(new Item("Portal Frame Depth", depth, "m", ""));

            itemList.Add(new Item("Roof Pitch", roofPitch, "degree", ""));
        }

        public void Print()
        {
            foreach (Item item in itemList)
            {
                Console.WriteLine(item.name + ":" + MathHelper.Round3dec(item.number) + item.unit);
            }
        }

        public void Print(List<Item> itemList)
        {
            foreach (Item item in itemList)
            {
                Console.WriteLine(item.name + ":" + MathHelper.Round3dec(item.number) + item.unit + " " + item.note);
            }
        }

        public void DataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = 4;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[1].Name = "Number";
            dataGridView.Columns[2].Name = "Unit";
            dataGridView.Columns[3].Name = "Note";

            foreach (Item item in itemList)
            {
                dataGridView.Rows.Add(item.name, MathHelper.Round3dec(item.number), item.unit, item.note);
            }
            dataGridView.AutoResizeColumns();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
