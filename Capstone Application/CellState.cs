using Capstone_Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CellState
{
    //prob of becoming state X, with Y neighbors of X
    public double[,][,] advProbs;
    public double[,,] prob;
    public double[] walkProbs = new double[4];
    //public double stickingProb;
    public double[] stickingProbs;
    public bool sticking;
    public bool mobile;
    public MoveType mobileNeighborhood;
    public GridType gridType;
    public List<Tuple<int, int>> startingLocations;
    public List<Capstone_Application.AgentContainerSetting> containerSettings;
    public NType neighborhoodType;

    public CellState(int totalStates, int neighborState, StatePageInfo info)
    {
        this.gridType = info.gridType.Value;
        advProbs = new double[totalStates, totalStates][,];
        prob = new double[totalStates, neighborState, info.neighbors.Value + 1];
        double[][][] probs = info.probs.Select(x => x.Select(y => y.ToArray()).ToArray()).ToArray();
        for (int i = 0; i < probs.Length; i++)
        {
            for (int j = 0; j < probs[i].Length; j++)
            {
                for (int k = 0; k < probs[i][j].Length; k++)
                {
                    prob[i, j, k] = probs[i][j][k];
                }
            }
        }
        containerSettings = info.containerSettings;
        stickingProbs = new double[totalStates];
        walkProbs = info.moveProbs.ToArray();
        stickingProbs = info.stickingProbs.ToArray();
        sticking = info.sticking.Value;
        mobileNeighborhood = info.mobileNeighborhood.Value;
        mobile = info.mobile.Value;
        startingLocations = new List<Tuple<int, int>>();
        startingLocations = info.startingLocations;
        this.neighborhoodType = info.nType;
    }

    //public double GetProbability(int state, int neighborState, int numNeighbors)
    //{
    //    return prob[state, neighborState, numNeighbors];
    //}

    //public void SetProbability(int state, int neighborState, int rows, int columns, double val)
    //{
    //    // advProbs[state, neighborState] = new double[rows, columns];
    //    advProbs[state, neighborState][rows, columns] = val;
    //}

    //public void InitializeArray(int state, int neighborState, int rows, int columns)
    //{
    //    advProbs[state, neighborState] = new double[rows, columns];
    //}

    //public double GetProbability(int state, int neighborState, int rows, int columns)
    //{
    //    return advProbs[state, neighborState][rows, columns];
    //}
}