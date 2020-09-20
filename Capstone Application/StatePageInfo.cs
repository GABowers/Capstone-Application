﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class StatePageInfo
    {
        public NType? nType;
        public GridType? gridType;
        public int? stateNum;
        public Color? color;
        public int? startingAmount;
        //int neighborState;
        public double?[,][,] advProbs;
        public List<List<List<double>>> probs; // [x,y] prob to go to state x with y neighbors
        //public List<string> locationCode;
        //public List<double> probValue;
        public List<double> moveProbs;
        public List<double> stickingProbs;

        public bool? sticking;
        public bool? mobile;
        public bool? storage = false;
        public bool? ai = false; // think about what type of search the ai will have - what range?
        public bool? growth = false;

        public MoveType? mobileNeighborhood;
        public List<Tuple<int, int>> startingLocations;
        public List<Tuple<string, double>> storageObjects;
        public int? neighbors;
        public List<AgentContainerSetting> containerSettings;

        public List<object> template_objects;

        public StatePageInfo(int currentState)
        {
            stateNum = currentState;
            color = Color.White;
            startingAmount = 0;
        }
    }
}
