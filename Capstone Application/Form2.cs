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
        Template template;
        TabPage tabPage2;
        bool editForm;
        bool template_reset = false;
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
                //RetrieveValues();
            }
            templateBox.SelectedIndex = 0;
            DisableTemplateResetInfo();
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

        //void RetrieveValues()
        //{
        //    int amountOfStates = int.Parse(stateNumberBox.Text);
        //    //for loop for each tab
        //    for (int i = 1; i < tabControl1.TabPages.Count; ++i)
        //    {
        //        UserControl2 newUC = tabControl1.TabPages[i].Controls.Cast<UserControl2>().Where(c => c.Name == ("uc." + i)).FirstOrDefault();
        //        controllerScript.AdvancedRetrieveProbValues(newUC, i);
        //    }
        //}

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
                    controllerScript.MainPageNext(int.Parse(stateNumberBox.Text), int.Parse(gridSizeHori.Text), int.Parse(gridSizeVert.Text), Template.None);
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

        private void RunTemplates()
        {
            switch (template)
            {
                case Template.None:
                    UpdateAllValues();
                    break;
                case Template.DLA:
                    controllerScript.UpdateMainTemplateInfo(template_reset);
                    int hori = 101;
                    if(int.TryParse(gridSizeHori.Text, out int result1))
                    {
                        hori = result1;
                    }
                    int vert = 101;
                    if(int.TryParse(gridSizeVert.Text, out int result2))
                    {
                        vert = result2;
                    }
                    controllerScript.MainPageNext(2, hori, vert, Template.DLA);
                    int halfHori = (int)((double)hori / 2);
                    List<double> moveProbs = new List<double>() { 0.25, 0.25, 0.25, 0.25};
                    List<double> stickingProbs = new List<double>() { 1};
                    controllerScript.StateInfoDirectEdit(0, NType.None, GridType.Box, Color.Gray,
                        new List<Tuple<int, int>>() { new Tuple<int, int>((int)((double)hori / 2), (int)((double)vert / 2)) },
                        0, 1, new List<List<List<double>>>(), false, 0, new List<double>(), false, new List<double>(),
                        false, false, false, new List<Tuple<string, double>>());
                    List<List<List<double>>> tempProbs = new List<List<List<double>>>();
                    for (int i = 0; i < 2; i++)
                    {
                        tempProbs.Add(new List<List<double>>());
                        for (int j = 0; j < 2; j++)
                        {
                            tempProbs[i].Add(new List<double>());
                            
                            for (int l = 0; l < 4 + 1; l++)
                            {
                                tempProbs[i][j].Add(0);
                                // add labels and text fields
                            }
                        }
                    }
                    tempProbs[0][0][0] = 0;
                    tempProbs[0][0][1] = 1;
                    tempProbs[0][0][2] = 1;
                    tempProbs[0][0][3] = 1;
                    tempProbs[0][0][4] = 1;
                    controllerScript.StateInfoDirectEdit(1, NType.VonNeumann, GridType.Box, Color.White,
                        new List<Tuple<int, int>>(), 4, 0, tempProbs, true, 0, new List<double>() { 0.25, 0.25, 0.25, 0.25 },
                        true, new List<double>() { 1, 0}, false, false, false, new List<Tuple<string, double>>());
                    break;
                case Template.Isle_Royale:
                    break;
                case Template.Ant_Sim:
                    break;
                default:
                    UpdateAllValues();
                    break;
            }
        }

        private void confirmTab_Click(object sender, EventArgs e)
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            RunTemplates();
            
            //add code to save all tab data to new class
            
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

        private void templateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(templateBox.SelectedIndex)
            {
                case 0:
                    stateNumberBox.Enabled = true;
                    template = Template.None;
                    DisableTemplateResetInfo();
                    break;
                case 1:
                    stateNumberBox.Enabled = false;
                    stateNumberBox.Text = 2.ToString();
                    template = Template.Random_Walk;
                    break;
                case 2:
                    stateNumberBox.Enabled = false;
                    stateNumberBox.Text = 2.ToString();
                    template = Template.DLA;
                    EnableTemplateResetInfo();
                    break;
                case 3:
                    stateNumberBox.Enabled = false;
                    template = Template.Isle_Royale;
                    break;
                case 4:
                    stateNumberBox.Enabled = false;
                    template = Template.Ant_Sim;
                    break;
                default:
                    template = Template.None;
                    break;
            }
        }

        private void EnableTemplateResetInfo()
        {
            template_reset_label.Visible = true;
            template_reset_checkbox.Visible = true;
            template_reset_explanation.Visible = true;
        }

        private void DisableTemplateResetInfo()
        {
            template_reset_label.Visible = false;
            template_reset_checkbox.Visible = false;
            template_reset_explanation.Visible = false;
        }

        private void template_reset_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(template_reset_checkbox.Checked)
            {
                template_reset = true;
            }
            else
            {
                template_reset = false;
            }
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

public enum Template
{
    None,
    Random_Walk,
    DLA,
    Isle_Royale,
    Ant_Sim
}