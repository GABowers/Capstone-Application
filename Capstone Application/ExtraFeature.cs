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
            var items = Enum.GetNames(typeof(AgentContainerThresholdBehavior));
            thresholdBehaviorInput.Items.AddRange(items);
        }

        public AgentContainerSetting Retrieve()
        {
            try
            {
                AgentContainerSetting a = new AgentContainerSetting();
                a.Name = nameInput.Text;
                var thresholdType = (ThresholdType)thresholdTypeInput.SelectedIndex;
                var thresholdValue = double.TryParse(thresholdInput.Text, out double result) ? result : 0.0;
                a.IterationBehavior = iterationBehaviorInput.Text;
                var behavior = (AgentContainerThresholdBehavior)thresholdBehaviorInput.SelectedIndex;
                var threshold = new AgentContainerThreshold(new Tuple<ThresholdType, double>(thresholdType, thresholdValue), behavior);
                a.Thresholds.Add(threshold);
                a.ThresholdStochastic = stochasticThresholdInput.Checked;
                a.Type = (AgentContainerType)typeInput.SelectedIndex;
                a.InitialValue = double.TryParse(initialInput.Text, out double result2) ? result2 : 0.0; ;
                a.Shade = shadeInput.Checked;
                return a;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }
    }

    public enum AgentContainerThresholdBehavior
    {
        Overflow = 0, // adds entity to surroundings IF they're valid - what does valid mean?
        Die = 1, // remove agent
        Find = 2, // seek out entities which can increase the threshold
    }
    public enum ThresholdType
    {
        LessThan=0,
        LessThanOrEqualTo=1,
        EqualTo=2,
        GreaterThanOrEqualTo=3,
        GreaterThan=4
    }
    public enum AgentContainerType
    {
        Double=1
    }
}
