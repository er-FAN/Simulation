﻿using Simulation.Engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Engine.tasks
{
    public interface ITask
    {
        string Name { get; }
        bool IsCompleted { get; }
        void ExecuteStep(LivingBeing being, World world);
        void ForceStop();
    }
}
