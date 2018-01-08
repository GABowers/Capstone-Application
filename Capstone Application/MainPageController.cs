using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    class MainPageController
    {
        static ControllerScript controllerScript;
        static MainPageInfo mainPageInfo;

        static MainPageController()
        {
            mainPageInfo = new MainPageInfo();
            controllerScript = new ControllerScript();
        }

        //public void SetInfo(MainPageInfo info, ComboBox gridType, ComboBox neighborType, TextBox stateNumberBox, TextBox gridSizeHori, TextBox gridSizeVert)
        //{
        //    mainPageInfo = info;

        //    gridType.SelectedIndex = (int)mainPageInfo.gridType;
        //    neighborType.SelectedIndex = (int)mainPageInfo.nType;
        //    //May have errors here - previously it was an int? so it checked if a value had been assigned.
        //    stateNumberBox.Text = mainPageInfo.numStates.ToString();
        //    gridSizeHori.Text = mainPageInfo.gridWidth.ToString();
        //    gridSizeVert.Text = mainPageInfo.gridHeight.ToString();
        //}

        //public void NextButton(GridType gridType, NType nType, int numStates, int gridWidth, int gridHeight)
        //{
        //    UpdateValues(gridType, nType, numStates, gridWidth, gridHeight);
        //    controllerScript.MainPageNext();
        //}

        //public void UpdateValues(GridType newgridType, NType newnType, int newnumStates, int newgridWidth, int newgridHeight)
        //{
        //    mainPageInfo.gridType = newgridType;
        //    mainPageInfo.nType = newnType;
        //    mainPageInfo.numStates = newnumStates;
        //    mainPageInfo.gridWidth = newgridWidth;
        //    mainPageInfo.gridHeight = newgridHeight;
        //}
    }
}
