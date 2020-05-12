using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Capstone_Application
{
    public partial class SaveDataDialog : Form
    {
        ControllerScript controller = Form1.controllerScript;
        RunSettings runSettings = Form1.runSettings;
        List<string> messages = new List<string>();
        bool checking = false;

        private bool finishedLoading = false;

        public SaveDataDialog()
        {
            InitializeComponent();
            GenFields();
            splitContainer2.Panel1.AutoScroll = true;
            RetrieveOrCreate();
            finishedLoading = true;
        }

        void RetrieveOrCreate()
        {
            if(!runSettings.Fresh)
            {
                templatePath.Text = runSettings.TemplatePath;
                templateIncrementInput.Text = string.Join(", ", runSettings.TemplateIncs);
                savePathsFolderInput.Text = runSettings.PathsPath;
                pathsIncInput.Text = string.Join(", ", runSettings.PathsIncs);
                saveImageFolderInput.Text = runSettings.ImagePath;
                imageIncInput.Text = string.Join(", ", runSettings.ImageIncs);
                saveDataFolderInput.Text = runSettings.DataPath;
                dataIncInput.Text = string.Join(", ", runSettings.DataIncs);
                saveOptions.SelectedNode = saveOptions.Nodes[0];
                saveOptions.Nodes[0].Checked = runSettings.SaveCounts;
                saveOptions.SelectedNode = saveOptions.Nodes[1];
                saveOptions.Nodes[1].Checked = runSettings.SaveTrans;
                saveOptions.SelectedNode = saveOptions.Nodes[2];
                saveOptions.Nodes[2].Checked = runSettings.SaveIndex;
                //if(runSettings.SaveCounts || runSettings.SaveTrans || runSettings.SaveIndex)
                //{
                //    saveOptions.SelectedNode = saveOptions.Nodes[0];
                //    saveOptions.Nodes[0].Checked = true;
                //}
                int num = controller.amountOfCellTypes;
                for (int i = 0; i < num; i++)
                {
                    string name_reset = "resetInput" + i;
                    string name_pause = "pauseInput" + i;
                    TextBox box_reset = (TextBox)resetAgentPanel.Controls.Find(name_reset, true).First();
                    TextBox box_pause = (TextBox)pauseAgentPanel.Controls.Find(name_pause, true).First();
                    box_reset.Text = runSettings.ResetCounts[i].ToString();
                    box_pause.Text = runSettings.PauseCounts[i].ToString();
                }
                resetIterationInput.Text = string.Join(", ", runSettings.ResetIterations);
                pauseIterationInput.Text = string.Join(", ", runSettings.PauseIterations);
                pauseRunInput.Text = string.Join(", ", runSettings.PauseRuns);
            }
            else
            {
                runSettings.Fresh = false;
            }
        }

        void GenFields()
        {
            resetAgentPanel.AutoScroll = true;
            pauseAgentPanel.AutoScroll = true;
            int num = controller.amountOfCellTypes;
            int x1 = 0;
            int x2 = 150;
            int y = 0;
            for (int i = 0; i < num; i++)
            {
                System.Drawing.Point textLoc = new System.Drawing.Point(x1, y);
                System.Drawing.Point inputLoc = new System.Drawing.Point(x2, y);
                Label resetText = new Label() { Name = "resetText" + i, Text = "Agent " + (i + 1), Location = textLoc, Size = new Size(50, 20) };
                Label pauseText = new Label() { Name = "pauseText" + i, Text = "Agent " + (i + 1), Location = textLoc, Size = new Size(50, 20) };
                TextBox resetAgentInput = new TextBox() { Name = "resetInput" + i, Location = inputLoc, Size = new Size(50, 20) };
                TextBox pauseAgentInput = new TextBox() { Name = "pauseInput" + i, Location = inputLoc, Size = new Size(50, 20) };
                resetAgentInput.TextChanged += (sender, e) =>
                {
                    string name = resetAgentInput.Name;
                    string shortName = name.Remove(0, 10);
                    int val = int.Parse(shortName);
                    if (int.TryParse(resetAgentInput.Text, out int result))
                    {
                        int size = runSettings.ResetCounts.Count();
                        runSettings.ResetCounts[val] = result;
                    }
                    else
                    {
                        runSettings.ResetCounts[val] = -1;
                    }
                };
                pauseAgentInput.TextChanged += (sender, e) =>
                {
                    string name = pauseAgentInput.Name;
                    string shortName = name.Remove(0, 10);
                    int val = int.Parse(shortName);
                    if (int.TryParse(pauseAgentInput.Text, out int result2))
                    {
                        int size = runSettings.ResetCounts.Count();
                        runSettings.PauseCounts[val] = result2;
                    }
                    else
                    {
                        runSettings.ResetCounts[val] = -1;
                    }
                };

                resetAgentPanel.Controls.Add(resetText);
                resetAgentPanel.Controls.Add(resetAgentInput);
                pauseAgentPanel.Controls.Add(pauseText);
                pauseAgentPanel.Controls.Add(pauseAgentInput);
                y += 25;
            }

            switch(controller.MainPageInfo.template)
            {
                case Template.None:
                    templateSpecificLabel.Text = "You're not using a template. These options only appear when doing so.";
                    break;
                case Template.Random_Walk:
                    templateSpecificLabel.Text = "Save a histogram of each agent's final location over multiple runs.";
                    metaSaveCheckBox.Checked = true;
                    for (int i = 0; i < saveOptions.Nodes.Count; i++)
                    {
                        if(saveOptions.Nodes[i].Text.Contains("Path"))
                        {
                            saveOptions.Nodes[i].Checked = true;
                        }
                    }
                    break;
            }
        }

        private void dataIncInput_Leave(object sender, EventArgs e)
        {
            // checks if inputs are valid. If so, adds them to list.
            List<int> list = new List<int>();
            string[] strings = dataIncInput.Text.Split(',');
            for (int i = 0; i < strings.Length; i++)
            {
                if(int.TryParse(strings[i], out int result))
                {
                    list.Add(result);
                }
            }
        }

        private void SaveJSON(string filename)
        {
            Type t = typeof(RunSettings);
            using (Stream stream = File.Open(filename, FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(t);
                json.WriteObject(stream, runSettings);
            }
        }

        private void LoadJSON(string filename)
        {
            Type t = typeof(RunSettings);
            using (Stream stream = File.OpenRead(filename))
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(t);
                Object thing = json.ReadObject(stream);
                runSettings = (RunSettings)thing;
            }
        }

        private void saveDataFolderInput_TextChanged(object sender, EventArgs e)
        {
            string filepath = saveDataFolderInput.Text;
            bool real = Directory.Exists(filepath);
            if(!real)
            {
                messages.Add("Path doesn't exist. It will be created.");
                runSettings.DataPath = saveDataFolderInput.Text;
            }
            else
            {
                runSettings.DataPath = saveDataFolderInput.Text;
            }
        }

        private void messageNextButton_Click(object sender, EventArgs e)
        {

        }

        private void messagePrevButton_Click(object sender, EventArgs e)
        {
            // messages.next
        }

        private void resetIterationInput_TextChanged(object sender, EventArgs e)
        {
            //if(int.TryParse(resetIterationInput.Text, out int result))
            //{
            //    resetIterationInput.BackColor = SystemColors.Window;
            //}
            //else
            //{
            //    resetIterationInput.BackColor = Color.PaleVioletRed;
            //}
            runSettings.ResetIterations.Clear();
            string[] text = resetIterationInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if(int.TryParse(text[i], out int result))
                {
                    runSettings.ResetIterations.Add(result);
                }
            }
        }

        private void saveOptions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (finishedLoading)
            {
                for (int i = 0; i < saveOptions.Nodes.Count; i++)
                {
                    string name = saveOptions.Nodes[i].Text;
                    if (name.Contains("Count"))
                    {
                        runSettings.SaveCounts = saveOptions.Nodes[i].Checked;
                    }
                    else if (name.Contains("Trans"))
                    {
                        runSettings.SaveTrans = saveOptions.Nodes[i].Checked;
                    }
                    else if (name.Contains("Index"))
                    {
                        runSettings.SaveIndex = saveOptions.Nodes[i].Checked;
                    }
                    else if (name.Contains("Path"))
                    {
                        runSettings.SavePaths = saveOptions.Nodes[i].Checked;
                    }
                    else if (name.Contains("Image"))
                    {
                        runSettings.SaveImage = saveOptions.Nodes[i].Checked;
                    }
                }
            }
        }

        void RecursiveCheck(TreeNode parent, bool checkState)
        {
            //Console.WriteLine("Parent: " + parent);
            parent.Checked = checkState;
            foreach(TreeNode sub in parent.Nodes)
            {
                //Console.WriteLine("Sub: " + sub);
                RecursiveCheck(sub, checkState);
            }
        }

        private void pauseIterationInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.PauseIterations.Clear();
            string[] text = pauseIterationInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.PauseIterations.Add(result);
                }
            }
        }

        private void dataIncInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.DataIncs.Clear();
            string[] text = dataIncInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.DataIncs.Add(result);
                }
            }

        }

        private void imageIncInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.ImageIncs.Clear();
            string[] text = imageIncInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.ImageIncs.Add(result);
                }
            }

        }

        private void pathsIncInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.PathsIncs.Clear();
            string[] text = pathsIncInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.PathsIncs.Add(result);
                }
            }
        }

        private void savePathsFolderInput_TextChanged(object sender, EventArgs e)
        {
            string filepath = savePathsFolderInput.Text;
            bool real = Directory.Exists(filepath);
            if (!real)
            {
                messages.Add("Path doesn't exist. It will be created.");
                runSettings.PathsPath = savePathsFolderInput.Text;
            }
            else
            {
                runSettings.PathsPath = savePathsFolderInput.Text;
            }
        }

        private void saveImageFolderInput_TextChanged(object sender, EventArgs e)
        {
            string filepath = saveImageFolderInput.Text;
            bool real = Directory.Exists(filepath);
            if (!real)
            {
                messages.Add("Path doesn't exist. It will be created.");
                runSettings.ImagePath = saveImageFolderInput.Text;
            }
            else
            {
                runSettings.ImagePath = saveImageFolderInput.Text;
            }
        }

        private void templatePath_TextChanged(object sender, EventArgs e)
        {
            // check if nonsensical
            string filepath = templatePath.Text;
            bool real = Directory.Exists(filepath);
            if (!real)
            {
                messages.Add("Path doesn't exist. It will be created.");
            }
            runSettings.TemplatePath = templatePath.Text;
        }

        private void templateIncrementInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.TemplateIncs.Clear();
            string[] text = templateIncrementInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.TemplateIncs.Add(result);
                }
            }
        }

        private void pauseRunInput_TextChanged(object sender, EventArgs e)
        {
            runSettings.PauseRuns.Clear();
            string[] text = pauseRunInput.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.PauseRuns.Add(result);
                }
            }
        }

        private void metaSaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            runSettings.MetaStore = metaSaveCheckBox.Checked;
        }

        private void histBox_TextChanged(object sender, EventArgs e)
        {
            runSettings.HistIncs.Clear();
            string[] text = histBox.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.HistIncs.Add(result);
                }
            }
        }

        private void histPathBox_TextChanged(object sender, EventArgs e)
        {
            string filepath = histPathBox.Text;
            bool real = Directory.Exists(filepath);
            if (!real)
            {
                messages.Add("Path doesn't exist. It will be created.");
            }
            runSettings.HistPath = histPathBox.Text;
        }

        private void histRunBox_TextChanged(object sender, EventArgs e)
        {
            runSettings.HistRunIncs.Clear();
            string[] text = histRunBox.Text.Split(',');
            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out int result))
                {
                    runSettings.HistRunIncs.Add(result);
                }
            }
        }
    }
}
