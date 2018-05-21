﻿using Capstone_Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
//using System.Threading.Tasks;

public class CA
{

    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
    public static MobileCA mobileCA = new MobileCA();
    int caType = 0;
    List<AgentController> activeAgents = new List<AgentController>();
    List<List<AgentController>> separateAgents = new List<List<AgentController>>();
    //AgentController[] activeAgentsArray;
    System.Object[,] objects;
    //private BlankGrid blankGrid;
    //AgentController agent;
    private CellState[] states;
    private Neighborhood neighborhood;
    private GridType gridType;
    public BlankGrid[,] grid;
    NType neighborType;
    BlankGrid[,] backup;
    bool getCI = false;
    public int gridWidth;
    public int gridHeight;
    private int numStates;
    public int agentLocation;
    private int connectedVertices;

    //int CICalcs = 0;
    int edgeCalcs = 0;

     List<int> stateCount = new List<int>();
    public List<int> stateCountReplacement = new List<int>();
    public List<int> neighborCount = new List<int>();
    List<int> transitions = new List<int>();
    List<double> cIndexes = new List<double>();
    public List<float> neighborAnalysis = new List<float>();
    private List<Tuple<int, int, int, int>> edges = new List<Tuple<int, int, int, int>>();
    List<Tuple<int, int, int, int>> dataList;

    private StatePageInfo statePageInfo;

    public int CaType { get => caType; set => caType = value; }
    //public AgentController[] ActiveAgentsArray { get => activeAgentsArray; set => activeAgentsArray = value; }

    public List<AgentController> ActiveAgents { get => activeAgents; set => activeAgents = value; }
    public int ConnectedVertices { get => connectedVertices; set => connectedVertices = value; }
    public List<double> CIndexes { get => cIndexes; set => cIndexes = value; }
    public List<int> StateCount { get => stateCount; set => stateCount = value; }
    public List<int> Transitions { get => transitions; set => transitions = value; }
    public List<Tuple<int, int, int, int>> Edges { get => edges; set => edges = value; }
    public List<List<AgentController>> SeparateAgents { get => separateAgents; set => separateAgents = value; }

    public CA(int width, int height, int numStates, NType type, int incomingCAType, bool permitCI,  GridType gType = GridType.Box)
    {
        StateCount.Clear();
        Transitions.Clear();
        CIndexes.Clear();
        gridWidth = width;
        gridHeight = height;
        this.numStates = numStates;
        neighborType = type;
        gridType = gType;
        grid = new BlankGrid[width, height];
        backup = new BlankGrid[width, height];
        neighborhood = new Neighborhood(type);
        states = new CellState[numStates];
        CaType = incomingCAType;
        getCI = permitCI;
        for (int i = 0; i < numStates; ++i)
        {
            StateCount.Add(0);
            CIndexes.Add(0);
            separateAgents.Add(new List<AgentController>());
            states[i] = new CellState(numStates, numStates, neighborhood.GetNeighborSize());
            for (int j = 0; j < (numStates - 1); j++)
            {
                Transitions.Add(0);
            }
        }
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
        //ActiveAgentsArray = new AgentController[gridSize];
        // if total cells < gridSize, check/decide connectivity method - every other point, or what?
        int state = 0;
        
        var list = new List<int>(Enumerable.Range(1, gridSize));
        list.Shuffle();
        // This uses totalCells, not list size, because the "second order" CA will almost always have less cells than the full grid size.
        for (int i = 0; i < totalCells; i++)
        {
            int increment = list[i] - 1;
            int xValue = (increment / gridWidth);
            int yValue = (increment % gridWidth);
            
            grid[xValue, yValue] = new BlankGrid();
            grid[xValue, yValue].AddAgent(xValue, yValue, new AgentController(xValue, yValue));
            grid[xValue, yValue].ContainsAgent = true;
            grid[xValue, yValue].agent.currentState = state;
            grid[xValue, yValue].agent.xLocation = xValue;
            grid[xValue, yValue].agent.yLocation = yValue;
            AddAgent(grid[xValue, yValue].agent);

            //ActiveAgentsArray[] = (grid[xValue, yValue].agent);

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
                        grid[i, j].ContainsAgent = false;
                    }
                }
            }
        }
    }

    public void AddAgent(AgentController agent)
    {
        ActiveAgents.Add(agent);
    }

    public void RemoveAgent(int xLoc, int yLoc)
    {
        foreach(AgentController agent in ActiveAgents)
        {
            if(agent.xLocation == xLoc)
            {
                if(agent.yLocation == yLoc)
                {
                    ActiveAgents.Remove(agent);
                }
            }
        }
    }

    public void SetStateInfo(int startState, int endState, int neighborState, int numNeighbors, float prob)
    {
        states[startState].SetProbability(endState, neighborState, numNeighbors, prob);

    }

    public void Set2ndOrder(int startState, double[] walkProbs, List<double> stickingProbs, bool sticking, int mobileNeighborhood)
    {
        states[startState].Set2ndOrderInfo(walkProbs, stickingProbs, sticking, mobileNeighborhood);
    }

    public void SetStateInfo(int startState, int endState, int neighborState, int rows, int columns, double prob)
    {
        states[startState].SetProbability(endState, neighborState, rows, columns, prob);
    }

    public void CreateStateArray(int startState, int endState, int neighborState, int rows, int columns)
    {
        states[startState].InitializeArray(endState, neighborState, rows, columns);
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
        for (int i = 0; i < numStates; i++)
        {
            separateAgents[i].Clear();
        }
        //CICalcs = 0;
        Array.Copy(grid, backup, gridWidth * gridHeight);
        for (int i = 0; i < numStates; ++i)
        {
            StateCount[i] = 0;
            for (int j = 0; j < (numStates - 1); j++)
            {
                Transitions[(i * (numStates - 1)) + j] = 0;
            }
        }
            
        if (CaType == 1) //1 = second order
        {
            for (int x = 0; x < ActiveAgents.Count; ++x)
            {
                agentLocation = x;
                if(CheckForMovement(ActiveAgents[x]))
                {
                    AgentMove(ActiveAgents[x]);
                }
                ActiveAgents[x].AddHistory();
            }
        }
        if (CaType == 0) // 0 = first order
        {
            {
                // int length = gridWidth * gridHeight;
                // int count = 0;
                // Parallel.For(0, length, i =>
                //{
                //    count++;
                //    int x = (i / gridWidth);
                //    int y = (i % gridWidth);
                //    Console.WriteLine("Length" + length + " Count: " + count);
                //    if (grid[x, y].ContainsAgent)
                //    {
                //        int oldState = grid[x, y].agent.currentState;
                //        if (neighborType == NType.Advanced)
                //        {
                //            double[] probChances = AdvancedGetProbChances(oldState, x, y);
                //            grid[x, y].agent.currentState = GetStateFromProbability(probChances);
                //        }
                //        else
                //        {
                //            List<int> neighborStateCount = GetNeighborCount(x, y);
                //            double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
                //            grid[x, y].agent.currentState = GetStateFromProbability(probChances);
                //        }
                //        int newState = grid[x, y].agent.currentState;
                //        CheckTransitions(oldState, newState);
                //        stateCount[newState] += 1;
                //    }
                //});
            }
            for (int x = 0; x < gridWidth; ++x)
            {
                for (int y = 0; y < gridHeight; ++y)
                {
                    if (grid[x, y].ContainsAgent)
                    {
                        int oldState = grid[x, y].agent.currentState;
                        if (neighborType == NType.Advanced)
                        {
                            double[] probChances = AdvancedGetProbChances(oldState, x, y);
                            grid[x, y].agent.currentState = GetStateFromProbability(probChances);
                        }
                        else
                        {
                            List<int> neighborStateCount = GetNeighborCount(x, y, oldState);
                            double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
                            grid[x, y].agent.currentState = GetStateFromProbability(probChances);
                        }
                        int newState = grid[x, y].agent.currentState;
                        separateAgents[newState].Add(grid[x, y].agent);
                        CheckTransitions(oldState, newState);
                        StateCount[newState] += 1;
                    }
                }
            }
        }

        if(getCI)
        {
            for (int i = 0; i < numStates; i++)
            {
                GetCIndex(i);
            }
        }
    }

    // clearly not working correctly. up to 5 times SLOWER than other method.
    //async void ASyncRun()
    //{
    //    dataList = new List<Tuple<int, int, int, int>>();
    //    List<Task<Tuple<int, int, int, int>>> tasks = new List<Task<Tuple<int, int, int, int>>>();
    //    for (int x = 0; x < gridWidth; ++x)
    //    {
    //        for (int y = 0; y < gridHeight; ++y)
    //        {
    //            // create tuple at end of operation returning location and new state.
    //            // Then move through grid changing each location (this part must be synchronous)
    //            tasks.Add(ASyncIndividualRun(x, y));
    //        }
    //    }
    //    //Console.WriteLine("Number of entries: " + tasks.Count);
    //    await Task.WhenAll(tasks);
    //    //when changing grid, make sure to check if an actual agent is there. If none, skip reassignment
    //    //Console.WriteLine("I'll never forget...");
    //    foreach (Task<Tuple<int, int, int, int>> task in tasks)
    //    {
    //        //Console.WriteLine(task.Result);
    //        //Console.WriteLine("We're here because we're here");
    //        dataList.Add(task.Result);
            
    //    }
        
    //}

    //async Task<Tuple<int, int, int, int>> ASyncIndividualRun(int x, int y)
    //{
    //    if (grid[x, y].ContainsAgent)
    //    {
    //        int newState;
    //        int oldState = grid[x, y].agent.currentState;
    //        // change when updating agent type setup
    //        if (neighborType == NType.Advanced)
    //        {
    //            double[] probChances = AdvancedGetProbChances(oldState, x, y);
    //            newState = GetStateFromProbability(probChances);
    //            //dataList.Add(new Tuple<int, int, int>(x, y, ));
    //        }
    //        else
    //        {
    //            List<int> neighborStateCount = GetNeighborCount(x, y);
    //            double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
    //            newState = GetStateFromProbability(probChances);
    //        }
    //        return new Tuple<int, int, int, int>(x, y, oldState, newState);

    //        //CheckTransitions(oldState, newState);
    //        //stateCount[newState] += 1;
    //    }
    //    else
    //    {
    //        return new Tuple<int, int, int, int>(x, y, -1, -1);
    //    }
    //}

    public void CheckTransitions(int oldState, int newState)
    {
        if(oldState != newState)
        {
            int tempNew = newState;
            if(newState > oldState)
            {
                tempNew = newState - 1;
            }
            Transitions[(oldState * (numStates - 1)) + tempNew] += 1;
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
                List<int> neighborStateCount = GetNeighborCount(x, y, currentState);
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

    public void GetCIndex(int state)
    {
        //CICalcs++;
        //Console.WriteLine("GetCIndex " + CICalcs);
        edgeCalcs = 0;
        Edges.Clear();
        ConnectedVertices = 0;
        //double connectivityIndex = 0;
        //double maxNeighbors = (gridWidth * gridHeight) * neighborhood.GetNeighborSize();
        int amount = StateCount[state];
        int neighborhoodFactor = 0;
        int maxEdges = 0;

        switch(neighborType)
        {
            case NType.None:
                neighborhoodFactor = 0;
                break;
            case NType.VonNeumann:
                neighborhoodFactor = 2;
                break;
            case NType.Moore:
                neighborhoodFactor = 4;
                break;
            case NType.Hybrid:
                // This is an estimate!
                neighborhoodFactor = 6;
                break;
            case NType.Advanced:
                //??? You tell me
                break;
        }

        // Determine the # edges for a maximally clustered graph
        {
            int n = amount;
            int s = (int)Math.Floor(Math.Sqrt((double)n));
            int x = s;
            int r = n - (s * s);
            int f = 0;
            int y = s;
            if(r != 0)
            {
                if (r > s)
                {
                    int t = r - s;
                    f = ((t - 1) * neighborhoodFactor) + 1;
                    y = s + 1;
                }
                else
                {
                    f = ((r - 1) * neighborhoodFactor) + 1;
                }
            }
            switch(neighborType)
            {
                case NType.None:
                    //Worry about later
                    break;
                case NType.VonNeumann:
                    maxEdges = ((x * (y - 1)) + (y * (x - 1))) + f;
                    break;
                case NType.Moore:
                    maxEdges = ((x * (y - 1)) + (y * (x - 1)) + (2 * (x - 1) * (y - 1))) + f;
                    break;
                case NType.Hybrid:
                    // Worry about later
                    break;
                case NType.Advanced:
                    //??? You tell me
                    break;
            }
        }
        for (int j = 0; j < separateAgents[state].Count; j++)
        {
            if (separateAgents[state][j].currentState == state)
            {
                // check if neighbors are of current state
                // Add neighbors to an edge count
                GetEdges(separateAgents[state][j].xLocation, separateAgents[state][j].yLocation, state);
            }
        }
        int finalEdge = edgeCalcs / 2;
        //Console.WriteLine("Old:" + Edges.Count + " New: " + edgeCalcs);
        double cIndex = 0;
        if(amount > 1)
        {
            cIndex = (finalEdge / (double)maxEdges) * ((double)ConnectedVertices / amount);
        }
        CIndexes[state] = cIndex;
    }

    private List<int> GetNeighborCount(int x, int y, int oldState)
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
            // Check that modifiedP exists--that it's not an empty spot

            if (modifiedP == null)
                continue;
            //Console.WriteLine("Values: " + modifiedP.X + "," + modifiedP.Y);
            if (backup[modifiedP.X, modifiedP.Y].ContainsAgent)
            {
                neighborCount[backup[modifiedP.X, modifiedP.Y].agent.currentState]++;
            }
        }
        return neighborCount;
    }

    // Gets neighbor count for the current grid
    private void GetEdges(int x, int y, int centerState)
    {
        bool connections = false;
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
            // Check that modifiedP exists--that it's not an empty spot

            if (modifiedP == null)
                continue;
            if (grid[modifiedP.X, modifiedP.Y].ContainsAgent)
            {
                if (connections == false)
                {
                    ConnectedVertices += 1;
                    connections = true;
                }
                if (grid[modifiedP.X, modifiedP.Y].agent.currentState == centerState)
                {
                    edgeCalcs++;
                    //Tuple<int, int, int, int> forward = new Tuple<int, int, int, int>(x, y, modifiedP.X, modifiedP.Y);
                    //Tuple<int, int, int, int> backward = new Tuple<int, int, int, int>(modifiedP.X, modifiedP.Y, x, y);
                    //if (Edges.Contains(forward))
                    //{
                    //    continue;
                    //}
                    //else if (Edges.Contains(backward))
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    Edges.Add(forward);
                    //}
                }
            }
        }
    }

    private double[] AdvancedGetProbChances(int currentState, int x, int y)
    {
        double[] probChances = new double[numStates];
        double totalProb = 0;

        //int minRows = 0;

        //int distance = ((minRows - 1) / 2);

        for (int p = 0; p < numStates; ++p)
        {
            double prob = 0;
            //skip if we are on the state of the current cell
            //we'll figure it out after we find out all the other probs
            if (p == currentState)
                continue;
            //the neighbor probabilities of different states are considered ADDITIVE
            //They will combine to form one probability to that other state

            for (int nP = 0; nP < numStates; ++nP)
            {
                int tempRows = states[currentState].advProbs[p, nP].GetLength(0);
                int distance = ((tempRows - 1) / 2);
                double tempProb = 0;
                for(int temp2Rows = 0; temp2Rows < tempRows; temp2Rows++)
                {
                    for(int temp2Cols = 0; temp2Cols < tempRows; temp2Cols++)
                    {
                        int newRow = (x - distance + temp2Rows);
                        int newCol = (y - distance + temp2Cols);

                        if (newRow < 0 || newRow >= gridWidth || newCol < 0 || newCol >= gridHeight)
                            continue;

                        // Add check for if new location is within bounds
                        if(grid[newRow,newCol].agent.currentState == nP)
                        {
                            tempProb = states[currentState].GetProbability(p, nP, temp2Rows, temp2Cols);
                            prob += tempProb;
                        }
                    }
                }
            }
            probChances[p] = prob;
            totalProb += prob;
        }

        // the chance of us not changing is the product of all the other states not happening
        double noChange = 1 - totalProb;
        probChances[currentState] = noChange > 0 ? noChange : 0;
        return probChances;
    }

    private double[] StandardGetProbChances(int currentState, List<int> neighborStateCount)
    {
        double[] probChances = new double[numStates];
        double totalProb = 0;

        for (int p = 0; p < numStates; ++p)
        {
            double prob = 0;
            //skip if we are on the state of the current cell
            //we'll figure it out after we find out all the other probs
            if (p == currentState)
                continue;

            //the neighbor probabilities of different states are considered ADDITIVE
            //They will combine to form one probability to that other state

            for (int nP = 0; nP < numStates; ++nP)
            {
                double tempProb = 0;
                if (nP == currentState)
                    continue;
                tempProb = states[currentState].GetProbability((p), nP, neighborStateCount[nP]);
                prob += tempProb;
            }
            probChances[p] = prob;
            totalProb += prob;
        }
        // the chance of us not changing is the product of all the other states not happening
        double noChange = 1 - totalProb;
        probChances[currentState] = noChange > 0 ? noChange : 0;
        return probChances;
    }

    private int GetStateFromProbability(double[] probChances)
    {
        //Should this be float or double?
        double[] agentAmountPerState = GetAgentAmountPerState(probChances.ToList());
        return GetIndexFromRange(agentAmountPerState);
    }

    private int GetIndexFromRange(double[] agentAmountPerState)
    {
        double range = agentAmountPerState[agentAmountPerState.Length - 1];
        var rng = new RNGCryptoServiceProvider();
        var bytes = new Byte[8];
        rng.GetBytes(bytes);
        var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
        Double randomDouble = ul / (Double)(1UL << 53);

        double pick = randomDouble * range;
        return Array.IndexOf(agentAmountPerState, agentAmountPerState.Where(x => x >= pick).First());
    }

    private double[] GetAgentAmountPerState(List<double> ratios)
    {
        double[] agentAmountPerState = new double[ratios.Count];
        double total = 0;
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
        var rng = new RNGCryptoServiceProvider();
        var bytes = new Byte[8];
        rng.GetBytes(bytes);
        var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
        Double randomWalk = ul / (Double)(1UL << 53);
        double upProb = states[currentAgent.currentState].walkProbs[0];
        double rightProb = states[currentAgent.currentState].walkProbs[1];
        double downProb = states[currentAgent.currentState].walkProbs[2];
        double leftProb = states[currentAgent.currentState].walkProbs[3];

        double newRight = (upProb + rightProb);
        double newDown = newRight + downProb;
        double newLeft = newDown + leftProb;

        Console.WriteLine("Prob: " + randomWalk + " up: " + upProb + " right: " + newRight + " down: " + newDown + " left: " + newLeft);

        if (randomWalk < upProb)
        {
            newY = currentAgent.yLocation - 1;
        }
        else if (randomWalk < (rightProb + upProb) && randomWalk > upProb)
        {
            newX = currentAgent.xLocation + 1;
        }
        else if (randomWalk < (downProb + rightProb + upProb) && randomWalk > (rightProb + upProb))
        {
            newY = currentAgent.yLocation + 1;
        }
        else
        {
            newX = currentAgent.xLocation - 1;
        }
        Tuple<bool, int, int> tempTuple = GridCheckForLocation(newX, newY);
        if(tempTuple.Item1)
        {
            if (tempTuple.Item2 != oldX || tempTuple.Item3 != oldY)
            {
                //Console.WriteLine("Old location: " + newX + "," + newY + " New Location" + tempTuple.Item2 + "," + tempTuple.Item3);
                if (grid[tempTuple.Item2, tempTuple.Item3].ContainsAgent == false && System.Object.ReferenceEquals(grid[tempTuple.Item2, tempTuple.Item3].agent, null) == true)
                {
                    grid[tempTuple.Item2, tempTuple.Item3].AddAgent(newX, newY, currentAgent);
                    grid[tempTuple.Item2, tempTuple.Item3].ContainsAgent = true;
                    grid[tempTuple.Item2, tempTuple.Item3].agent.currentState = currentAgent.currentState;
                    grid[tempTuple.Item2, tempTuple.Item3].agent.xLocation = tempTuple.Item2;
                    grid[tempTuple.Item2, tempTuple.Item3].agent.yLocation = tempTuple.Item3;
                    ActiveAgents[agentLocation] = grid[tempTuple.Item2, tempTuple.Item3].agent;
                    //Debug.Log("New: " + grid[newX, newY].agent.xLocation + ", " + grid[newX, newY].agent.yLocation);
                    grid[oldX, oldY].ContainsAgent = false;
                    grid[oldX, oldY].agent = null;
                }
            }
        }
    }

    //private int NewXValue (int newX)
    //{ Put this after the previous step. Have previous step only return true/false
    //    if (newX > (gridWidth - 1) || newY > (gridHeight - 1) || newX < 0 || newY < 0)
    //{
    //    switch (gridType)
    //    {
    //        case GridType.Box:
    //            return false;
    //        case GridType.CylinderW:
    //            if (newY > (gridHeight - 1) || newY < 0)
    //                return false;
    //            else
    //            {
    //                return true;
    //                // THIS IS NOT CORRECT: THIS NEEDS TO BE CHANGED TO RETURN THESE MODIFIED VALUES
    //                //if (newX >= gridWidth)
    //                //{
    //                //    newX -= gridWidth;
    //                //    return true;
    //                //}
    //                //else
    //                //{
    //                //    newX += gridWidth;
    //                //    return true;
    //                //}
    //            }
    //        case GridType.CylinderH:
    //            if (newX > (gridHeight - 1) || newX < 0)
    //                return false;
    //            else
    //            {
    //                return true;
    //                // SEE ABOVE
    //                //if (newY >= gridHeight)
    //                //{
    //                //    newY -= gridHeight;
    //                //}
    //                //else
    //                //{
    //                //    newY += gridHeight;
    //                //}
    //            }
    //        case GridType.Torus:
    //            {
    //                return true;
    //                //    if (newX >= gridWidth)
    //                //    {
    //                //        newX -= gridWidth;
    //                //    }
    //                //    else
    //                //    {
    //                //        newX += gridWidth;
    //                //    }
    //                //}
    //                //{
    //                //    if (newY >= gridHeight)
    //                //    {
    //                //        newY -= gridHeight;
    //                //    }
    //                //    else
    //                //    {
    //                //        newY += gridHeight;
    //                //    }
    //            }
    //    }
    //}
    //else
    //    return true;
    //}

    private bool IsReal(int newX, int newY)
    {
        switch (gridType)
        {
            case GridType.Box:
                if (newX > (gridWidth - 1) || newY > (gridHeight - 1) || newX < 0 || newY < 0)
                    return false;
                else
                    return true;
            case GridType.CylinderW:
                if (newY > (gridHeight - 1) || newY < 0)
                    return false;
                else
                {
                    return true;
                }
            case GridType.CylinderH:
                if (newX > (gridHeight - 1) || newX < 0)
                    return false;
                else
                {
                    return true;
                }
            case GridType.Torus:
                return true;
            default:
                return false;
        }
    }

    private bool CheckForMovement(AgentController currentAgent)
    {
        // Make method to check for agent in surrounding area. the "surrounding area" will of course need
        // to be extensible to the 4 or 8. Can use for this method and for agentmove.
        if (states[currentAgent.currentState].sticking)
        {
            // Add check for type???
            List<Tuple<int, int>> neighborList = GetNeighborhood(currentAgent);

            for (int i = 0; i < neighborList.Count; i++)
            {
                Tuple<bool, int> tempTuple = AgentCheck(neighborList[i].Item1, neighborList[i].Item2);
                if(tempTuple.Item1)
                {
                    var rng = new RNGCryptoServiceProvider();
                    var bytes = new Byte[8];
                    rng.GetBytes(bytes);
                    var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                    Double rand = ul / (Double)(1UL << 53);
                    double stayProb = states[currentAgent.currentState].stickingProbs[tempTuple.Item2];
                    if (rand < stayProb)
                        return false;
                    else
                        return true;
                }
                //check if locations contain agents. If so, run sticking prob
            }
            return true;
        }
        else
            return true;
    }

    List<Tuple<int, int>> GetNeighborhood(AgentController agent)
    {
        List<Tuple<int, int>> neighborList = new List<Tuple<int, int>>();
        switch(states[agent.currentState].mobileNeighborhood)
        {
            case 0:
                for(int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if((Math.Abs(i) + Math.Abs(j)) % 2 != 0)
                        {
                            Tuple<bool, int, int> tempTuple = GridCheckForLocation(agent.xLocation + i, agent.yLocation + j);
                            if (tempTuple.Item1)
                            {
                                Tuple<int, int> temperTuple = new Tuple<int, int>(tempTuple.Item2, tempTuple.Item3);
                                neighborList.Add(temperTuple);
                            }
                        }
                    }
                }
                break;
            case 1:
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((Math.Abs(i) + Math.Abs(j)) == 0)
                        {
                            continue;
                        }
                        Tuple<bool, int, int> tempTuple = GridCheckForLocation(agent.xLocation + i, agent.yLocation + j);
                        if (tempTuple.Item1)
                        {
                            Tuple<int, int> temperTuple = new Tuple<int, int>(tempTuple.Item2, tempTuple.Item3);
                            neighborList.Add(temperTuple);
                        }
                    }
                }
                break;
            default:
                break;
        }
        return neighborList;

    }

    Tuple<bool, int, int> GridCheckForLocation(int x, int y)
    {
        //Console.WriteLine("Old location: " + x + "," + y);
        switch (gridType)
        {
            case GridType.Box:
                if(x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
                {
                    return new Tuple<bool, int, int>(false, x, y);
                }
                else
                {
                    //Console.WriteLine("New location: " + x + "," + y);
                    return new Tuple<bool, int, int>(true, x, y);
                }
                break;
            case GridType.CylinderW:
                if(y < 0 || y >= gridHeight)
                {
                    return new Tuple<bool, int, int>(false, x, y);
                }
                else
                {
                    if(x < 0)
                    {
                        x += gridWidth;
                    }
                    else if(x >= gridWidth)
                    {
                        x -= gridWidth;
                    }
                    //Console.WriteLine("New location: " + x + "," + y);
                    return new Tuple<bool, int, int>(true, x, y);
                }
                break;
            case GridType.CylinderH:
                if (x < 0 || x >= gridWidth)
                {
                    return new Tuple<bool, int, int>(false, x, y);
                }
                else
                {
                    if (y < 0)
                    {
                        y += gridHeight;
                    }
                    else if (y >= gridHeight)
                    {
                        y -= gridHeight;
                    }
                    //Console.WriteLine("New location: " + x + "," + y);
                    return new Tuple<bool, int, int>(true, x, y);
                }
                break;
            case GridType.Torus:
                {
                    if(x < 0)
                    {
                        x += gridWidth;
                    }
                    else if(x >= gridWidth)
                    {
                        x -= gridWidth;
                    }
                    if(y < 0)
                    {
                        y += gridHeight;
                    }
                    else if(y >= gridHeight)
                    {
                        y -= gridHeight;
                    }
                    //Console.WriteLine("New location: " + x + "," + y);
                    return new Tuple<bool, int, int>(true, x, y);
                }
                break;
        }
        return new Tuple<bool, int, int>(false, x, y);
    }

    Tuple<bool, int> AgentCheck(int x, int y)
    {
        if (grid[x, y].ContainsAgent == false)
        {
            return new Tuple<bool, int>(false, -1);
        }
        else
        {
            return new Tuple<bool, int>(true, grid[x, y].agent.currentState);
        }
    }

    //private void RemoveBadAgents()
    //{
    //    int agentCleanup = 0;
    //    for (int i = 0; i < gridWidth; i++)
    //    {
    //        for (int j = 0; j < gridHeight; j++)
    //        {
    //            if (activeAgents.Contains(grid[i, j].agent) == false && grid[i, j].containsAgent == true)
    //            {
    //                grid[i, j].agent = null;
    //                grid[i, j].containsAgent = false;
    //                agentCleanup++;
    //            }
    //        }
    //    }
    //}
}

public static class ThreadSafeRandom
{
    [ThreadStatic] private static Random Local;

    public static Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
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