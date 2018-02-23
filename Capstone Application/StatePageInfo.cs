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
        public NType nType;
        public int caType;
        public int stateNum;
        public Color color;
        public int? startingAmount;
        //int neighborState;
        public double[,][,] advProbs;
        public float[,,] probs; // [x,y] prob to go to state x with y neighbors
        public List<string> locationCode;
        public List<double> probValue;
        public double[] walkProbs = new double[4];
        public double stickingProb;
        public bool sticking;

        public StatePageInfo(int totalStates, int neighbors, int currentState, NType neighborType, int caType)
        {
            nType = neighborType;
            stateNum = currentState;
            color = Color.White;
            startingAmount = 0;
            advProbs = new double[totalStates, totalStates][,];
            probs = new float[totalStates, totalStates, neighbors + 1];
        }

        //public StatePageInfo(int totalStates, int neighbors, int currentState, int rows, int columns)
        //{
        //    stateNum = currentState;
        //    color = Color.White;
        //    startingAmount = 0;
        //    advProbs = new double[totalStates, totalStates][,];
        //    probs = new float[totalStates, totalStates, neighbors + 1];
        //}

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
