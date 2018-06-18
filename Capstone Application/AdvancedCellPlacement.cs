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
    public partial class AdvancedCellPlacement : Form
    {
        Form1 form;
        Form2 modelForm;
        ControllerScript controllerScript = Form1.controllerScript;
        int state;
        public AdvancedCellPlacement(Form1 mainForm, Form2 container, int thisState)
        {
            InitializeComponent();
            form = mainForm;
            modelForm = container;
            state = thisState;
            //PopulateGrid();
        }

        //void PopulateGrid()
        //{
        //    dataGridView1.Rows.Clear();
        //    // check settings script
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Tuple<int, int>> tempList = new List<Tuple<int, int>>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //how to use tryparse on object?
                // add to other grid types
                if(int.TryParse(dataGridView1[1, i].Value.ToString(), out int result) && int.TryParse(dataGridView1[2, i].Value.ToString(), out int otherResult))
                {
                    Tuple<int, int> tempTuple = new Tuple<int, int>(int.Parse(dataGridView1[1, i].Value.ToString()), int.Parse(dataGridView1[2, i].Value.ToString()));
                    tempList.Add(tempTuple);
                }

            }
            controllerScript.SetStartingLocations(tempList, state);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(new DataGridViewRow());
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1[0, i].Value = (i+1).ToString();
            }
        }
    }
}
