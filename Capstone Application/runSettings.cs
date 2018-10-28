﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class RunSettings
    {
        string dataPath;
        List<int> dataIncs;
        string imagePath;
        List<int> imageIncs;
        string pathsPath;
        List<int> pathsIncs;
        List<int> resetIterations;
        List<int> resetCounts;
        List<int> pauseIterations;
        List<int> pauseCounts;
        bool autoReset;
        bool autoPause;
        bool saveCounts;
        bool saveTrans;
        bool saveIndex;

        public string DataPath { get => dataPath; set => dataPath = value; }
        public List<int> DataIncs { get => dataIncs; set => dataIncs = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public List<int> ImageIncs { get => imageIncs; set => imageIncs = value; }
        public string PathsPath { get => pathsPath; set => pathsPath = value; }
        public List<int> PathsIncs { get => pathsIncs; set => pathsIncs = value; }
        public List<int> ResetIterations { get => resetIterations; set => resetIterations = value; }
        public List<int> ResetCounts { get => resetCounts; set => resetCounts = value; }
        public List<int> PauseIterations { get => pauseIterations; set => pauseIterations = value; }
        public List<int> PauseCounts { get => pauseCounts; set => pauseCounts = value; }
        public bool AutoReset { get => autoReset; set => autoReset = value; }
        public bool AutoPause { get => autoPause; set => autoPause = value; }
        public bool SaveCounts { get => saveCounts; set => saveCounts = value; }
        public bool SaveTrans { get => saveTrans; set => saveTrans = value; }
        public bool SaveIndex { get => saveIndex; set => saveIndex = value; }

        public RunSettings(int agents)
        {
            DataIncs = new List<int>();
            ImageIncs = new List<int>();
            PathsIncs = new List<int>();
            ResetIterations = new List<int>();
            PauseIterations = new List<int>();
            ResetCounts = new List<int>();
            PauseCounts = new List<int>();
            for (int i = 0; i < agents; i++)
            {
                ResetCounts.Add(-1);
                PauseCounts.Add(-1);
            }
        }
    }
}