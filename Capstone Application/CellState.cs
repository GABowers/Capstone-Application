using System;
using System.Collections;
using System.Collections.Generic;

public class CellState
{
    //prob of becoming state X, with Y neighbors of X
    public double[,][,] advProbs;
    float[,,] prob;
    public double[] walkProbs = new double[4];
    //public double stickingProb;
    public double[] stickingProbs;
    public bool sticking;
    public int mobileNeighborhood;
    public List<Tuple<int, int>> startingLocations;

    public CellState(int totalStates, int neighborState, int neighborSize)
    {
        advProbs = new double[totalStates, totalStates][,];
        prob = new float[totalStates, neighborState, neighborSize + 1];
        stickingProbs = new double[totalStates];
    }

    public void SetProbability(int state, int neighborState, int numNeighbors, float val)
    {
        prob[state, neighborState, numNeighbors] = val;
    }

    public float GetProbability(int state, int neighborState, int numNeighbors)
    {
        return prob[state, neighborState, numNeighbors];
    }

    public void Set2ndOrderInfo(double[] incomingWalkProbs, List<double> incomingStickingProbs, bool incomingSticking, int incomingNeighborhood, List<Tuple<int, int>> incomingStartingLocations)
    {
        //stickingProbs = new double[incomingStickingProbs.Count];
        walkProbs = incomingWalkProbs;
        stickingProbs = incomingStickingProbs.ToArray();
        sticking = incomingSticking;
        mobileNeighborhood = incomingNeighborhood;
        startingLocations = new List<Tuple<int, int>>();
        startingLocations = incomingStartingLocations;
    }

    public void SetProbability(int state, int neighborState, int rows, int columns, double val)
    {
        // advProbs[state, neighborState] = new double[rows, columns];
        advProbs[state, neighborState][rows, columns] = val;
    }

    public void InitializeArray(int state, int neighborState, int rows, int columns)
    {
        advProbs[state, neighborState] = new double[rows, columns];
    }

    public double GetProbability(int state, int neighborState, int rows, int columns)
    {
        return advProbs[state, neighborState][rows, columns];
    }
}