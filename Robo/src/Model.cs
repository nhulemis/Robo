using System;
using System.Collections.Generic;
using System.Drawing;

namespace Robo.src
{
    abstract class Model
    {
        protected bool                  m_isBusy;
        List<Cell>                      m_pathPlanning;



        public abstract void OnUpdate();
        public abstract void OnRender(Graphics grs);
        public abstract void OnInit();

    }
}
