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
        public string InitialValue { get; set; }
        public string IterationBehavior { get; set; }
        public double Threshold { get; set; }
        public AgentContainerThresholdBehavior ThresholdBehavior { get; set; }
        public bool ThresholdStochastic { get; set; }
        public bool Shade { get; set; }
    }
}
