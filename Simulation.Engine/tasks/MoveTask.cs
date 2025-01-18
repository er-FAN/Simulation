﻿using Simulation.Engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Engine.tasks
{
    public class MoveTask : ITask
    {
        public string Name => "حرکت";
        //Form1 form=new Form1();
        public bool IsCompleted { get; private set; } = false;
        private Location Destination;

        public MoveTask(Location destination)
        {
            Destination = destination;
        }

        public void ExecuteStep(LivingBeing being, World world)
        {
            //form.WriteLine($"🚶‍♂️ {being.Name} در حال حرکت به سمت {Destination} است.");

            if (being.Location.X < Destination.X) being.Location.X++;
            else if (being.Location.X > Destination.X) being.Location.X--;

            if (being.Location.Y < Destination.Y) being.Location.Y++;
            else if (being.Location.Y > Destination.Y) being.Location.Y--;

            if (being.Location.X == Destination.X && being.Location.Y == Destination.Y)
            {
                IsCompleted = true;
                //form.WriteLine($"✅ {being.Name} به مقصد خود رسید.");
            }
        }

        public void ForceStop()
        {
            throw new NotImplementedException();
        }
    }
}
