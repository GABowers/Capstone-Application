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

        TabPage tabPage2;
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
                controllerScript.SetMainInfo(stateNumberBox, gridSizeHori, gridSizeVert);
                InstantiateNewTabs();
                PreventChanges();
                RetrieveValues();
            }
        }

        void PreventChanges()
        {
            stateNumberBox.Enabled = false;
            gridSizeHori.Enabled = false;
            gridSizeVert.Enabled = false;
            //lock advanced placement
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
                UserControl2 uc2 = new UserControl2(amountOfStates, i);
                tabPage2 = new TabPage();
                int j = i + 1;
                tabPage2.Text = "State " + j;
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.TabPages[j].Controls.Add(uc2);
                uc2.Name = "uc." + j;
                
                this.Show();
            }
        }

        void RetrieveValues()
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            //for loop for each tab
            for (int i = 1; i < tabControl1.TabPages.Count; ++i)
            {
                UserControl2 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl2>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                controllerScript.AdvancedRetrieveProbValues(newUC, i);
            }
        }

        private void UpdateAllValues()
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);

            //for loop for each tab
            for (int i = 1; i < tabControl1.TabPages.Count; ++i)
            {
                UserControl2 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl2>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
                controllerScript.AdvancedUpdateProbValues(newUC, i);
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
                if(int.TryParse(stateNumberBox.Text, out int result1) && int.TryParse(gridSizeHori.Text, out int result2) && int.TryParse(gridSizeVert.Text, out int result3))
                {
                    controllerScript.MainPageNext(int.Parse(stateNumberBox.Text), int.Parse(gridSizeHori.Text), int.Parse(gridSizeVert.Text));
                    InstantiateNewTabs();
                }
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
            int amountOfStates = int.Parse(stateNumberBox.Text);
            //add code to save all tab data to new class
            UpdateAllValues();
            if (editForm == false)
            {
                mainForm.UpdateIterationResetCell(int.Parse(stateNumberBox.Text));
                mainForm.UpdateIterationPauseCell(int.Parse(stateNumberBox.Text));
            }

            Form1.runSettings = new RunSettings(amountOfStates);
            controllerScript.runSettings = Form1.runSettings;
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