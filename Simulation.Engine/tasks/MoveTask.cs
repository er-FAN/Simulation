﻿using Simulation.Engine.events;
using Simulation.Engine.models;
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
        public Location Destination;
        private ITask? _waitFor; // فیلد پشتیبان برای WaitFor

        public event EventHandler<TaskCompletedEventArgs> OnCompleted;

        public ITask? Waited { get; set; }


        public ITask? WaitFor
        {
            get => _waitFor;
            set
            {
                _waitFor = value;
                IsWaited = _waitFor != null; // به‌روزرسانی IsWaited هنگام تغییر WaitFor
                if (WaitFor != null)
                {
                    WaitFor.OnCompleted += WaitForCompleted;
                }
            }
        }

        public bool IsWaited { get; private set; } // فقط‌خوان و به‌صورت خودکار به‌روزرسانی می‌شود

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
                OnCompleted?.Invoke(this,new TaskCompletedEventArgs());
                //if(Waited != null)
                //{
                //    Waited.WaitFor = null;
                //}
                
                //form.WriteLine($"✅ {being.Name} به مقصد خود رسید.");
            }
        }

        public void ForceStop()
        {
            throw new NotImplementedException();
        }

        private void HandleWaitForCompleted(ITask completedTask)
        {
            WaitFor = null;
        }

        public void TaskCompleted(object? sender, TaskCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void WaitForCompleted(object? sender, TaskCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
