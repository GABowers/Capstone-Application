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
        bool autoReset = false;
        bool countReset = false;
        bool iterationReset = false;
        int iterationResetValue = -1;
        List<int> countResetValues = new List<int>();
        List<int> countSaveValues = new List<int>();
        List<int> imageSaveValues = new List<int>();

        public bool CountSave { get => countSave; set => countSave = value; }
        public bool ImageSave { get => imageSave; set => imageSave = value; }
        public bool AutoReset { get => autoReset; set => autoReset = value; }
        public bool CountReset { get => countReset; set => countReset = value; }
        public bool IterationReset { get => iterationReset; set => iterationReset = value; }
        public int IterationResetValue { get => iterationResetValue; set => iterationResetValue = value; }
        public List<int> CountResetValues { get => countResetValues; set => countResetValues = value; }
        public List<int> CountSaveValues { get => countSaveValues; set => countSaveValues = value; }
        public List<int> ImageSaveValues { get => imageSaveValues; set => imageSaveValues = value; }

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
