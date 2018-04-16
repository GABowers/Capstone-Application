using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class Settings
    {
        bool countSave = false;
        bool imageSave = false;
        bool pathSave = false;
        bool autoReset = false;
        bool autoPause = false;
        bool countReset = false;
        bool countPause = false;
        bool iterationReset = false;
        bool iterationPause = false;
        bool runMax = false;
        int iterationResetValue = -1;
        int iterationPauseValue = -1;
        int runMaxValue = -1;
        List<int> countResetValues = new List<int>();
        List<int> countPauseValues = new List<int>();
        List<int> countSaveValues = new List<int>();
        List<int> imageSaveValues = new List<int>();
        List<int> pathSaveValues = new List<int>();
        List<Tuple<int, Tuple<int, int>>> startingLocations = new List<Tuple<int, Tuple<int, int>>>();

        public bool CountSave { get => countSave; set => countSave = value; }
        public bool ImageSave { get => imageSave; set => imageSave = value; }
        public bool AutoReset { get => autoReset; set => autoReset = value; }
        public bool CountReset { get => countReset; set => countReset = value; }
        public bool IterationReset { get => iterationReset; set => iterationReset = value; }
        public int IterationResetValue { get => iterationResetValue; set => iterationResetValue = value; }
        public List<int> CountResetValues { get => countResetValues; set => countResetValues = value; }
        public List<int> CountSaveValues { get => countSaveValues; set => countSaveValues = value; }
        public List<int> ImageSaveValues { get => imageSaveValues; set => imageSaveValues = value; }
        public bool RunMax { get => runMax; set => runMax = value; }
        public int RunMaxValue { get => runMaxValue; set => runMaxValue = value; }
        public List<int> CountPauseValues { get => countPauseValues; set => countPauseValues = value; }
        public bool IterationPause { get => iterationPause; set => iterationPause = value; }
        public bool CountPause { get => countPause; set => countPause = value; }
        public bool AutoPause { get => autoPause; set => autoPause = value; }
        public int IterationPauseValue { get => iterationPauseValue; set => iterationPauseValue = value; }
        public List<int> PathSaveValues { get => pathSaveValues; set => pathSaveValues = value; }
        public bool PathSave { get => pathSave; set => pathSave = value; }
        public List<Tuple<int, Tuple<int, int>>> StartingLocations { get => startingLocations; set => startingLocations = value; }

        public void CheckSettings(Form1 form)
        {
            if(CountSave)
            {

            }
            if(ImageSave)
            {

            }

            if (AutoReset)
            {
                if(CountReset)
                {

                }
                if(IterationReset)
                {

                }
            }
        }
    }
}
