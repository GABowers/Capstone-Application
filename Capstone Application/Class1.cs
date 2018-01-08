using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    class StatePageInfo
    {
        public int stateNum;
        public int color;
        public int? startingAmount;
        int neighborState;
        public float?[,,] probs; // [x,y] prob to go to state x with y neighbors

        public StatePageInfo(int totalStates, int neighbors, int currentState)
        {
            stateNum = currentState;
            color = 0;
            startingAmount = 0;
            probs = new float?[totalStates, totalStates, neighbors + 1];
        }
    }
}
