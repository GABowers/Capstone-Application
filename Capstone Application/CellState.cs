using System;
using System.Collections;
using System.Collections.Generic;

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
    public int mobileNeighborhood;
    public List<Tuple<int, int>> startingLocations;

    public CellState(int totalStates, int neighborState, int neighborSize, List<List<List<double>>> probs, List<double> incomingWalkProbs, List<double> incomingStickingProbs, bool incomingSticking, int incomingNeighborhood, bool incMobile, List<Tuple<int, int>> incomingStartingLocations)
    {
        advProbs = new double[totalStates, totalStates][,];
        prob = new double[totalStates, neighborState, neighborSize + 1];
        for (int i = 0; i < probs.Count; i++)
        {
            for (int j = 0; j < probs[i].Count; j++)
            {
                for (int k = 0; k < probs[i][j].Count; k++)
                {
                    prob[i, j, k] = probs[i][j][k];
                }
            }
        }

        stickingProbs = new double[totalStates];
        walkProbs = incomingWalkProbs.ToArray();
        stickingProbs = incomingStickingProbs.ToArray();
        sticking = incomingSticking;
        mobileNeighborhood = incomingNeighborhood;
        mobile = incMobile;
        startingLocations = new List<Tuple<int, int>>();
        startingLocations = incomingStartingLocations;
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