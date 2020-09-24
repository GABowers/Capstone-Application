using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class AgentContainerSetting
    {
        public string Name { get; set; }
        public AgentContainerType Type { get; set; }
        public double InitialValue { get; set; }
        public string IterationBehavior { get; set; }
        public List<AgentContainerThreshold> Thresholds { get; set; }
        public bool ThresholdStochastic { get; set; }
        public bool Shade { get; set; }
        public AgentContainerSetting()
        {
            Thresholds = new List<AgentContainerThreshold>();
        }
    }

    public class AgentContainerThreshold
    {
        public Tuple<ThresholdType, double> Threshold { get; private set; }
        public AgentContainerThresholdBehavior Behavior { get; private set; }
        public AgentContainerThreshold(Tuple<ThresholdType, double> threshold, AgentContainerThresholdBehavior behavior)
        {
            Threshold = threshold;
            Behavior = behavior;
        }
    }
}
