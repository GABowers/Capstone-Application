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
        public GridType gridType;
        public int stateNum;
        public Color color;
        public int? startingAmount;
        //int neighborState;
        public double[,][,] advProbs;
        public List<List<List<double>>> probs; // [x,y] prob to go to state x with y neighbors
        //public List<string> locationCode;
        //public List<double> probValue;
        public List<double> moveProbs;
        public List<double> stickingProbs = new List<double>();

        public bool sticking;
        public bool mobile;
        public bool storage = false;
        public bool ai = false; // think about what type of search the ai will have - what range?
        public bool growth = false;

        public int mobileNeighborhood;
        public List<Tuple<int, int>> startingLocations;
        public List<Tuple<string, double>> storageObjects = new List<Tuple<string, double>>();
        public int neighbors;

        public StatePageInfo(int currentState)
        {
            stateNum = currentState;
            color = Color.White;
            startingAmount = 0;
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
