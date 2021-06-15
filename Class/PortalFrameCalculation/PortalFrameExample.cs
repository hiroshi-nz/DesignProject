using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation
{
    class PortalFrameExample
    {
        public static void SuperMarket()
        {
            //PortalFrameCalculation.Solve();

            double roofPitch = 3;//3-5 deg
            double eavesHeight = 8;//5-10m
            double portalFrameSpan = 40;//15-50m
            double portalFrameHalfSpan = portalFrameSpan / 2;//for the ease of understanding
            double portalFrameSpacing = 8;//8-12m
            double portalFrameDepth = 40;

            PortalFrameArea portalFrameArea = new PortalFrameArea(roofPitch, eavesHeight, portalFrameSpan, portalFrameDepth, portalFrameSpacing);
            WindCalculation windCalculation = new WindCalculation(portalFrameArea);

            PortalFrameCalculation portalFrameCalculation = new PortalFrameCalculation(portalFrameArea);
        }
    }
}
