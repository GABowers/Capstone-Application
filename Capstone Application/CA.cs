﻿using Capstone_Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SysRand = System.Random;
//using System.Threading.Tasks;

public class CA
{
    int caType = 0;
    List<AgentController> activeAgents = new List<AgentController>();
    System.Object[,] objects;
    //private BlankGrid blankGrid;
    //AgentController agent;
    private CellState[] states;
    private Neighborhood neighborhood;
    private GridType gridType;
    public BlankGrid[,] grid;
    BlankGrid[,] backup;
    public int gridWidth;
    public int gridHeight;
    private int numStates;
    public int agentLocation;
    SysRand myRand;
    public static List<int> stateCount = new List<int>();
    public List<int> stateCountReplacement = new List<int>();
    public List<int> neighborCount = new List<int>();
    public List<float> neighborAnalysis = new List<float>();

    private StatePageInfo statePageInfo;

    public CA(int width, int height, int numStates, NType type, GridType gType = GridType.Box)
    {
        gridWidth = width;
        gridHeight = height;
        this.numStates = numStates;
        gridType = gType;
        grid = new BlankGrid[width, height];
        backup = new BlankGrid[width, height];
        neighborhood = new Neighborhood(type);
        states = new CellState[numStates];
        for (int i = 0; i < numStates; ++i)
        {
            stateCount.Add(0);
            states[i] = new CellState(numStates, numStates, neighborhood.GetNeighborSize());
        }
        myRand = new SysRand();


    }

    //public void ChangeCell(int i, int j)
    //{
    //    int currentState = grid[i, j];
    //    int newState = currentState + 1;
    //    if (newState >= numStates)
    //        newState = (newState - numStates);
    //    grid[i, j] = newState;
    //}

    // Keep for multithreading
    //public void ModifiedOneIteration()
    //{
    //    Array.Copy(grid, backup, gridWidth * gridHeight);
    //    for (int i = 0; i < numStates; ++i)
    //        stateCount[i] = 0;
    //}

    public void InitializeGrid(List<int> cellAmounts)
    {
        myRand = new SysRand();
        // This method creates the grid after the user inputs the necessary initial and transition information

        // make sure ratios is as big as our numStates

        //Get a list of how many cells of each type we need
        List<int> agentCount = new List<int>();
        int totalCells = 0;
        for (int i = 0; i < numStates; ++i)
        {
            agentCount.Add(cellAmounts[i]);
            totalCells += cellAmounts[i];
        }

        // this is a list of which states still needed to be added
        
        int gridSize = (gridWidth * gridHeight);
        int currentMin = gridSize;
        int state = 0;
        while (agentCount.Any())
        {
            int randX = myRand.Next(gridSize);
            int xValue = (randX / gridWidth);
            int yValue = (randX % gridWidth);
            
            if (randX >= currentMin)
            {
                while ((System.Object.ReferenceEquals(grid[xValue, yValue], null) == false))
                {
                    randX += 1;
                    if(randX >= (gridWidth * gridHeight))
                    {
                        randX -= (gridWidth * gridHeight);
                    }
                    xValue = (randX / gridWidth);
                    yValue = (randX % gridWidth);
                }
            }
            
            grid[xValue, yValue] = new BlankGrid();
            grid[xValue, yValue].AddAgent();
            grid[xValue, yValue].containsAgent = true;
            grid[xValue, yValue].agent.currentState = state;
            grid[xValue, yValue].agent.xLocation = xValue;
            grid[xValue, yValue].agent.yLocation = yValue;

            activeAgents.Add(grid[xValue, yValue].agent);

            if (randX < currentMin)
            {
                currentMin = randX;
            }

            // Subtract that state from its agentcount
            // And if it goes to zero remove it from both our lists
            agentCount[0]--;

            if (agentCount[0] == 0)
            {
                agentCount.RemoveAt(0);
                state += 1;
            }
        }
        if(totalCells < gridSize)
        {
            for (int i = 0; i < gridWidth; ++i)
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    if (System.Object.ReferenceEquals(grid[i, j], null))
                    {
                        grid[i, j] = new BlankGrid();
                        grid[i, j].containsAgent = false;
                    }
                }
            }
        }
    }

    public void SetStateInfo(int startState, int endState, int neighborState, int numNeighbors, float prob)
    {
        states[startState].SetProbability(endState, neighborState, numNeighbors, prob);

    }

    public void SetCellState(int x, int y, int state)
    {
        grid[x, y].agent.currentState = state;
    }

    public int GetCellState(int x, int y)
    {
        //This should ONLY fire if the grid point contains an agent.
        return grid[x, y].agent.currentState;
    }

    //public void OneIteration()
    //{
    //    Array.Copy(grid, backup, gridWidth * gridHeight);
    //    for (int i = 0; i < numStates; ++i)
    //        stateCount[i] = 0;
    //    for (int x = 0; x < gridWidth; ++x)
    //    {
    //        for (int y = 0; y < gridHeight; ++y)
    //        {
    //            int currentState = grid[x, y];
    //            List<int> neighborStateCount = GetNeighborCount(x, y);
    //            float[] probChances = GetProbChances(currentState, neighborStateCount);
    //            grid[x, y] = GetStateFromProbability(probChances);
    //            stateCount[currentState] += 1;
    //        }
    //    }
    //}

    public void OneIteration()
    {
        //Console.WriteLine("CA OneIteration starting");
        Array.Copy(grid, backup, gridWidth * gridHeight);
        for (int i = 0; i < numStates; ++i)
            stateCount[i] = 0;
        if (caType == 1) //1 = second order
        {
            for (int x = 0; x < activeAgents.Count; ++x)
            {
                agentLocation = x;
                AgentMove(activeAgents[x]);
            }
            //HUGE HUGE HUGE PROBLEM. Agents appearing from nowhere. This needs to be fixed!
            RemoveBadAgents();
            //This ^^^ function is ONLY a temp stop-gap measure!
        }
        if (caType == 0) // 0 = first order
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                for (int y = 0; y < gridHeight; ++y)
                {
                    int currentState = grid[x, y].agent.currentState;
                    List<int> neighborStateCount = GetNeighborCount(x, y);
                    float[] probChances = GetProbChances(currentState, neighborStateCount);
                    grid[x, y].agent.currentState = GetStateFromProbability(probChances);
                    stateCount[currentState] += 1;
                }
            }
        }
    }

    public void NeighborAnalysis()
    {
        for (int i = 0; i < numStates; ++i)
        {
            stateCountReplacement.Add(0);
            neighborCount.Add(0);
            neighborAnalysis.Add(0);
        }
        for (int x = 0; x < gridWidth; ++x)
        {
            for (int y = 0; y < gridHeight; ++y)
            {
                int currentState = grid[x, y].agent.currentState;
                List<int> neighborStateCount = GetNeighborCount(x, y);
                stateCountReplacement[currentState] += 1;
                for (int i = 0; i < numStates; ++i)
                {
                    if (i == currentState)
                    {
                        for (int j = 0; j < neighborStateCount.Count(); ++j)
                        {
                            if (j == currentState)
                                continue;
                            int tempInt = neighborStateCount[j];
                            int tempInt2 = neighborCount[i];
                            neighborCount[i] = (tempInt + tempInt2);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < numStates; ++i)
        {
            neighborAnalysis[i] = ((float)neighborCount[i] / (8 * stateCountReplacement[i]));
        }
    }

    private List<int> GetNeighborCount(int x, int y)
    {
        List<int> neighborCount = new List<int>();
        for (int i = 0; i < numStates; ++i)
            neighborCount.Add(new int());
        List<Point> neighbors = neighborhood.GetNeighbors(x, y);
        
        //Get a count of each state in our neighborhood
        foreach (Point p in neighbors)
        {
            // We can't change the variable in a foreach iteration
            // So we make a copy
            Point modifiedP = p;

            // if modifiedP is not on the grid, adjust grid
            if (!modifiedP.WithinRange(gridWidth, gridHeight))
            {
                switch (gridType)
                {
                    case GridType.Box:
                        modifiedP = null; // make it null to skip it
                        break;
                    case GridType.CylinderW:
                        modifiedP = Point.AdjustCylinderW(gridWidth, modifiedP);
                        break;
                    case GridType.CylinderH:
                        modifiedP = Point.AdjustCylinderH(gridHeight, modifiedP);
                        break;
                    case GridType.Torus:
                        modifiedP = Point.AdjustTorus(gridWidth, gridHeight, modifiedP);
                        break;
                }
            }
            if (modifiedP == null)
                continue;
            neighborCount[backup[modifiedP.X, modifiedP.Y].agent.currentState]++;
        }
        return neighborCount;
    }

    private float[] GetProbChances(int currentState, List<int> neighborStateCount)
    {
        float[] probChances = new float[numStates];
        float totalProb = 0;

        for (int p = 0; p < numStates; ++p)
        {
            float prob = 0;
            //skip if we are on the state of the current cell
            //we'll figure it out after we find out all the other probs
            if (p == currentState)
                continue;

            //the neighbor probabilities of different states are considered ADDITIVE
            //They will combine to form one probability to that other state

            for (int nP = 0; nP < numStates; ++nP)
            {
                float tempProb = 0;
                if (nP == currentState)
                    continue;
                tempProb = states[currentState].GetProbability((p), nP, neighborStateCount[nP]);
                prob += tempProb;
            }
            probChances[p] = prob;
            totalProb += prob;
        }
        // the chance of us not changing is the product of all the other states not happening
        float noChange = 1 - totalProb;
        probChances[currentState] = noChange > 0 ? noChange : 0;
        return probChances;
    }

    private int GetStateFromProbability(float[] probChances)
    {
        //Should this be float or int?
        float[] agentAmountPerState = GetAgentAmountPerState(probChances.ToList());
        return GetIndexFromRange(agentAmountPerState);
    }

    private int GetIndexFromRange(float[] agentAmountPerState)
    {
        float range = agentAmountPerState[agentAmountPerState.Length - 1];
        float pick = (float)myRand.NextDouble() * range;
        return Array.IndexOf(agentAmountPerState, agentAmountPerState.Where(x => x >= pick).First());
    }

    private float[] GetAgentAmountPerState(List<float> ratios)
    {
        float[] agentAmountPerState = new float[ratios.Count];
        float total = 0;
        for (int i = 0; i < ratios.Count; ++i)
        {
            total += ratios[i];
            agentAmountPerState[i] = total;
        }
        return agentAmountPerState;
    }

    private void AgentMove(AgentController currentAgent)
    {
        //NEED to check things. Need to make check for probability of Random walk itself (encapsulate in IF). Then, check if the new place is already filled.
        //Debug.Log("Old: " + currentAgent.xLocation + ", " + currentAgent.yLocation);
        int newX = currentAgent.xLocation;
        int newY = currentAgent.yLocation;
        int oldX = currentAgent.xLocation;
        int oldY = currentAgent.yLocation;
        double upProb = 0.24;
        double rightProb = 0.24;
        double downProb = 0.28;
        double leftProb = 0.24;
        double randomWalk = myRand.NextDouble();
        if (randomWalk < upProb)
        {
            newY = currentAgent.yLocation + 1;
        }
        else if (randomWalk < (rightProb + upProb) && randomWalk > upProb)
        {
            newX = currentAgent.xLocation + 1;
        }
        else if (randomWalk < (downProb + rightProb + upProb) && randomWalk > (rightProb + upProb))
        {
            newY = currentAgent.yLocation - 1;
        }
        else
        {
            newX = currentAgent.xLocation - 1;
        }
        if (newX > (gridWidth - 1) || newY > (gridHeight - 1) || newX < 0 || newY < 0)
        {
            switch (gridType)
            {
                case GridType.Box:
                    newX = currentAgent.xLocation;
                    newY = currentAgent.yLocation;
                    break;
                case GridType.CylinderW:
                    if (newX >= gridWidth)
                    {
                        newX -= gridWidth;
                        newY = currentAgent.yLocation;
                    }
                    else
                    {
                        newX += gridWidth;
                        newY = currentAgent.yLocation;
                    }
                    break;
                case GridType.CylinderH:
                    if (newY >= gridHeight)
                    {
                        newX = currentAgent.xLocation;
                        newY -= gridHeight;
                    }
                    else
                    {
                        newX = currentAgent.xLocation;
                        newY += gridHeight;
                    }
                    break;
                case GridType.Torus:
                    {
                        if (newX >= gridWidth)
                        {
                            newX -= gridWidth;
                        }
                        else
                        {
                            newX += gridWidth;
                        }
                    }
                    {
                        if (newY >= gridHeight)
                        {
                            newY -= gridHeight;
                        }
                        else
                        {
                            newY += gridHeight;
                        }
                    }
                    break;
            }
        }
        if (newX != oldX || newY != oldY)
        {
            if (grid[newX, newY].containsAgent == false && System.Object.ReferenceEquals(grid[newX, newY].agent, null) == true)
            {
                grid[newX, newY].AddAgent();
                grid[newX, newY].containsAgent = true;
                grid[newX, newY].agent.currentState = currentAgent.currentState;
                grid[newX, newY].agent.xLocation = newX;
                grid[newX, newY].agent.yLocation = newY;
                activeAgents[agentLocation] = grid[newX, newY].agent;
                //Debug.Log("New: " + grid[newX, newY].agent.xLocation + ", " + grid[newX, newY].agent.yLocation);
                grid[oldX, oldY].containsAgent = false;
                grid[oldX, oldY].agent = null;
            }
        }
    }

    private void RemoveBadAgents()
    {
        int agentCleanup = 0;
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (activeAgents.Contains(grid[i, j].agent) == false && grid[i, j].containsAgent == true)
                {
                    grid[i, j].agent = null;
                    grid[i, j].containsAgent = false;
                    agentCleanup++;
                }
            }
        }
    }
}
// Had to comment this out?? Not sure what's to blame.
//public enum GridType
//{
//    Box,
//    CylinderW,
//    CylinderH,
//    Torus
//}