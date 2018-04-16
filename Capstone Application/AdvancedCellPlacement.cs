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
        int count;
        Form1 form;
        int state;
        public AdvancedCellPlacement(Form1 mainForm, int cellCount, int thisState)
        {
            InitializeComponent();
            form = mainForm;
            state = thisState;
            count = cellCount;
            PopulateGrid();
        }

        void PopulateGrid()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < count; i++)
            {
                dataGridView1.Rows.Add();
            }
            // check settings script
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < count; i++)
            {
                //how to use tryparse on object?
                // add to other grid types
                if(int.TryParse(dataGridView1[1, i].Value.ToString(), out int result) && int.TryParse(dataGridView1[2, i].Value.ToString(), out int otherResult))
                {
                    Tuple<int, int> tempTuple = new Tuple<int, int>(int.Parse(dataGridView1[1, i].Value.ToString()), int.Parse(dataGridView1[2, i].Value.ToString()));
                    Tuple<int, Tuple<int, int>> otherTuple = new Tuple<int, Tuple<int, int>>(state, tempTuple);
                    form.settingsScript.StartingLocations.Add(otherTuple);
                }
            }
        }
    }
}
