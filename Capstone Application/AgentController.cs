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
        public CA Parent { get; private set; }
        public BlankGrid Cell { get; private set; }
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //ControllerScript controllerScript = Form1.controllerScript;
        //CA caScript = Form1.controllerScript.myCA;
        public int currentState;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int iterations_alive = 0;
        public double[] walkProbs;
        private bool historyChange = false;
        List<Tuple<int, int, int>> history = new List<Tuple<int, int, int>>();
        //List<Tuple<int, int>> neighborhood = new List<Tuple<int, int>>();
        //List<AgentContainerSetting> containerSettings;
        List<ContainerController> containers;

        public List<Tuple<int, int, int>> History { get => history; set => history = value; }
        public bool HistoryChange { get => historyChange; set => historyChange = value; }
        internal List<ContainerController> Containers { get => containers; set => containers = value; }
        AgentController targetAgent;
        public bool Busy { get; private set; } // use when the agent has a long-running task. In this case travelling on a path.
        Action ExecutionAction;

        public AgentController(int agentX, int agentY, int state, CA parent, BlankGrid cell)
        {
            this.Parent = parent;
            this.Cell = cell;
            this.currentState = state;
            this.walkProbs = parent.states[this.currentState].walkProbs;
            History.Add(Tuple.Create(agentX, agentY, state));
            Containers = new List<ContainerController>();
            foreach(var con in parent.GetStateInfo(state).containerSettings)
            {
                Containers.Add(new ContainerController(con));
            }
        }

        public void Execute()
        {
            ExecutionAction();
        }

        public void AddContainer(List<AgentContainerSetting> input)
        {
            //if (input != null && input.Count > 0)
            //{
            //    Containers = new List<ContainerController>();
            //    for (int i = 0; i < input.Count; i++)
            //    {
            //        Containers.Add(new ContainerController(input[i]));
            //    }
            //}
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
                    switch (Containers[i].IterativeBehaviors[j].Item1)
                    {
                        case Operation.Add:
                            values[j] = values[j] + Containers[i].IterativeBehaviors[j].Item2;
                            break;
                        case Operation.Div:
                            values[j] = values[j] / Containers[i].IterativeBehaviors[j].Item2;
                            break;
                        case Operation.Mul:
                            values[j] = values[j] * Containers[i].IterativeBehaviors[j].Item2;
                            break;
                        case Operation.None:
                            break;
                        case Operation.Pow:
                            values[j] = Math.Pow(values[j], Containers[i].IterativeBehaviors[j].Item2);
                            break;
                        case Operation.Sub:
                            values[j] = values[j] - Containers[i].IterativeBehaviors[j].Item2;
                            break;
                    }
                }
                // with all of these possible values, we need to pick between them.
                
                var bytes = new Byte[8];
                rng.GetBytes(bytes);
                var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                Double randomDouble = ul / (Double)(1UL << 53);
                int answer = (int)Math.Floor(randomDouble * values.Count);
                value = values[answer];
                for (int j = 0; j < Containers[i].Thresholds.Count; j++)
                {
                    if (CheckThreshold(Containers[i].Thresholds[j].Threshold, value))
                    {
                        value = HandleThreshold(value, i, j);
                    }
                }
                Containers[i].Value = value;
            }
        }

        bool CheckThreshold(Tuple<ThresholdType, double> threshold, double value)
        {
            var thresholdValue = threshold.Item2;
            switch(threshold.Item1)
            {
                case ThresholdType.LessThan:
                    if(value < thresholdValue)
                    {
                        return true;
                    }
                    break;
                case ThresholdType.LessThanOrEqualTo:
                    if (value <= thresholdValue)
                    {
                        return true;
                    }
                    break;
                case ThresholdType.EqualTo:
                    if (value == thresholdValue)
                    {
                        return true;
                    }
                    break;
                case ThresholdType.GreaterThanOrEqualTo:
                    if (value >= thresholdValue)
                    {
                        return true;
                    }
                    break;
                case ThresholdType.GreaterThan:
                    if (value > thresholdValue)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void AddContainerValue(double value, int i)
        {
            Containers[i].Value += value;
        }

        dynamic HandleThreshold(double input, int i, int j)
        {
            dynamic output = input;
            switch(Containers[i].Thresholds[j].Behavior)
            {
                case AgentContainerThresholdBehavior.Overflow:
                    var a = StaticMethods.GetMoveNeighbors(Parent.GetStateInfo(currentState), this.Cell, new Tuple<int, int>(Parent.gridWidth, Parent.gridHeight));
                    a = a.Where(x => x.ContainsAgent).ToList();
                    for (int k = 0; k < a.Count; k++)
                    {

                        a[k].Agent.AddContainerValue(input / a.Count, i);
                        output -= input / a.Count;
                    }
                    break;
                case AgentContainerThresholdBehavior.Die:
                    Cell.RemoveAgent();
                    Parent.RemoveAgent(X, Y);
                    break;
                case AgentContainerThresholdBehavior.Find: // this automatically finds the closest
                    var agents = Parent.ActiveAgents.Where(x => x.Containers.Any(y => y.Name.Equals(Containers[i].Name))).Where(y => y != this).ToList();
                    if(agents.Count > 0)
                    {
                        int index = 0;
                        var distance = StaticMethods.GetDistance(this.Cell, agents[0].Cell);
                        for (int k = 0; k < agents.Count; k++)
                        {
                            var curDistance = StaticMethods.GetDistance(this.Cell, agents[k].Cell);
                            if (curDistance < distance)
                            {
                                distance = curDistance;
                                index = k;
                            }
                        }
                        if (distance == 0)
                        {
                            // arrived
                        }
                        else
                        {
                            Busy = true;
                            var path = StaticMethods.FindPath(this, agents[index].Cell, false);
                            ExecutionAction = (() => // look to see if destination agent has moved?
                            {
                                if (path.Count == 0)
                                {
                                    Busy = false;
                                }
                                else
                                {
                                    var success = Parent.MoveAgent(this, new Tuple<int, int>(path[0].X, path[0].Y));
                                    path.RemoveAt(0);
                                    if (path.Count == 0)
                                    {
                                        Busy = false;
                                    }
                                }
                            });
                        }
                    }
                    break;
            }
            return output;
        }

        public void Update(BlankGrid cell)
        {
            this.Cell = cell;
            this.X = Cell.X;
            this.Y = Cell.Y;
        }

        public void AddHistory()
        {
            History.Add(Tuple.Create(X, Y, currentState));
            HistoryChange = true;
        }

        public void AddDLAHistory()
        {
            iterations_alive += 1;
            History.Add(Tuple.Create(X, Y, currentState));
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
