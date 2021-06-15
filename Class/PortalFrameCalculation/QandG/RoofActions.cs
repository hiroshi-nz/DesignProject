using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.PortalFrameCalculation
{
    class RoofActions
    {
        public static double StructuralElements(double projectionArea)//Table 3.2
            //the plan projection of the surface area of roof supported by the member.
        {
            double ans = 1.8 / projectionArea + 0.12;

            if(ans <= 0.25)
            {
                return 0.25 * 1000;//kPa to Pa
            }
            else
            {
                return ans * 1000;//kPa to Pa
            }
        }
    }
}
