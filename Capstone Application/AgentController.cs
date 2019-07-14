using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class AgentController
    {
        CA parent;
        BlankGrid[,] grid;
        RNGCryptoServiceProvider rng;
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
        List<AgentContainerSetting> containerSettings;
        List<ContainerController> containers;

        public List<Tuple<int, int, int>> History { get => history; set => history = value; }
        public bool HistoryChange { get => historyChange; set => historyChange = value; }
        internal List<ContainerController> Containers { get => containers; set => containers = value; }

        public AgentController(int agentX, int agentY, int state, CA parent, BlankGrid[,] grid)
        {
            this.parent = parent;
            this.grid = grid;
            History.Add(Tuple.Create(agentX, agentY, state));
            rng = new RNGCryptoServiceProvider();
        }

        public void AddContainer(List<AgentContainerSetting> input)
        {
            if (input != null && input.Count > 0)
            {
                Containers = new List<ContainerController>();
                for (int i = 0; i < input.Count; i++)
                {
                    Containers.Add(new ContainerController(input[i]));
                }
            }
        }

        public void HandleContainer()
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                double value = Containers[i].Value;
                // get new values
                List<double> values = new List<double>();
                for (int j = 0; j < Containers[i].IterativeBehaviors.Count; j++)
                {
                    values.Add(value);
                    for (int k = 0; k < Containers[i].IterativeBehaviors[j].Count; k++)
                    {
                        switch (Containers[i].IterativeBehaviors[j][k].Item1)
                        {
                            case Operation.Add:
                                values[j] = values[j] + Containers[i].IterativeBehaviors[j][k].Item2;
                                break;
                            case Operation.Div:
                                values[j] = values[j] / Containers[i].IterativeBehaviors[j][k].Item2;
                                break;
                            case Operation.Mul:
                                values[j] = values[j] * Containers[i].IterativeBehaviors[j][k].Item2;
                                break;
                            case Operation.None:
                                break;
                            case Operation.Pow:
                                values[j] = Math.Pow(values[j], Containers[i].IterativeBehaviors[j][k].Item2);
                                break;
                            case Operation.Sub:
                                values[j] = values[j] - Containers[i].IterativeBehaviors[j][k].Item2;
                                break;
                        }
                    }
                }
                // with all of these possible values, we need to pick between them.
                
                var bytes = new Byte[8];
                rng.GetBytes(bytes);
                var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                Double randomDouble = ul / (Double)(1UL << 53);
                int answer = (int)Math.Floor(randomDouble * values.Count);
                //Console.WriteLine("double," + randomDouble+",values,"+string.Join(",",values)+",answer,"+answer);
                value = values[answer];
                if (value > Containers[i].Threshold)
                {
                    value = HandleThreshold(value, i);
                }
                Containers[i].Value = value;
            }
        }

        public void AddContainerValue(double value, int i)
        {
            Containers[i].Value += value;
        }

        dynamic HandleThreshold(double input, int i)
        {
            dynamic output = input;
            switch(Containers[i].ThresholdBehavior)
            {
                case AgentContainerThresholdBehavior.Overflow:
                    List<Tuple<int, int>> a = parent.GetNeighborhood(this);
                    for (int j = 0; j < a.Count; j++)
                    {
                        grid[a[j].Item1, a[j].Item2].agent.AddContainerValue(input / a.Count, i);
                        output -= input / a.Count;
                    }
                    break;
            }
            return output;
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

    public enum Operation
    {
        Add,
        Sub,
        Mul,
        Div,
        Pow,
        Equate,
        None
    }
}
