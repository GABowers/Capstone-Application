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
    public partial class EditWindow : Form
    {
        Form1 main;
        ControllerScript controller = Form1.controllerScript;
        int editState = 0;
        List<RadioButton> buttons;

        public int EditState { get => editState; set => editState = value; }

        public EditWindow(Form1 form)
        {
            this.StartPosition = FormStartPosition.Manual;
            main = form;
            InitializeComponent();
            Create();
            this.SetDesktopLocation(main.Location.X - this.Width, main.Location.Y);
        }

        private void EditWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.editGridButton.Text = "Edit Grid";
            controller.editModeOn = false;
        }

        private void EditWindow_Shown(object sender, EventArgs e)
        {
            main.editGridButton.Text = "Editing";
            controller.editModeOn = true;
        }

        void Create()
        {
            buttons = new List<RadioButton>();
            int y = mainLabel.Location.Y + 25;
            for (int i = 0; i < controller.amountOfCellTypes; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = "State " + (i + 1).ToString();
                radioButton.Location = new System.Drawing.Point(5, y);
                radioButton.CheckedChanged += new System.EventHandler(radioButton_CheckChanged);
                buttons.Add(radioButton);
                this.Controls.Add(radioButton);
                y += 20;
            }
        }

        private void radioButton_CheckChanged(object sender, EventArgs e)
        {
            EditState = buttons.FindIndex(x => x.Checked);
        }
    }
}
