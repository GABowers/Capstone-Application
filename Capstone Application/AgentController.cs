using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class AgentController
    {
        //ControllerScript controllerScript = Form1.controllerScript;
        //CA caScript = Form1.controllerScript.myCA;
        public int currentState;
        public int xLocation;
        public int yLocation;
        public int iterations_alive = 0;
        public double[] walkProbs;
        private bool historyChange = false;
        List<Tuple<int, int, int>> history = new List<Tuple<int, int, int>>();
        //List<Tuple<int, int>> neighborhood = new List<Tuple<int, int>>();

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

        public void AddDLAHistory()
        {
            iterations_alive += 1;
            History.Add(Tuple.Create(xLocation, yLocation, currentState));
            if (History.Count > 2)
            {
                History.RemoveAt(0);
            }
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
    }
}
