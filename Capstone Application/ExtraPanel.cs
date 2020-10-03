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
    public partial class ExtraPanel : UserControl
    {
        //int y = 0;
        //int x = 3;
        List<ExtraFeature> extras;

        public ExtraPanel()
        {
            extras = new List<ExtraFeature>();
            InitializeComponent();
            //y = addLabel.Location.Y + addLabel.Height + 5;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            ExtraFeature extra = new ExtraFeature(extras.Count);
            //extra.Location = new System.Drawing.Point(x, y);
            //y += extra.Height + 5;
            //this.Controls.Add(extra);
            extra.removeButton.Click += ((object button_sender, EventArgs e_a) =>
            {
                extras.RemoveAt(extra.location);
                tableLayoutPanel1.Controls.RemoveAt(extra.index);
                tableLayoutPanel1.RowStyles.RemoveAt(extra.location + 1);
                //Reposition();
            });
            tableLayoutPanel1.Controls.Add(extra);
            extra.index = tableLayoutPanel1.Controls.GetChildIndex(extra);
            tableLayoutPanel1.SetRow(extra, extras.Count + 1);
            tableLayoutPanel1.SetColumn(extra, 0);
            tableLayoutPanel1.SetColumnSpan(extra, 2);
            extras.Add(extra);
        }

        //void Reposition()
        //{
        //    y = addLabel.Location.Y + addLabel.Height + 5;
        //    for (int i = 0; i < extras.Count; i++)
        //    {
        //        extras[i].location = i;
        //        extras[i].index = this.Controls.GetChildIndex(extras[i]); 
        //        extras[i].Location = new System.Drawing.Point(x, y);
        //        y += extras[i].Height + 5;
        //    }
        //}

        public List<AgentContainerSetting> Retrieve()
        {
            List<AgentContainerSetting> output = new List<AgentContainerSetting>();
            for (int i = 0; i < extras.Count; i++)
            {
                output.Add(extras[i].Retrieve());
            }
            return output;
        }
    }
}
