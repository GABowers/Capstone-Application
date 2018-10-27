﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class AgentController
    {
        ControllerScript controllerScript = Form1.controllerScript;
        CA caScript = Form1.controllerScript.myCA;
        public int currentState;
        public int xLocation;
        public int yLocation;
        private bool historyChange = false;
        List<Tuple<int, int, int>> history = new List<Tuple<int, int, int>>();
        List<Tuple<int, int>> neighborhood = new List<Tuple<int, int>>();

        public List<Tuple<int, int, int>> History { get => history; set => history = value; }
        public bool HistoryChange { get => historyChange; set => historyChange = value; }

        public AgentController(int agentX, int agentY, int state)
        {
            History.Add(Tuple.Create(agentX, agentY, state));
        }

        public void AddHistory()
        {
            History.Add(Tuple.Create(xLocation, yLocation, currentState));
            HistoryChange = true;
        }

        public void TransitionCheck()
        {

        }

        public void MovementCheck()
        {

        }

        public Tuple<bool, int> NeighborCheck(BlankGrid[,] grid, int x, int y)
        {
            if(grid[x, y].ContainsAgent == false)
            {
                return new Tuple<bool, int>(false, -1);
            }
            else
            {
                return new Tuple<bool, int>(true, grid[x, y].agent.currentState);
            }
        }

        //This could be optimized further. For immobile CA, we could set the neighborhood at the start of a CA run, so each agent knows
        //its neighbors in advance, rather than calculation each iteration. However, this method will work for both immobile and mobile
        //CA. May change things if too slow.
        public bool NeighborCheck(BlankGrid[,] grid, CellState states)
        {
            int highX = xLocation + 1;
            int lowX = xLocation - 1;
            int highY = yLocation + 1;
            int lowY = yLocation - 1;
            List<Tuple<int, int>> grids = new List<Tuple<int, int>>();
            if (states.mobileNeighborhood == 0)
            {
                if (highX > 0 && highX < caScript.gridWidth && grid[highX, yLocation] != null)
                {
                    grids.Add(Tuple.Create(highX, yLocation));
                }
                if (lowX > 0 && lowX < caScript.gridWidth && grid[lowX, yLocation] != null)
                {
                    grids.Add(Tuple.Create(lowX, yLocation));
                }
                if (highY > 0 && highY < caScript.gridHeight && grid[xLocation, highY] != null)
                {
                    grids.Add(Tuple.Create(xLocation, highY));
                }
                if (lowY > 0 && lowY < caScript.gridHeight && grid[xLocation, lowY] != null)
                {
                    grids.Add(Tuple.Create(xLocation, lowY));
                }

                switch(grids.Count)
                {
                    case 1:
                        if (grid[grids[0].Item1, grids[0].Item2].ContainsAgent)
                            return true;
                        break;
                    case 2:
                        if (grid[grids[0].Item1, grids[0].Item2].ContainsAgent || grid[grids[1].Item1, grids[1].Item2].ContainsAgent)
                            return true;
                        break;
                    case 3:
                        if (grid[grids[0].Item1, grids[0].Item2].ContainsAgent || grid[grids[1].Item1, grids[1].Item2].ContainsAgent || grid[grids[2].Item1, grids[2].Item2].ContainsAgent)
                            return true;
                        break;
                    case 4:
                        if (grid[grids[0].Item1, grids[0].Item2].ContainsAgent || grid[grids[1].Item1, grids[1].Item2].ContainsAgent || grid[grids[2].Item1, grids[2].Item2].ContainsAgent || grid[grids[3].Item1, grids[3].Item2].ContainsAgent)
                            return true;
                        break;
                    default:
                        return false;
                }
                return false;
            }
            else if (states.mobileNeighborhood == 1)
            {
                if (grid[highX, yLocation].ContainsAgent || grid[lowX, yLocation].ContainsAgent || grid[xLocation, highY].ContainsAgent || grid[xLocation, lowY].ContainsAgent || grid[highX, highY].ContainsAgent || grid[lowX, lowY].ContainsAgent || grid[highX, lowY].ContainsAgent || grid[lowX, highY].ContainsAgent)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
