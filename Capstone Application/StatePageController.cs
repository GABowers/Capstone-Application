using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    class StatePageController
    {
        static ControllerScript controllerScript;
        static StatePageInfo statePageInfo;
        static StatePageController()
        {
            controllerScript = new ControllerScript();
        }
        public void SetInfo(StatePageInfo info)
        {
            statePageInfo = info;


        }

    }
}
