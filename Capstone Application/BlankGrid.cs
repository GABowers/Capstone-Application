﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class BlankGrid
    {
        public bool containsAgent = false;
        public AgentController agent;

        // Use this for initialization
        void Start()
        {

        }

        public bool ContainsAgent
        {
            get { return containsAgent; }
            set { containsAgent = value; }
        }

        //public AgentController AgentController
        //{
        //    get { return agent; }
        //    set { agent = value; }
        //}

        public void AddAgent()
        {
            agent = new AgentController();
        }
    }
}
