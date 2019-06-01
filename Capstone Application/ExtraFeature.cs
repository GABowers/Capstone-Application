using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class ExtraFeature : UserControl
    {
        public int location = 0;
        public int index = 0;
        public ExtraFeature(int location)
        {
            this.location = location;
            InitializeComponent();
        }

        public AgentContainerSetting Retrieve()
        {
            AgentContainerSetting a = new AgentContainerSetting();
            a.Name = nameInput.Text;
            a.Threshold = double.TryParse(thresholdInput.Text, out double result)? result : 0.0;
            a.IterationBehavior = iterationBehaviorInput.Text;
            a.ThresholdBehavior = (AgentContainerThresholdBehavior)thresholdBehaviorInput.SelectedIndex;
            a.ThresholdStochastic = stochasticThresholdInput.Checked;
            a.Type = (AgentContainerType)typeInput.SelectedIndex;
            a.InitialValue = initialInput.Text;
            a.Shade = shadeInput.Checked;
            return a;
        }
    }

    public enum AgentContainerThresholdBehavior
    {
        Overflow = 0,
    }
    public enum AgentContainerType
    {
        Double=1
    }
}
