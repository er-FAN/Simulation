﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Engine.events
{
    public interface IHasEventListener
    {
        public List<IEventListener> EventListeners { get; set; }
    }
}
