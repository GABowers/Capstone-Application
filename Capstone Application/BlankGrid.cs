using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class BlankGrid
    {
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost { get { return GCost + HCost; } }
        public BlankGrid PathParent { get; set; }
        public CA Parent { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public AgentController Agent { get; private set; }

        // Use this for initialization
        //void Start()
        //{

        //}

        public BlankGrid(int x, int y, CA parent)
        {
            X = x;
            Y = y;
            this.Parent = parent;
        }

        public BlankGrid(int x, int y, AgentController agent, CA Parent):this(x, y, Parent)
        {
            AddAgent(agent);
        }

        public bool ContainsAgent
        {
            get
            {
                return Agent != null;
            }
        }

        //public AgentController AgentController
        //{
        //    get { return agent; }
        //    set { agent = value; }
        //}

        public void AddAgent(AgentController prevAgent)
        {
            Agent = prevAgent;
            Agent.Update(this);
        }

        public void RemoveAgent()
        {
            Agent = null;
        }

        //public PathfindingNode CreateSubNode()
        //{
        //    return new PathfindingNode(X, Y, this, !ContainsAgent);
        //}
    }
}
