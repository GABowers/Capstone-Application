using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class GroupingForm : Form
    {
        ControllerScript controllerScript = Form1.controllerScript;
        Form1 mainform;
        public GroupingForm(Form1 form)
        {
            InitializeComponent();
            mainform = form;
        }

        int NumRuns { get; set; } = 0;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(textBox1.Text, out int result))
            {
                NumRuns = result;
            }
        }

        private void runGetAllGroups_Click(object sender, EventArgs e)
        {
            controllerScript.GetAllGroupings(NumRuns, mainform, progressBar1);
            this.Close();
        }
    }
}
