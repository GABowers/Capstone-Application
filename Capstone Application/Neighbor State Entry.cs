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
    public partial class Neighbor_State_Entry : UserControl
    {
        public Neighbor_State_Entry()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count < 1)
            {
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn newColumn2 = new DataGridViewTextBoxColumn();
                newColumn.MinimumWidth = 20;
                newColumn2.MinimumWidth = 20;
                dataGridView1.Columns.Add(newColumn);
                dataGridView1.Rows.Add(new DataGridViewRow());
                dataGridView2.Columns.Add(newColumn2);
                dataGridView2.Rows.Add(new DataGridViewRow());
            }
            else if (dataGridView1.Columns.Count >= 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn newColumn2 = new DataGridViewTextBoxColumn();
                    newColumn.MinimumWidth = 20;
                    newColumn2.MinimumWidth = 20;
                    dataGridView1.Columns.Add(newColumn);
                    dataGridView1.Rows.Add(new DataGridViewRow());
                    dataGridView2.Columns.Add(newColumn2);
                    dataGridView2.Rows.Add(new DataGridViewRow());
                }
            }
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            //if statement to check that amount is greater than 1
            if (dataGridView1.Columns.Count > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    dataGridView1.Columns.RemoveAt(0);
                    dataGridView1.Rows.RemoveAt(0);
                    dataGridView2.Columns.RemoveAt(0);
                    dataGridView2.Rows.RemoveAt(0);
                }
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView2.ColumnHeadersVisible = false;
                dataGridView2.RowHeadersVisible = false;
            }
        }
    }
}
