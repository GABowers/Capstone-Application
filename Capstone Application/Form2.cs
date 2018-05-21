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
    public partial class Form2 : Form
    {
        Form1 mainForm;
        ControllerScript controllerScript = Form1.controllerScript;
        MainPageController mainPageController;
        GridType gridType;
        NType neighborType;

        TabPage tabPage2;
        int currentGridType = 0;
        int neighborCount = 0;
        bool editForm;
        public Form2(string name, Form1 main, bool edit)
        {
            editForm = edit;
            mainForm = main;
            InitializeComponent();
            this.Text = name;
            mainPageController = new MainPageController();
            if (editForm)
            {
                controllerScript.SetMainInfo(caTypeBox, neighborTypeBox, gridTypeBox, stateNumberBox, gridSizeHori, gridSizeVert);
                InstantiateNewTabs();
                PreventChanges();
                RetrieveValues();
            }
        }

        void PreventChanges()
        {
            caTypeBox.Enabled = false;
            neighborTypeBox.Enabled = false;
            gridTypeBox.Enabled = false;
            stateNumberBox.Enabled = false;
            gridSizeHori.Enabled = false;
            gridSizeVert.Enabled = false;
            checkBoxCount.Enabled = false;
            checkBoxTrans.Enabled = false;
            checkBoxBIndex.Enabled = false;
            checkBoxPath.Enabled = false;
            for (int i = 0; i < (tabControl1.TabPages.Count - 1); i++)
            {
                int intToUse = i + 1;
                string tabName = "uc." + intToUse.ToString();
                tabControl1.TabPages[intToUse].Controls[tabName].Controls["agentCount"].Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void InstantiateNewTabs()
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            //MAJOR
            //MAJOR
            //MAJOR BUG HERE: When deleting pages, it somehow keeps the last one! And leaves one extra page with things messed up. THIS MUST BE FIXED.
            for (int i = 1; i < tabControl1.TabPages.Count; ++i)
            {
                tabControl1.TabPages.Remove(tabControl1.TabPages[i]);
            }
            //Use this to add tabs
            for (int i = 0; i < amountOfStates; ++i)
            {
                if (this.caTypeBox.SelectedIndex == 0)
                {
                    if (this.neighborTypeBox.SelectedIndex == 4)
                    {
                        UserControl2 uc2 = new UserControl2();
                        tabPage2 = new TabPage();
                        int j = i + 1;
                        tabPage2.Text = "State " + j;
                        uc2.UpdateProbFields(j, amountOfStates);
                        //controllerScript.UpdateProbFields(uc, j);
                        //uc.UpdateValues(j, amountOfStates, neighborCount);
                        tabControl1.TabPages.Add(tabPage2);
                        tabControl1.TabPages[j].Controls.Add(uc2);
                        uc2.Name = "uc." + j;
                    }
                    else
                    {
                        UserControl1 uc = new UserControl1();
                        tabPage2 = new TabPage();
                        int j = i + 1;
                        tabPage2.Text = "State " + j;
                        uc.UpdateProbFields(j, amountOfStates, neighborCount);
                        uc.FillForm();
                        //controllerScript.UpdateProbFields(uc, j);
                        //uc.UpdateValues(j, amountOfStates, neighborCount);
                        tabControl1.TabPages.Add(tabPage2);
                        tabControl1.TabPages[j].Controls.Add(uc);
                        uc.Name = "uc." + j;
                    }
                }

                else if(this.caTypeBox.SelectedIndex == 1)
                {
                    int j = i + 1;
                    _2ndOrderTabs uc3 = new _2ndOrderTabs(mainForm, this, j, amountOfStates);
                    tabPage2 = new TabPage();
                    tabPage2.Text = "State " + j;
                    //uc3.SetInfo(j, amountOfStates);
                    //uc3.UpdateStickFields(j, amountOfStates);
                    // uc3.UpdateProbFields(j, amountOfStates);
                    // controllerScript.UpdateProbFields(uc, j);
                    // uc.UpdateValues(j, amountOfStates, neighborCount);
                    tabControl1.TabPages.Add(tabPage2);
                    tabControl1.TabPages[j].Controls.Add(uc3);
                    uc3.Name = "uc." + j;
                }
                
                this.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void caType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(caTypeBox.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    break;
            }
        }

        private void neighborType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (neighborTypeBox.SelectedIndex)
            {
                case 0:
                    neighborCount = 0;
                    controllerScript.neighborhoodType = 0;
                    neighborType = NType.None;
                    break;

                case 1:
                    neighborCount = 4;
                    controllerScript.neighborhoodType = 1;
                    neighborType = NType.VonNeumann;
                    break;

                case 2:
                    neighborCount = 8;
                    controllerScript.neighborhoodType = 2;
                    neighborType = NType.Moore;
                    break;

                case 3:
                    neighborCount = 12;
                    controllerScript.neighborhoodType = 3;
                    neighborType = NType.Hybrid;
                    break;
                case 4:
                    neighborCount = 0;
                    controllerScript.neighborhoodType = 4;
                    neighborType = NType.Advanced;
                    break;
            }
        }

        private void gridType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (gridTypeBox.SelectedIndex)
            {
                case 0:
                    gridType = GridType.Box;
                    break;

                case 1:
                    gridType = GridType.CylinderW;
                    break;

                case 2:
                    gridType = GridType.CylinderH;
                    break;

                case 3:
                    gridType = GridType.Torus;
                    break;
            }
        }

        void RetrieveValues()
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            //for loop for each tab
            if (this.caTypeBox.SelectedIndex == 0)
            {
                if (this.neighborTypeBox.SelectedIndex == 4)
                {
                    // add check for each tab to see rows and columns
                    for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                    {
                        UserControl2 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl2>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                        controllerScript.AdvancedRetrieveProbValues(newUC, i);
                    }
                }
                else
                {
                    for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                    {
                        UserControl1 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl1>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                        controllerScript.RetrieveProbValues(newUC, i);
                    }
                }
            }
            else if (this.caTypeBox.SelectedIndex == 1)
            {
                // add neighbor-specific stuff later

                for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                {
                    _2ndOrderTabs newUC = tabControl1.TabPages[i].Controls.Cast<_2ndOrderTabs>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                    controllerScript.Retrieve2ndOrder(newUC, i);
                }
            }
        }

        private void UpdateAllValues()
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            if(checkBoxCount.Checked)
            {
                mainForm.settingsScript.CountPossible = true;
            }
            else
            {
                mainForm.settingsScript.CountPossible = false;
            }
            if (checkBoxTrans.Checked)
            {
                mainForm.settingsScript.TransPossible = true;
            }
            else
            {
                mainForm.settingsScript.TransPossible = false;
            }
            if (checkBoxBIndex.Checked)
            {
                mainForm.settingsScript.BIndexPossible = true;
            }
            else
            {
                mainForm.settingsScript.BIndexPossible = false;
            }
            if (checkBoxPath.Checked)
            {
                mainForm.settingsScript.PathPossible = true;
            }
            else
            {
                mainForm.settingsScript.PathPossible = false;
            }
            //for loop for each tab
            if (this.caTypeBox.SelectedIndex == 0)
            {
                if (this.neighborTypeBox.SelectedIndex == 4)
                {
                    // add check for each tab to see rows and columns
                    for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                    {
                        UserControl2 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl2>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                        controllerScript.AdvancedUpdateProbValues(newUC, i);
                    }
                }
                else
                {
                    for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                    {
                        UserControl1 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl1>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                        controllerScript.UpdateProbValues(newUC, i);
                    }
                }
            }
            else if(this.caTypeBox.SelectedIndex == 1)
            {
                // add neighbor-specific stuff later

                for (int i = 1; i < tabControl1.TabPages.Count; ++i)
                {
                    _2ndOrderTabs newUC = tabControl1.TabPages[i].Controls.Cast<_2ndOrderTabs>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                    controllerScript.Update2ndOrder(newUC, i);
                }
            }
            if(editForm)
            {
                controllerScript.EditCA();
            }
        }

        private void previousTab_Click(object sender, EventArgs e)
        {
            int prevTab = tabControl1.SelectedIndex - 1;
            if (prevTab < 0)
                prevTab = tabControl1.SelectedIndex;
            //UpdateAllValues();
            tabControl1.SelectTab(prevTab);
        }

        private void nextTab_Click(object sender, EventArgs e)
        {
            // Consider modifying so StatePageInfo class is added/edited when moving between pages with next/previous
            
                //WHERE SHOULD THIS BEEEEEE
                if (tabControl1.SelectedIndex == 0)
                {
                    controllerScript.MainPageNext(gridType, neighborType, int.Parse(stateNumberBox.Text), int.Parse(gridSizeHori.Text), int.Parse(gridSizeVert.Text), caTypeBox.SelectedIndex);
                    InstantiateNewTabs();
                }
                else
                {
                    //UpdateAllValues();
                }
                int nextTab = tabControl1.SelectedIndex + 1;
                if (nextTab > (tabControl1.TabCount - 1))
                    nextTab = tabControl1.SelectedIndex;
                tabControl1.SelectTab(nextTab);
        }

        private void cancelTab_Click(object sender, EventArgs e)
        {
            this.Close();
            //add code to discard new class (where info is sent)
            //NOTE: currently all data is re-saved when a new tab is tabbed. It should be saved as a copy of original data, so if the user presses cancel the old data is retained.
        }

        private void confirmTab_Click(object sender, EventArgs e)
        {
            //add code to save all tab data to new class
            UpdateAllValues();
            if (editForm == false)
            {
                mainForm.UpdateIterationResetCell(int.Parse(stateNumberBox.Text));
                mainForm.UpdateIterationPauseCell(int.Parse(stateNumberBox.Text));
            }
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.OnOtherFormClose();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
public enum GridType
{
    Box,
    CylinderW,
    CylinderH,
    Torus
}