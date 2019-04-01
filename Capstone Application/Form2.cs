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
        object templateUC;
        bool editForm;
        bool template_reset = false;
        bool finalized = false;
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
            //MAJOR
            //MAJOR
            //MAJOR BUG HERE: When deleting pages, it somehow keeps the last one! And leaves one extra page with things messed up. THIS MUST BE FIXED.
            for (int i = 1; i < tabControl1.TabPages.Count; ++i)
            {
                tabControl1.TabPages.Remove(tabControl1.TabPages[i]);
            }
            if(int.TryParse(stateNumberBox.Text, out int result))
            {
                for (int i = 0; i < result; ++i)
                {
                    UserControl2 uc2 = new UserControl2(controllerScript.GetStatePage(i), result, i);
                    tabPage2 = new TabPage();
                    int j = i + 1;
                    tabPage2.Text = "State " + j;
                    tabControl1.TabPages.Add(tabPage2);
                    tabControl1.TabPages[j].Controls.Add(uc2);
                    uc2.Name = "uc." + j;
                    this.Show();
                }
            }
            //Use this to add tabs
            
        }

        void RetrieveValues()
        {
            stateNumberBox.Text = controllerScript.MainPageInfo.numStates.ToString();
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
            finalized = true;
            // Consider modifying so StatePageInfo class is added/edited when moving between pages with next/previous
            
            //WHERE SHOULD THIS BEEEEEE
            if (tabControl1.SelectedIndex == 0)
            {
                switch(template)
                {
                    case Template.None:
                        if (int.TryParse(stateNumberBox.Text, out int result1) && int.TryParse(gridSizeHori.Text, out int result2) && int.TryParse(gridSizeVert.Text, out int result3))
                        {
                            controllerScript.MainPageNext(int.Parse(stateNumberBox.Text), int.Parse(gridSizeHori.Text), int.Parse(gridSizeVert.Text), Template.None);
                            InstantiateNewTabs();
                        }
                        break;

                    default:
                        FinalizeTemplates();
                        InstantiateNewTabs();
                        break;
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
                    break;
                case Template.DLA:
                    {
                    }
                        break;
                case Template.Isle_Royale:
                    break;
                case Template.Ant_Sim:
                    break;
                case Template.Gas:
                    {
                        GasTemplateUC gasUC = new GasTemplateUC();
                        templatePanel.Controls.Add(gasUC);
                        templateUC = gasUC;
                    }
                    break;
                case Template.Random_Walk:
                    {
                        RandomTemplateUC ranUC = new RandomTemplateUC();
                        templatePanel.Controls.Add(ranUC);
                        templateUC = ranUC;
                    }
                    break;
            }
        }

        private void FinalizeTemplates()
        {
            switch (template)
            {
                case Template.Random_Walk:
                    {
                        RandomTemplateUC ranUC = (RandomTemplateUC)templateUC;
                        bool _1d = ranUC.radio1D.Checked;
                        controllerScript.UpdateMainTemplateInfo(template_reset);
                        int hori = 51;
                        double up = _1d? 0 : 0.25;
                        double down = _1d ? 0 : 0.25;
                        double left = _1d ? 0.50 : 0.25;
                        double right = _1d ? 0.50 : 0.25;
                        if (int.TryParse(gridSizeHori.Text, out int result1))
                        {
                            hori = result1;
                        }
                        int vert = _1d ? 1 : 51;
                        if (int.TryParse(gridSizeVert.Text, out int result2))
                        {
                            vert = result2;
                        }
                        int statenum = int.Parse(stateNumberBox.Text);
                        controllerScript.MainPageNext(statenum, hori, vert, Template.Random_Walk);
                        int halfHori = (int)(hori / 2.0);
                        int halfVert = (int)(vert / 2.0);
                        List<List<List<double>>> immobile_probs = new List<List<List<double>>>();
                        List<List<List<double>>> tempProbs = new List<List<List<double>>>();
                        controllerScript.StateInfoDirectEdit(0, NType.None, GridType.Box, Color.White,
                            new List<Tuple<int, int>>() { new Tuple<int, int>(halfHori, halfVert)}, 0, 1, 
                            tempProbs, true, 0, new List<double>() { up, right, down, left },
                            true, new List<double>() { 0, 0 }, false, false, false, new List<Tuple<string, double>>());
                    }
                    break;

                case Template.Gas:
                    {
                        GasTemplateUC gasUC = (GasTemplateUC)templateUC;
                        List<double> template_storage = new List<double>();
                        string m_name = gasUC.name;
                        double mol_vol = gasUC.GetVolume();
                        double vrms = gasUC.GetVRMS();
                        double mm = gasUC.GetMM();
                        double t = gasUC.temperature;
                        double cross_sec = gasUC.GetCross(mol_vol);
                        int hori = 1000;
                        if (int.TryParse(gridSizeHori.Text, out int result1))
                        {
                            hori = result1;
                        }
                        int vert = 1000;
                        if (int.TryParse(gridSizeVert.Text, out int result2))
                        {
                            vert = result2;
                        }
                        controllerScript.MainPageNext(1, hori, vert, Template.Gas);
                        controllerScript.StateInfoDirectEdit(0, NType.None, GridType.Box, Color.White,
                            new List<Tuple<int, int>>(), 0, 0, new List<List<List<double>>>(), true, 4, new List<double>() { 0.25, 0.25, 0.25, 0.25 },
                            false, new List<double>(), false, false, false, new List<Tuple<string, double>>());
                        double full_vol = ((hori) * (vert) * mol_vol);
                        template_storage.Add(t);
                        template_storage.Add(full_vol);
                        template_storage.Add(mm);
                        template_storage.Add(Math.Pow(mol_vol, (1.0 / 3.0)));
                        template_storage.Add(cross_sec);
                        template_storage.Add(gasUC.k);
                        template_storage.Add(vrms);
                        template_storage.Add(gasUC.avagodro);
                        template_storage.Add(gasUC.resolution);
                        controllerScript.GetStatePage(0).template_objects = new List<object>();
                        for (int i = 0; i < template_storage.Count; i++)
                        {
                            controllerScript.GetStatePage(0).template_objects.Add(template_storage[i]);
                        }
                    }
                    break;
                case Template.DLA:
                    {
                        controllerScript.UpdateMainTemplateInfo(template_reset);
                        int hori = 101;
                        if (int.TryParse(gridSizeHori.Text, out int result1))
                        {
                            hori = result1;
                        }
                        int vert = 101;
                        if (int.TryParse(gridSizeVert.Text, out int result2))
                        {
                            vert = result2;
                        }
                        controllerScript.MainPageNext(2, hori, vert, Template.DLA);
                        int halfHori = (int)((double)hori / 2);
                        List<List<List<double>>> immobile_probs = new List<List<List<double>>>();
                        for (int i = 0; i < 2; i++)
                        {
                            immobile_probs.Add(new List<List<double>>());
                            for (int j = 0; j < 2; j++)
                            {
                                immobile_probs[i].Add(new List<double>());

                                for (int l = 0; l < 4 + 1; l++)
                                {
                                    immobile_probs[i][j].Add(0);
                                    // add labels and text fields
                                }
                            }
                        }
                        controllerScript.StateInfoDirectEdit(0, NType.VonNeumann, GridType.Box, Color.Gray,
                            new List<Tuple<int, int>>() { new Tuple<int, int>((int)((double)hori / 2), (int)((double)vert / 2)) },
                            4, 1, immobile_probs, false, 0, new List<double>(), false, new List<double>(),
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
                            true, new List<double>() { 0, 0 }, false, false, false, new List<Tuple<string, double>>());
                    }
                    break;
            }
        }

        private void confirmTab_Click(object sender, EventArgs e)
        {
            int amountOfStates = int.Parse(stateNumberBox.Text);
            if(!finalized)
            {
                FinalizeTemplates();
            }
            UpdateAllValues();

            //add code to save all tab data to new class

            if(!editForm)
            {
                Form1.runSettings = new RunSettings(amountOfStates);
                controllerScript.runSettings = Form1.runSettings;
                //controllerScript.ResetTemplate();
                controllerScript.ResetRuns();
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

        private void templateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(templateBox.SelectedIndex)
            {
                case 0:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = true;
                    stateNumberBox.Text = 0.ToString();
                    template = Template.None;
                    DisableTemplateResetInfo();
                    break;
                case 1:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = false;
                    stateNumberBox.Text = 1.ToString();
                    template = Template.Random_Walk;
                    RunTemplates();
                    break;
                case 2:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = false;
                    stateNumberBox.Text = 2.ToString();
                    template = Template.DLA;
                    EnableTemplateResetInfo();
                    RunTemplates();
                    break;
                case 3:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = false;
                    template = Template.Isle_Royale;
                    RunTemplates();
                    break;
                case 4:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = false;
                    template = Template.Ant_Sim;
                    RunTemplates();
                    break;
                case 5:
                    controllerScript.SetupStateInfo();
                    stateNumberBox.Enabled = false;
                    stateNumberBox.Text = 1.ToString();
                    template = Template.Gas;
                    RunTemplates();
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
    Ant_Sim,
    Gas
}