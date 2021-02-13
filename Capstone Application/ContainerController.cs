using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    class ContainerController
    {
        string name;
        double value;
        List<Tuple<Operation, double>> iterativeBehaviors;
        bool stochasticThreshold;

        public ContainerController(AgentContainerSetting input)
        {
            Name = input.Name;
            Value = input.InitialValue;
            Thresholds = input.Thresholds;
            IterativeBehaviors = new List<Tuple<Operation, double>>();
            ParseOperation(input.IterationBehavior);
            StochasticThreshold = input.ThresholdStochastic;
        }

        public bool StochasticThreshold { get => stochasticThreshold; set => stochasticThreshold = value; }
        public List<Tuple<Operation, double>> IterativeBehaviors { get => iterativeBehaviors; set => iterativeBehaviors = value; }
        public List<AgentContainerThreshold> Thresholds { get; set; }
        public double Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }

        void ParseOperation(string input)
        {
            if(input.Length > 0)
            {
                string all = new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                string[] pieces = all.Split(',');
                for (int i = 0; i < pieces.Length; i++)
                {
                    Operation o = Operation.None;
                    List<Tuple<Operation, double>> cur = new List<Tuple<Operation, double>>();
                    if (pieces[i].Length > 0)
                    {
                        switch (pieces[i][0])
                        {
                            case '+':
                                o = Operation.Add;
                                break;
                            case '-':
                                o = Operation.Sub;
                                break;
                            case '*':
                                o = Operation.Mul;
                                break;
                            case '/':
                                o = Operation.Div;
                                break;
                            case '^':
                                o = Operation.Pow;
                                break;
                        }
                        if (double.TryParse(pieces[i].Remove(0, 1), out double result))
                        {
                            IterativeBehaviors.Add(new Tuple<Operation, double>(o, result));
                        }
                    }
                }
            }
        }
    }
}
