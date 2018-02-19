using System.Collections;
using System.Collections.Generic;

public class CellState
{
    //prob of becoming state X, with Y neighbors of X
    public double[,][,] advProbs;
    float[,,] prob;
    public double[] walkProbs = new double[4];

    public CellState(int totalStates, int neighborState, int neighborSize)
    {
        advProbs = new double[totalStates, totalStates][,];
        prob = new float[totalStates, neighborState, neighborSize + 1];
    }

    public void SetProbability(int state, int neighborState, int numNeighbors, float val)
    {
        prob[state, neighborState, numNeighbors] = val;
    }

    public float GetProbability(int state, int neighborState, int numNeighbors)
    {
        return prob[state, neighborState, numNeighbors];
    }

    public void Set2ndOrderInfo(double[] incomingWalkProbs)
    {
        walkProbs = incomingWalkProbs;
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