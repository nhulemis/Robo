using System;
using System.Collections.Generic;

namespace Robo.src
{
    abstract class Model
    {
        protected bool                  m_isBusy;
        List<Cell>                      m_pathPlanning;
        public abstract void OnUpdate();
        public abstract void OnInit();

    }
}
