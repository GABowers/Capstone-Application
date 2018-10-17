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
        Tuple<Tuple<List<bool>, List<bool>, List<bool>>, List<bool>, List<bool>> typeChecks;

        public SaveDataDialog()
        {
            InitializeComponent();
            GenFields();
            splitContainer2.Panel1.AutoScroll = true;
            RetrieveOrCreate();
        }

        void RetrieveOrCreate()
        {
            //if (runSettings != null)
            //{

            //}
            //else
            //{
            //    runSettings = new RunSettings(controller.amountOfCellTypes);
            //}
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
            if (!checking)
            {
                checking = true;
                RecursiveCheck(saveOptions.SelectedNode, saveOptions.SelectedNode.Checked);
                checking = false;
            }
            runSettings.SaveCounts = saveOptions.Nodes[0].Nodes[0].Checked;
            runSettings.SaveTrans = saveOptions.Nodes[0].Nodes[1].Checked;
            runSettings.SaveIndex = saveOptions.Nodes[0].Nodes[2].Checked;
        }

        void RecursiveCheck(TreeNode parent, bool checkState)
        {
            Console.WriteLine("Parent: " + parent);
            parent.Checked = checkState;
            foreach(TreeNode sub in parent.Nodes)
            {
                Console.WriteLine("Sub: " + sub);
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
    }
}
