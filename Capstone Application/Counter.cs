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
    public partial class Counter : Form
    {
        Form1 form;
        ControllerScript controllerScript = Form1.controllerScript;
        int states;
        public Counter(Form1 outsideForm)
        {
            form = outsideForm;
            InitializeComponent();
            // Check for CA
            AddInfo();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void AddInfo()
        {
            if(controllerScript.createdCA == true)
            {
                states = controllerScript.amountOfCellTypes;
                for (int i = 0; i < states; i++)
                {
                    dataGridView1.Rows.Add(new DataGridViewRow());
                }
                for (int i = 0; i < states; i++)
                {
                    dataGridView1[0, i].Value = (i + 1).ToString();
                    dataGridView1[1, i].Value = controllerScript.myCA.stateCount[i];
                }
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
        }

        public void UpdateCounts()
        {
            for (int i = 0; i < states; i++)
            {
                dataGridView1[1, i].Value = controllerScript.myCA.stateCount[i];
            }
        }

        private void Counter_FormClosed(object sender, FormClosedEventArgs e)
        {
            // sets "counteropened" in main form to false
        }
    }
}
