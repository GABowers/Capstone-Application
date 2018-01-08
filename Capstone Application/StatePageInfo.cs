using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class StatePageInfo
    {
        public int stateNum;
        public Color color;
        public int? startingAmount;
        int neighborState;
        public float[,,] probs; // [x,y] prob to go to state x with y neighbors
        public List<string> locationCode;
        public List<double> probValue;

        public StatePageInfo(int totalStates, int neighbors, int currentState)
        {
            stateNum = currentState;
            color = Color.White;
            startingAmount = 0;
            probs = new float[totalStates, totalStates, neighbors + 1];
        }

        //public StatePageInfo(int currentState)
        //{
        //    stateNum = currentState;
        //    color = Color.White;
        //    startingAmount = 0;
        //    locationCode = new List<string>();
        //    probValue = new List<double>();
        //}
    }
}
