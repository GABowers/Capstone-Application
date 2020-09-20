using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class BlankGrid
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public AgentController Agent { get; private set; }

        // Use this for initialization
        //void Start()
        //{

        //}

        public BlankGrid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public BlankGrid(int x, int y, AgentController agent):this(x, y)
        {
            AddAgent(agent);
        }

        public bool ContainsAgent { get; private set; }

        //public AgentController AgentController
        //{
        //    get { return agent; }
        //    set { agent = value; }
        //}

        public void AddAgent(AgentController prevAgent)
        {
            Agent = prevAgent;
            Agent.Update(this);
            ContainsAgent = true;
        }

        public void RemoveAgent()
        {
            Agent = null;
            ContainsAgent = false;
        }
    }
}
