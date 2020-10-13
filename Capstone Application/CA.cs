using Capstone_Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
//using System.Threading.Tasks;

public class CA
{

    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    ControllerScript controller;
    Template template;
    bool template_reset = false;
    bool containerObjects = false;
    public static MobileCA mobileCA = new MobileCA();
    //int caType = 0;
    List<List<AgentController>> separateAgents = new List<List<AgentController>>();
    //AgentController[] activeAgentsArray;
    //System.Object[,] objects;
    //private BlankGrid blankGrid;
    //AgentController agent;
    private CellState[] states;
    private List<NType> neighborhoods;
    //private List<GridType> grids;
    //private GridType gridType;
    public BlankGrid[,] grid;
    List<NType> neighborTypes;
    static int[,] backup;
    public int gridWidth;
    public int gridHeight;
    private int numStates;
    public int agentLocation;
    private int connectedVertices;

    //int CICalcs = 0;
    int edgeCalcs = 0;

    public List<int> stateCountReplacement = new List<int>();
    public List<int> neighborCount = new List<int>();
    List<double> cIndexes = new List<double>();
    public List<float> neighborAnalysis = new List<float>();
    private List<Tuple<int, int, int, int>> edges = new List<Tuple<int, int, int, int>>();
    //List<Tuple<int, int, int, int>> dataList;

    private List<StatePageInfo> infos;
    private List<List<object>> generics = new List<List<object>>();

    //public int CaType { get => caType; set => caType = value; }
    //public AgentController[] ActiveAgentsArray { get => activeAgentsArray; set => activeAgentsArray = value; }

    public List<AgentController> ActiveAgents { get; private set; }
    public int ConnectedVertices { get => connectedVertices; set => connectedVertices = value; }
    public List<double> CIndexes { get => cIndexes; set => cIndexes = value; }
    public ConcurrentDictionary<int, int> StateCount { get; set; }
    public ConcurrentDictionary<Tuple<int, int>, int> Transitions { get; private set; }
    public List<Tuple<int, int, int, int>> Edges { get => edges; set => edges = value; }
    private List<List<AgentController>> SeparateAgents { get => separateAgents; set => separateAgents = value; }

    public CA(ControllerScript control, int width, int height, int numStates, List<NType> types, List<StatePageInfo> info, Template incTemplate, bool templateReset = false)
    {
        controller = control;
        template = incTemplate;
        template_reset = templateReset;
        StateCount = new ConcurrentDictionary<int, int>();
        Transitions = new ConcurrentDictionary<Tuple<int, int>, int>();
        CIndexes.Clear();
        gridWidth = width;
        gridHeight = height;
        this.numStates = numStates;
        neighborTypes = types;
        infos = info;
        grid = new BlankGrid[width, height];
        backup = new int[width, height];
        neighborhoods = types;
        ActiveAgents = new List<AgentController>();
        //neighborhood = new Neighborhood(type);
        states = new CellState[numStates];
        for (int i = 0; i < numStates; ++i)
        {
            StateCount.AddOrUpdate(i, 0, (k, v) => 0);
            CIndexes.Add(0);
            separateAgents.Add(new List<AgentController>());
            states[i] = new CellState(numStates, numStates, info[i]);
            for (int j = 0; j < numStates; j++)
            {
                Transitions.AddOrUpdate(new Tuple<int, int>(i,j), 0, (k, v) => 0);
            }
            generics.Add(info[i].template_objects);
            if(info[i].containerSettings != null && info[i].containerSettings.Count > 0)
            {
                containerObjects = true;
                states[i].containerSettings = info[i].containerSettings;
            }
        }
        TemplateCheck();
    }

    public CellState GetStateInfo(int i)
    {
        return states[i];
    }

    void TemplateCheck()
    {
        switch(template)
        {
            case Template.DLA:
                {
                    double a = (double)gridWidth / 2;
                    double b = (double)gridHeight / 2;
                    double a_floor = Math.Floor(a);
                    double b_floor = Math.Floor(b);
                    List<Tuple<int, int>> locations = new List<Tuple<int, int>>();
                    int total = gridWidth * gridHeight;
                    double val = 360.0 / total;
                    for (int i = 0; i < total; i++)
                    {
                        double use = val * i;
                        double rad = (use * Math.PI) / 180.0;
                        double x = a_floor * Math.Cos(rad);
                        double y = b_floor * Math.Sin(rad);

                        Tuple<int, int> loc = new Tuple<int, int>((int)(Math.Floor(x) + a_floor), (int)(Math.Floor(y) + b_floor));
                        locations.Add(loc);
                    }
                    var result = locations.Distinct().ToList();
                    generics.Add(new List<object>() { result });
                }
                break;
            case Template.Gas:
                {

                }
                break;
        }
        
    }

    public void InitializeGrid(List<int> cellAmounts)
    {
        // This method creates the grid after the user inputs the necessary initial and transition information

        // make sure ratios is as big as our numStates

        //Get a list of how many cells of each type we need
        List<int> agentCount = new List<int>();
        int totalCells = 0;
        for (int i = 0; i < numStates; ++i)
        {
            StateCount.AddOrUpdate(i, 0, (k, v) => cellAmounts[i]);
            agentCount.Add(cellAmounts[i]);
            totalCells += cellAmounts[i];
        }

        // this is a list of which states still needed to be added

        int gridSize = (gridWidth * gridHeight);
        //ActiveAgentsArray = new AgentController[gridSize];
        // if total cells < gridSize, check/decide connectivity method - every other point, or what?
        int state = 0;
        int reducer = 0;
        
        var list = new List<int>(Enumerable.Range(1, gridSize));
        list.Shuffle();

        // Checks for custom-placed locations. If there are any, places them, removes from the appropriate agentCount list,
        // then adds to an increment which will lower the totalCells increment below.

        for (int i = 0; i < numStates; i++)
        {
            if (states[i].startingLocations == null)
            {
                continue;
            }
            else
            {
                if (states[i].startingLocations.Count > 0)
                {
                    for (int j = 0; j < states[i].startingLocations.Count; j++)
                    {
                        int x = states[i].startingLocations[j].Item1;
                        int y = states[i].startingLocations[j].Item2;
                        int combination = (x * gridWidth) + y;
                        grid[x, y] = new BlankGrid(x, y, new AgentController(x, y, i, this, grid[x, y]), this);
                        grid[x, y].Agent.AddContainer(states[i].containerSettings);
                        AddAgent(grid[x, y].Agent);
                        separateAgents[i].Add(grid[x, y].Agent);

                        list.Remove(combination);
                        reducer++;

                        // Subtract that state from its agentcount
                        // And if it goes to zero remove it from both our lists

                        agentCount[i]--;

                        if (agentCount[i] == 0)
                        {
                            agentCount.RemoveAt(i);
                            if (i == 0)
                            {
                                state += 1;
                            }
                        }
                    }
                }
            }
        }

        // This uses totalCells, not list size, because the "second order" CA will almost always have less cells than the full grid size.
        for (int i = 0; i < (totalCells - reducer); i++)
        {
            int increment = list[i] - 1;
            int xValue = (increment / gridWidth);
            int yValue = (increment % gridWidth);
            
            grid[xValue, yValue] = new BlankGrid(xValue, yValue, new AgentController(xValue, yValue, state, this, grid[xValue, yValue]), this);
            grid[xValue, yValue].Agent.AddContainer(states[state].containerSettings);
            AddAgent(grid[xValue, yValue].Agent);
            separateAgents[state].Add(grid[xValue, yValue].Agent);

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
                        grid[i, j] = new BlankGrid(i, j, this);
                    }
                }
            }
        }
    }

    public void AddAgent(AgentController agent)
    {
        agent.walkProbs = states[agent.currentState].walkProbs;
        ActiveAgents.Add(agent);
    }

    public void RemoveAgent(int xLoc, int yLoc)
    {
        for (int i = 0; i < ActiveAgents.Count; i++)
        {
            var agent = ActiveAgents[i];
            if (agent.X == xLoc)
            {
                if (agent.Y == yLoc)
                {
                    ActiveAgents.Remove(agent);
                    grid[xLoc, yLoc].RemoveAgent();
                }
            }
        }
    }

    public void OneIteration()
    {
        for (int i = 0; i < numStates; i++)
        {
            separateAgents[i].Clear();
        }
        //CICalcs = 0;
        Backup();
        for (int i = 0; i < numStates; ++i)
        {
            StateCount.AddOrUpdate(i, 0, (k, v) => 0);
            for (int j = 0; j < numStates; j++)
            {
                Transitions.AddOrUpdate(new Tuple<int, int>(i,j), 0, (k, v) => 0);
            }
        }
        if (template == Template.None || template == Template.Random_Walk)
        {
            if(states.Any(x => x.mobile))
            {
                HandleMobileAgents();
            }
            else
            {
                HandleImmobileAgents();
            }            
        }
        else if(template == Template.DLA)
        {
            DLARoutine();
        }
        else if (template == Template.Gas)
        {
            GasRoutine();
        }
    }

    public void UpdateCIndex()
    {
        for (int i = 0; i < numStates; i++)
        {
            GetCIndex(i);
        }
    }

    void Backup()
    {
        Parallel.For(0, gridWidth, (x) =>
        {
            Parallel.For(0, gridHeight, (y) =>
            {
                if (grid[x, y].ContainsAgent)
                {
                    backup[x, y] = grid[x, y].Agent.currentState;
                }
                else
                {
                    backup[x, y] = -1;
                }
            });
        });
    }

    void HandleMobileAgents()
    {
        for (int x = 0; x < ActiveAgents.Count; ++x)
        {
            if (ActiveAgents[x].Busy)
            {
                ActiveAgents[x].Execute();
            }
            else
            {
                if (states[ActiveAgents[x].currentState].mobile)
                {
                    if (CheckForMovement(ActiveAgents[x]))
                    {
                        AgentMove(ActiveAgents[x], x);
                    }
                    ActiveAgents[x].AddHistory();
                }
                else
                {
                    int oldState = ActiveAgents[x].currentState;
                    int xLoc = ActiveAgents[x].X;
                    int yLoc = ActiveAgents[x].Y;
                    if (neighborTypes[oldState] == NType.Advanced)
                    {
                        double[] probChances = AdvancedGetProbChances(oldState, xLoc, yLoc);
                        ActiveAgents[x].currentState = GetStateFromProbability(probChances);
                    }
                    else
                    {
                        List<int> neighborStateCount = GetNeighborCount(xLoc, yLoc, oldState);
                        double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
                        ActiveAgents[x].currentState = GetStateFromProbability(probChances);
                    }
                    int newState = ActiveAgents[x].currentState;
                    separateAgents[newState].Add(ActiveAgents[x]);
                    CheckTransitions(oldState, newState);
                    StateCount.AddOrUpdate(newState, 0, (k, v) => v + 1);
                }
                if (containerObjects)
                {
                    ActiveAgents[x].HandleContainer();
                }
            }
        }
    }

    void HandleImmobileAgents()
    {
        List<int> neighbors = Enumerable.Repeat(0, states.Length).ToList();
        
        Parallel.For(0, ActiveAgents.Count, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount}, (i) =>
        {
            if (ActiveAgents[i].Busy)
            {
                ActiveAgents[i].Execute();
            }
            else
            {
                int oldState = ActiveAgents[i].currentState;

                if (neighborTypes[oldState] == NType.None)
                {
                    double[] probChances = StandardGetProbChances(oldState, neighbors);
                    ActiveAgents[i].currentState = GetStateFromProbability(probChances);
                }
                else if (neighborTypes[oldState] == NType.Advanced)
                {
                    double[] probChances = AdvancedGetProbChances(oldState, ActiveAgents[i].X, ActiveAgents[i].Y);
                    ActiveAgents[i].currentState = GetStateFromProbability(probChances);
                }
                else
                {
                    List<int> neighborStateCount = GetNeighborCount(ActiveAgents[i].X, ActiveAgents[i].Y, oldState);
                    double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
                    ActiveAgents[i].currentState = GetStateFromProbability(probChances);
                }

                int newState = ActiveAgents[i].currentState;
                lock(separateAgents)
                {
                    separateAgents[newState].Add(ActiveAgents[i]);
                }
                CheckTransitions(oldState, newState);
                StateCount.AddOrUpdate(newState, 0, (key, value) => value + 1);
                if (containerObjects)
                {
                    ActiveAgents[i].HandleContainer();
                }
            }
        });
    }

    public List<List<Tuple<int, int, int>>> GetPaths()
    {
        List<List<Tuple<int, int, int>>> paths = new List<List<Tuple<int, int, int>>>();
        for (int i = 0; i < ActiveAgents.Count; i++)
        {
            paths.Add(ActiveAgents[i].History);
        }
        return paths;
    }

    void DLARoutine()
    {
        
        AgentController cur = ActiveAgents[ActiveAgents.Count - 1];
        List<Tuple<int, int>> locations = (List<Tuple<int, int>>)generics[2][0];
        if (cur.currentState == 0)
        {
            for (int i = 0; i < ActiveAgents.Count; i++)
            {
                StateCount.AddOrUpdate(ActiveAgents[i].currentState, 0, (k, v) => v + 1);
                ActiveAgents[i].HistoryChange = false;
            }
            if(locations.Count == 0)
            {
                controller.Pause();
            }
            locations.Shuffle();
            int x = locations[0].Item1;
            int y = locations[0].Item2;
            grid[x, y].AddAgent(new AgentController(x, y, 1, this, grid[x, y]));
            AddAgent(grid[x, y].Agent);
            StateCount.AddOrUpdate(ActiveAgents.Last().currentState, 0, (k, v) => v + 1);
        }
        else if (cur.currentState == 1)
        {
            for (int i = 0; i < ActiveAgents.Count - 1; i++)
            {
                StateCount.AddOrUpdate(ActiveAgents[i].currentState, 0, (k, v) => v + 1);
                ActiveAgents[i].HistoryChange = false;
            }
            if (CheckForMovement(cur))
            {
                AgentMove(cur, ActiveAgents.Count - 1);
            }
            cur.AddDLAHistory();
            int oldState = cur.currentState;
            int xLoc = cur.X;
            int yLoc = cur.Y;
            List<int> neighborStateCount = GetNeighborCount(xLoc, yLoc, oldState);
            double[] probChances = StandardGetProbChances(oldState, neighborStateCount);
            cur.currentState = GetStateFromProbability(probChances);
            int newState = cur.currentState;
            //separateAgents[newState].Add(ActiveAgents[x]);
            CheckTransitions(oldState, newState);
            StateCount.AddOrUpdate(newState, 0, (k, v) => v + 1);

            if ((oldState == newState) == false)
            {
                var found_locs = locations.Where(v => v.Item1 == cur.X).ToList();
                if (found_locs.Count > 0)
                {
                    bool found = found_locs.Any(x => x.Item2 == cur.Y);
                    if (found)
                    {
                        if(template_reset)
                        {
                            controller.reset_now = true;
                        }
                        else
                        {
                            locations.Remove(new Tuple<int, int>(cur.X, cur.Y));
                            controller.Pause();
                        }
                    }
                }
            }
        }

        for (int i = 0; i < numStates; i++)
        {
            GetCIndex(i);
        }
    }

    void GasRoutine()
    {
        Tuple<double, double> center = new Tuple<double, double>((double)gridWidth/2, (double)gridHeight/2);
        // get pressure: num molecules/avagodros = n, T and V from prev data
        double n = ActiveAgents.Count / 6.022e23;
        double t = (double)generics[0][0];
        double v = (double)generics[0][1];
        double mm = (double)generics[0][2];
        double side_length = (double)generics[0][3];
        double cross_sec = (double)generics[0][4];
        double k = (double)generics[0][5];
        double vrms = (double)generics[0][6];
        double avagodro = (double)generics[0][7];
        int resolution = Convert.ToInt32(generics[0][8]);

        double m = mm / 1000;
        double p = (n * 8.3145 * t) / v;
        double d = (mm * p) / (8.3145 * t);
        double num_den = p / (k * t);
        double mean_free = 1 / (num_den * cross_sec);
        double diffuse = (1.0 / 3.0) * (mean_free * vrms);
        // based on resolution, determine number and location of boxes, as well as centroids
        List<Tuple<double, double, double, double>> boxes = new List<Tuple<double, double, double, double>>();
        List<int> box_counts = new List<int>();
        List<Tuple<double, double>> centroid_vectors = new List<Tuple<double, double>>();
        double width_inc = gridWidth / (double)resolution;
        double height_inc = gridHeight / (double)resolution;
        double width_half = width_inc / 2;
        double height_half = height_inc / 2;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                double x_loc = i * width_inc;
                double y_loc = j * height_inc;
                boxes.Add(new Tuple<double, double, double, double>(x_loc, y_loc, (x_loc + width_inc), (y_loc + height_inc)));
                box_counts.Add(0);
                centroid_vectors.Add(new Tuple<double, double>((x_loc + width_half) - center.Item1, (y_loc + height_half) - center.Item2));
            }
        }
        // Based on resolution, determine equilibrium proportion per box, then compare to reality
        int total_molecules = ActiveAgents.Count; // CHANGE LATER FOR CORRECT TYPES
        //double mols_per_box = (double)total_molecules / (boxes.Count);
        double gradient_x = 0;
        double gradient_y = 0;
        List<Tuple<int, int>> agent_locations = new List<Tuple<int, int>>();
        for (int i = 0; i < ActiveAgents.Count; i++)
        {
            // later add check for if agent is of correct type
            agent_locations.Add(new Tuple<int, int>(ActiveAgents[i].X, ActiveAgents[i].Y));
        }
        int count = agent_locations.Count;
        while(agent_locations.Count > 0)
        {
            if (count == 0)
            {
                throw new Exception("You're looping too many times, something's wrong!");
            }
            for (int i = 0; i < boxes.Count; i++)
            {
                if(agent_locations[0].Item1 >= boxes[i].Item1 && agent_locations[0].Item1 < boxes[i].Item3 && agent_locations[0].Item2 >= boxes[i].Item2 && agent_locations[0].Item2 < boxes[i].Item4)
                {
                    box_counts[i] += 1;
                    agent_locations.RemoveAt(0);
                    break;
                }
            }
            count--;
        }
        for (int i = 0; i < box_counts.Count; i++)
        {
            double num_mol = box_counts[i] / avagodro;
            double conc = num_mol / v;
            double partial_pressure = conc * 8.3145 * t;
            gradient_x += centroid_vectors[i].Item1 * (1 - (partial_pressure/p));
            gradient_y += centroid_vectors[i].Item2 * (1 - (partial_pressure/p));
        }
        double direction_vrms = vrms / 4;
        gradient_x += center.Item1;
        gradient_y += center.Item2;
        for (int i = 0; i < ActiveAgents.Count; i++)
        {
            AgentController cur = ActiveAgents[i];
            double a_x = ((gradient_x - cur.X)*side_length)/d;
            double a_y = ((gradient_y - cur.Y)*side_length)/d;
            //find proportional change in each direction
            cur.walkProbs[0] = (cur.walkProbs[0] * diffuse - a_y) / diffuse;
            cur.walkProbs[1] = (cur.walkProbs[1] * diffuse + a_x) / diffuse;
            cur.walkProbs[2] = (cur.walkProbs[2] * diffuse + a_y) / diffuse;
            cur.walkProbs[3] = (cur.walkProbs[3] * diffuse - a_x) / diffuse;
            AgentMove(cur, i);
            cur.AddHistory();
        }
    }

    public List<Tuple<int, int>> AddEnds()
    {
        List<Tuple<int, int>> output = new List<Tuple<int, int>>();
        for (int i = 0; i < ActiveAgents.Count; i++)
        {
            output.Add(new Tuple<int, int>(ActiveAgents[i].X, ActiveAgents[i].Y));
        }
        return output;
    }

    public void CheckTransitions(int oldState, int newState)
    {
        if(oldState != newState)
        {
            Transitions.AddOrUpdate(new Tuple<int, int>(oldState, newState), 0, (k, v) => v + 1);
        }
    }

    public double GetCIndex(int state)
    {
        //CICalcs++;
        edgeCalcs = 0;
        Edges.Clear();
        ConnectedVertices = 0;
        //double connectivityIndex = 0;
        //double maxNeighbors = (gridWidth * gridHeight) * neighborhood.GetNeighborSize();
        int amount = 0;
        if(StateCount.TryGetValue(state, out int val))
        {
            amount = val;
        }
        int neighborhoodFactor = 0;
        int maxEdges = 0;

        switch(neighborTypes[state])
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
            switch(neighborTypes[state])
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
                GetEdges(separateAgents[state][j].X, separateAgents[state][j].Y, state);
            }
        }
        int finalEdge = edgeCalcs / 2;
        double cIndex = 0;
        if(amount > 1)
        {
            cIndex = (finalEdge / (double)maxEdges) * ((double)ConnectedVertices / amount);
        }
        CIndexes[state] = cIndex;
        return cIndex;
    }

    private List<int> GetNeighborCount(int x, int y, int state)
    {
        List<int> neighborCount = new List<int>();
        for (int i = 0; i < numStates; ++i)
        {
            neighborCount.Add(0);
        }
        List<Tuple<int, int>> neighbors = GetNeighbors(x, y, state);
        bool skip = false;
        foreach (var p in neighbors)
        {
            Tuple<int, int> pp = new Tuple<int, int>(p.Item1, p.Item2);
            skip = false;
            if(p.Item1 < 0 || p.Item1 >= gridWidth || p.Item2 < 0 || p.Item2 >= gridHeight)
            {
                int xx;
                int yy;
                switch (states[state].gridType)
                {
                    case GridType.Box:
                        skip = true;
                        break;
                    case GridType.CylinderW: // adjust a point to wrap around the horizontal axis - Cylinder
                        if(p.Item2 < 0 || p.Item2 >= gridHeight)
                        {
                            skip = true;
                        }
                        else
                        {
                            xx = p.Item1;
                            yy = p.Item2;
                            if(xx < 0)
                            {
                                xx += gridWidth;
                            }
                            else if (xx >= gridWidth)
                            {
                                xx -= gridWidth;
                            }
                            pp = new Tuple<int, int>(xx, yy);
                        }
                        break;
                    case GridType.CylinderH:
                        if (p.Item1 < 0 || p.Item1 >= gridWidth)
                        {
                            skip = true;
                        }
                        else
                        {
                            xx = p.Item1;
                            yy = p.Item2;
                            if (yy < 0)
                            {
                                yy += gridHeight;
                            }
                            else if (yy >= gridHeight)
                            {
                                yy -= gridHeight;
                            }
                            pp = new Tuple<int, int>(xx, yy);
                        }
                        break;
                    case GridType.Torus:
                        xx = p.Item1;
                        yy = p.Item2;
                        if (yy < 0)
                        {
                            yy += gridHeight;
                        }
                        else if (yy >= gridHeight)
                        {
                            yy -= gridHeight;
                        }
                        if (xx < 0)
                        {
                            xx += gridWidth;
                        }
                        else if (xx >= gridWidth)
                        {
                            xx -= gridWidth;
                        }
                        pp = new Tuple<int, int>(xx, yy);
                        break;
                }
            }
            if(!skip)
            {
                if (backup[pp.Item1, pp.Item2]>= 0)
                {
                    neighborCount[backup[pp.Item1, pp.Item2]]++;
                }
            }
        }
        return neighborCount;
    }

    List<Tuple<int, int>> GetNeighbors(int x, int y, int state)
    {
        List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();
        if ((int)neighborhoods[state] > 0) // at least von neumann
        {
            neighbors.Add(new Tuple<int, int>(x - 1, y));
            neighbors.Add(new Tuple<int, int>(x, y - 1));
            neighbors.Add(new Tuple<int, int>(x + 1, y));
            neighbors.Add(new Tuple<int, int>(x, y + 1));
        }
        if ((int)neighborhoods[state] > 1) // at least moore
        {
            neighbors.Add(new Tuple<int, int>(x - 1, y - 1));
            neighbors.Add(new Tuple<int, int>(x + 1, y - 1));
            neighbors.Add(new Tuple<int, int>(x + 1, y + 1));
            neighbors.Add(new Tuple<int, int>(x - 1, y + 1));
        }
        if ((int)neighborhoods[state] > 2) // at least hybrid
        {
            neighbors.Add(new Tuple<int, int>(x - 2, y));
            neighbors.Add(new Tuple<int, int>(x, y - 2));
            neighbors.Add(new Tuple<int, int>(x + 2, y));
            neighbors.Add(new Tuple<int, int>(x, y + 2));
        }
        return neighbors;
    }

    // Gets neighbor count for the current grid
    private void GetEdges(int x, int y, int state)
    {
        bool connections = false;
        var neighbors = GetNeighbors(x, y, state);

        // this ^^^ only returns places that exist. SHould it be that way???
        bool skip = false;
        foreach (var p in neighbors)
        {
            Tuple<int, int> pp = new Tuple<int, int>(p.Item1, p.Item2);
            skip = false;
            if (p.Item1 < 0 || p.Item1 >= gridWidth || p.Item2 < 0 || p.Item2 >= gridHeight)
            {
                int xx;
                int yy;
                switch (states[state].gridType)
                {
                    case GridType.Box:
                        skip = true;
                        break;
                    case GridType.CylinderW: // adjust a point to wrap around the horizontal axis - Cylinder
                        if (p.Item2 < 0 || p.Item2 >= gridHeight)
                        {
                            skip = true;
                        }
                        else
                        {
                            xx = p.Item1;
                            yy = p.Item2;
                            if (xx < 0)
                            {
                                xx += gridWidth;
                            }
                            else if (xx >= gridWidth)
                            {
                                xx -= gridWidth;
                            }
                            pp = new Tuple<int, int>(xx, yy);
                        }
                        break;
                    case GridType.CylinderH:
                        if (p.Item1 < 0 || p.Item1 >= gridWidth)
                        {
                            skip = true;
                        }
                        else
                        {
                            xx = p.Item1;
                            yy = p.Item2;
                            if (yy < 0)
                            {
                                yy += gridHeight;
                            }
                            else if (yy >= gridHeight)
                            {
                                yy -= gridHeight;
                            }
                            pp = new Tuple<int, int>(xx, yy);
                        }
                        break;
                    case GridType.Torus:
                        xx = p.Item1;
                        yy = p.Item2;
                        if (yy < 0)
                        {
                            yy += gridHeight;
                        }
                        else if (yy >= gridHeight)
                        {
                            yy -= gridHeight;
                        }
                        if (xx < 0)
                        {
                            xx += gridWidth;
                        }
                        else if (xx >= gridWidth)
                        {
                            xx -= gridWidth;
                        }
                        pp = new Tuple<int, int>(xx, yy);
                        break;
                }
            }
            if (!skip)
            {
                if (backup[pp.Item1, pp.Item2] >= 0)
                {
                    if (connections == false)
                    {
                        ConnectedVertices += 1;
                        connections = true;
                    }
                    if (grid[pp.Item1, pp.Item2].Agent.currentState == state)
                    {
                        edgeCalcs++;
                        // this needs to be outside of "contains agent"
                    }
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
                        if(grid[newRow,newCol].Agent.currentState == nP)
                        {
                            tempProb = states[currentState].advProbs[p, nP][temp2Rows, temp2Cols];
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

        for (int toState = 0; toState < numStates; toState++)
        {
            double prob = 0;
            //skip if we are on the state of the current cell
            //we'll figure it out after we find out all the other probs
            if (toState == currentState)
                continue;

            //the neighbor probabilities of different states are considered ADDITIVE
            //They will combine to form one probability to that other state

            for (int neighborState = 0; neighborState < numStates; neighborState++)
            {
                double tempProb = 0;
                //if (neighborState == currentState)
                //    continue;
                tempProb = states[currentState].prob[toState, neighborState, neighborStateCount[neighborState]];
                
                prob += tempProb;
            }
            probChances[toState] = prob;
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

    private void AgentMove(AgentController currentAgent, int agentLocation)
    {
        //NEED to check things. Need to make check for probability of Random walk itself (encapsulate in IF). Then, check if the new place is already filled.
        //Debug.Log("Old: " + currentAgent.xLocation + ", " + currentAgent.yLocation);
        int newX = currentAgent.X;
        int newY = currentAgent.Y;
        int oldX = currentAgent.X;
        int oldY = currentAgent.Y;
        var bytes = new Byte[8];
        rng.GetBytes(bytes);
        var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
        Double randomWalk = ul / (Double)(1UL << 53);
        double upProb = currentAgent.walkProbs[0];
        double rightProb = currentAgent.walkProbs[1];
        double downProb = currentAgent.walkProbs[2];
        double leftProb = currentAgent.walkProbs[3];

        double newRight = (upProb + rightProb);
        double newDown = newRight + downProb;
        double newLeft = newDown + leftProb;


        if (randomWalk < upProb)
        {
            newY = currentAgent.Y - 1;
        }
        else if (randomWalk < (rightProb + upProb) && randomWalk > upProb)
        {
            newX = currentAgent.X + 1;
        }
        else if (randomWalk < (downProb + rightProb + upProb) && randomWalk > (rightProb + upProb))
        {
            newY = currentAgent.Y + 1;
        }
        else
        {
            newX = currentAgent.X - 1;
        }
        Tuple<bool, int, int> tempTuple = GetGridTypeLocation(newX, newY, currentAgent.currentState);
        if(tempTuple.Item1)
        {
            if (tempTuple.Item2 != oldX || tempTuple.Item3 != oldY)
            {
                if (grid[tempTuple.Item2, tempTuple.Item3].ContainsAgent == false && System.Object.ReferenceEquals(grid[tempTuple.Item2, tempTuple.Item3].Agent, null) == true)
                {
                    grid[tempTuple.Item2, tempTuple.Item3].AddAgent(currentAgent);
                    ActiveAgents[agentLocation] = grid[tempTuple.Item2, tempTuple.Item3].Agent;
                    grid[oldX, oldY].RemoveAgent();
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

    //private bool IsReal(int newX, int newY)
    //{
    //    switch (gridType)
    //    {
    //        case GridType.Box:
    //            if (newX > (gridWidth - 1) || newY > (gridHeight - 1) || newX < 0 || newY < 0)
    //                return false;
    //            else
    //                return true;
    //        case GridType.CylinderW:
    //            if (newY > (gridHeight - 1) || newY < 0)
    //                return false;
    //            else
    //            {
    //                return true;
    //            }
    //        case GridType.CylinderH:
    //            if (newX > (gridHeight - 1) || newX < 0)
    //                return false;
    //            else
    //            {
    //                return true;
    //            }
    //        case GridType.Torus:
    //            return true;
    //        default:
    //            return false;
    //    }
    //}

    public bool MoveAgent(AgentController agent, Tuple<int, int> position)
    {
        int oldX = agent.X;
        int oldY = agent.Y;
        if (grid[position.Item1, position.Item2].ContainsAgent == false && System.Object.ReferenceEquals(grid[position.Item1, position.Item2].Agent, null) == true)
        {
            grid[position.Item1, position.Item2].AddAgent(agent);
            ActiveAgents[agentLocation] = grid[position.Item1, position.Item2].Agent;
            grid[oldX, oldY].RemoveAgent();
            agent.AddHistory();
            return true;
        }
        else
        {
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
            List<BlankGrid> neighborList = StaticMethods.GetMoveNeighbors(states[currentAgent.currentState], currentAgent.Cell, new Tuple<int, int>(gridWidth, gridHeight));

            for (int i = 0; i < neighborList.Count; i++)
            {
                if(neighborList[i].ContainsAgent)
                {
                    var bytes = new Byte[8];
                    rng.GetBytes(bytes);
                    var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                    Double rand = ul / (Double)(1UL << 53);
                    double stayProb = states[currentAgent.currentState].stickingProbs[neighborList[i].Agent.currentState];
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

    Tuple<bool, int, int> GetGridTypeLocation(int x, int y, int localState)
    {
        switch (states[localState].gridType)
        {
            case GridType.Box:
                if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
                {
                    return new Tuple<bool, int, int>(false, x, y);
                }
                else
                {
                    return new Tuple<bool, int, int>(true, x, y);
                }
            //break;
            case GridType.CylinderW:
                if (y < 0 || y >= gridHeight)
                {
                    return new Tuple<bool, int, int>(false, x, y);
                }
                else
                {
                    if (x < 0)
                    {
                        x += gridWidth;
                    }
                    else if (x >= gridWidth)
                    {
                        x -= gridWidth;
                    }
                    return new Tuple<bool, int, int>(true, x, y);
                }
            //break;
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
                    return new Tuple<bool, int, int>(true, x, y);
                }
            //break;
            case GridType.Torus:
                {
                    if (x < 0)
                    {
                        x += gridWidth;
                    }
                    else if (x >= gridWidth)
                    {
                        x -= gridWidth;
                    }
                    if (y < 0)
                    {
                        y += gridHeight;
                    }
                    else if (y >= gridHeight)
                    {
                        y -= gridHeight;
                    }
                    return new Tuple<bool, int, int>(true, x, y);
                }
                //break;
        }
        return new Tuple<bool, int, int>(false, x, y);
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

public class UnorderedTupleComparer<T> : IEqualityComparer<Tuple<T, T>>
{
    private IEqualityComparer<T> comparer;
    public UnorderedTupleComparer(IEqualityComparer<T> comparer = null)
    {
        this.comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public bool Equals(Tuple<T, T> x, Tuple<T, T> y)
    {
        return comparer.Equals(x.Item1, y.Item1) && comparer.Equals(x.Item2, y.Item2) ||
            comparer.Equals(x.Item1, y.Item2) && comparer.Equals(x.Item2, y.Item1);
    }

    public int GetHashCode(Tuple<T, T> obj)
    {
        return comparer.GetHashCode(obj.Item1) ^ comparer.GetHashCode(obj.Item2);
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