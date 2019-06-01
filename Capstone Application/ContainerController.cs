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
        double threshold;
        AgentContainerThresholdBehavior thresholdBehavior;
        List<List<Tuple<Operation, double>>> iterativeBehaviors;
        bool stochasticThreshold;

        public ContainerController(AgentContainerSetting input)
        {
            Name = input.Name;
            Threshold = input.Threshold;
            ThresholdBehavior = input.ThresholdBehavior;
            IterativeBehaviors = new List<List<Tuple<Operation, double>>>();
            ParseOperation(input.IterationBehavior);
            StochasticThreshold = input.ThresholdStochastic;
        }

        public bool StochasticThreshold { get => stochasticThreshold; set => stochasticThreshold = value; }
        public List<List<Tuple<Operation, double>>> IterativeBehaviors { get => iterativeBehaviors; set => iterativeBehaviors = value; }
        public AgentContainerThresholdBehavior ThresholdBehavior { get => thresholdBehavior; set => thresholdBehavior = value; }
        public double Threshold { get => threshold; set => threshold = value; }
        public double Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }

        void ParseOperation(string input)
        {
            // wrong. needs to be LIst of list of list, to allow for multiple container objects, multiple options for each, and multiple operations for each option
            string all = new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            string[] pieces = all.Split(',');
            for (int i = 0; i < pieces.Length; i++)
            {
                List<Tuple<Operation, double>> cur = new List<Tuple<Operation, double>>();
                if (pieces[i] == "None")
                {
                    cur.Add(new Tuple<Operation, double>(Operation.None, 0));
                }
                else
                {
                    string[] sub_pieces = pieces[i].Split('.');
                    for (int j = 0; j < sub_pieces.Length; j++)
                    {
                        Operation o = Operation.None;
                        switch (sub_pieces[j][0])
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
                        cur.Add(new Tuple<Operation, double>(o, double.Parse(sub_pieces[j].Remove(0, 1))));
                    }
                    
                }
                IterativeBehaviors.Add(cur);
            }
        }

    }
}
