﻿using System;
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
            this.StartPosition = FormStartPosition.Manual;
            form = outsideForm;
            InitializeComponent();
            // Check for CA
            AddInfo();
            SetSize();
            this.SetDesktopLocation(outsideForm.Location.X + outsideForm.Width, outsideForm.Location.Y);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void AddInfo()
        {
            //string misc = "Misc.";
            //string ci = "CI: ";
            if(controllerScript.CreatedCA == true)
            {
                states = controllerScript.amountOfCellTypes;
                for (int i = 0; i < states; i++)
                {
                    dataGridView1.Rows.Add(new DataGridViewRow());
                }
                for (int i = 0; i < states; i++)
                {
                    dataGridView1[0, i].Value = (i + 1).ToString();
                    try
                    {
                        dataGridView1[1, i].Value = controllerScript.myCA.StateCount[i];
                    }
                    catch (Exception)
                    {
                        dataGridView1[1, i].Value = "N/A";
                    }
                    //try
                    //{
                    //    dataGridView1[2, i].Value = controllerScript.myCA.Transitions[i];
                    //}
                    //catch (Exception)
                    //{
                    //    dataGridView1[1, i].Value = "N/A";
                    //}
                    try
                    {
                        dataGridView1[3, i].Value = controllerScript.myCA.CIndexes[i];
                    }
                    catch (Exception)
                    {
                        dataGridView1[3, i].Value = "N/A";
                    }
                    //dataGridView1[3, i].Value = controllerScript.ReturnConnectivityIndex(i);
                }
                //dataGridView1.Rows.Add(new DataGridViewRow());
                //dataGridView1[0, states].Value = misc;
                //dataGridView1[1, states].Value = ci + controllerScript.ReturnConnectivityIndex();
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
        }

        void SetSize()
        {
            this.Height = 62 + (22 * dataGridView1.Rows.Count);
            this.Width = 233;
        }

        public void UpdateCounts()
        {
            for (int i = 0; i < states; i++)
            {
                dataGridView1[1, i].Value = controllerScript.myCA.StateCount[i];
                //dataGridView1[2, i].Value = controllerScript.myCA.Transitions[i];
                //dataGridView1[3, i].Value = controllerScript.ReturnConnectivityIndex(i);
            }
        }

        private void Counter_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.counterFormOpen = false;
        }
    }
}
