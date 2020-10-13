using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class RunSettings
    {
        bool fresh = true;
        string dataPath;
        string imagePath;
        string pathsPath;
        string templatePath;
        bool autoReset;
        bool autoPause;
        bool saveCounts;
        bool saveTrans;
        bool saveIndex;
        bool savePaths;
        bool saveImage;
        bool metaStore;
        string histPath;

        public string DataPath { get => dataPath; set => dataPath = value; }
        public List<int> DataIncs { get; set; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public List<int> ImageIncs { get; set; }
        public string PathsPath { get => pathsPath; set => pathsPath = value; }
        public List<int> PathsIncs { get; set; }
        public List<int> ResetIterations { get; set; }
        public List<int> ResetCounts { get; set; }
        public List<int> PauseIterations { get; set; }
        public List<int> PauseCounts { get; set; }
        public List<int> PauseRuns { get; set; }
        public bool AutoReset { get => autoReset; set => autoReset = value; }
        public bool AutoPause { get => autoPause; set => autoPause = value; }
        public bool SaveCounts { get => saveCounts; set => saveCounts = value; }
        public bool SaveTrans { get => saveTrans; set => saveTrans = value; }
        public bool SaveIndex { get => saveIndex; set => saveIndex = value; }
        public List<int> TemplateIncs { get; set; }
        public string TemplatePath { get => templatePath; set => templatePath = value; }
        public bool Fresh { get => fresh; set => fresh = value; }
        public bool MetaStore { get => metaStore; set => metaStore = value; }
        public bool SavePaths { get => savePaths; set => savePaths = value; }
        public bool SaveImage { get => saveImage; set => saveImage = value; }
        public List<int> HistIncs { get; set; }
        public string HistPath { get => histPath; set => histPath = value; }
        public List<int> HistRunIncs { get; set; }

        public RunSettings(int agents)
        {
            DataIncs = new List<int>();
            ImageIncs = new List<int>();
            PathsIncs = new List<int>();
            TemplateIncs = new List<int>();
            ResetIterations = new List<int>();
            PauseIterations = new List<int>();
            ResetCounts = Enumerable.Repeat(0, agents).ToList();
            PauseCounts = Enumerable.Repeat(0, agents).ToList();
            HistIncs = new List<int>();
            HistRunIncs = new List<int>();
            PauseRuns = new List<int>();
            Fresh = true;
        }
    }
}
