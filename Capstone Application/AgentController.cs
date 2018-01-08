using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class AgentController
    {
        ControllerScript controllerScript = Form1.controllerScript;
        CA caScript = Form1.controllerScript.myCA;
        public int currentState;
        public int xLocation;
        public int yLocation;

        public void TransitionCheck()
        {

        }

        public void MovementCheck()
        {

        }

        //This could be optimized further. For immobile CA, we could set the neighborhood at the start of a CA run, so each agent knows
        //its neighbors in advance, rather than calculation each iteration. However, this method will work for both immobile and mobile
        //CA. May change things if too slow.
        public void NeighborCheck()
        {
            List<int> xLocations = new List<int>();
            List<int> yLocations = new List<int>();
            if(controllerScript.neighborhoodType >= 0)
            {

                //this will need to change if we move to a system which allows for non-overlapping neighborhoods
                if (controllerScript.neighborhoodType >= 1)
                {
                    xLocations.Add(xLocation);
                    yLocations.Add(yLocation + 1);
                    xLocations.Add(xLocation + 1);
                    yLocations.Add(yLocation);
                    xLocations.Add(xLocation);
                    yLocations.Add(yLocation - 1);
                    xLocations.Add(xLocation - 1);
                    yLocations.Add(yLocation);
                    if (controllerScript.neighborhoodType >= 2)
                    {
                        xLocations.Add(xLocation + 1);
                        yLocations.Add(yLocation + 1);
                        xLocations.Add(xLocation + 1);
                        yLocations.Add(yLocation - 1);
                        xLocations.Add(xLocation - 1);
                        yLocations.Add(yLocation - 1);
                        xLocations.Add(xLocation - 1);
                        yLocations.Add(yLocation + 1);
                        if (controllerScript.neighborhoodType >= 3)
                        {
                            xLocations.Add(xLocation);
                            yLocations.Add(yLocation + 2);
                            xLocations.Add(xLocation + 2);
                            yLocations.Add(yLocation);
                            xLocations.Add(xLocation);
                            yLocations.Add(yLocation - 2);
                            xLocations.Add(xLocation - 2);
                            yLocations.Add(yLocation);
                        }
                    }
                }
            }
            if (controllerScript.neighborhoodType >= 1)
            {
                for (int i = 0; i < xLocations.Count; i++)
                {
                    if (WithinRange(xLocations[i], yLocations[i], caScript.gridWidth, caScript.gridHeight) == false)
                    {
                        switch (controllerScript.gType)
                        {
                            case GridType.Box:
                                //modifiedP = null; // make it null to skip it
                                break;
                            case GridType.CylinderW:
                                //modifiedP = Point.AdjustCylinderW(gridWidth, modifiedP);
                                break;
                            case GridType.CylinderH:
                                //modifiedP = Point.AdjustCylinderH(gridHeight, modifiedP);
                                break;
                            case GridType.Torus:
                                //modifiedP = Point.AdjustTorus(gridWidth, gridHeight, modifiedP);
                                break;
                        }
                        xLocations.RemoveAt(i);
                        yLocations.RemoveAt(i);
                        i = (i - 1);
                    }
                }
            }
        }

        public bool WithinRange(int locationX, int locationY, int xMax, int yMax, int xMin = 0, int yMin = 0)
        {
            if (locationX < xMin || locationY < yMin)
                return false;
            if (locationX >= xMax || locationY >= yMax)
                return false;
            return true;
        }
    }
}
