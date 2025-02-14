﻿using Simulation.Engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Engine.tasks
{

    public class EatTask : ITask
    {
        //Form1 form = new Form1();
        public string Name => "خوردن غذا";
        public bool IsCompleted { get; private set; } = false;
        private int steps = 0;
        private ITask? _waitFor; // فیلد پشتیبان برای WaitFor

        public event Action<ITask>? OnCompleted;

        public ITask? WaitFor
        {
            get => _waitFor;
            set
            {
                _waitFor = value;
                IsWaited = _waitFor != null; // به‌روزرسانی IsWaited هنگام تغییر WaitFor
                if (WaitFor != null)
                {
                    WaitFor.OnCompleted += HandleWaitForCompleted;
                }
            }
        }
        public ITask? Waited { get; set; }
        public bool IsWaited { get; private set; } // فقط‌خوان و به‌صورت خودکار به‌روزرسانی می‌شود
        EdibleObject EdibleObject { get; set; } = new EdibleObject();
        public void ExecuteStep(LivingBeing being, World world)
        {

            //form.WriteLine($"🍽️ {being.Name} در حال خوردن غذا (مرحله {steps}).");
            steps++;
            if (being.EdibleObjects.Count > 0)
            {
                EdibleObject = being.EdibleObjects.First();
                if(EdibleObject != null)
                {
                    if (EdibleObject.Energy > 5)
                    {

                        being.Energy += 5;
                        EdibleObject.Energy -= 5;
                    }
                    else if (EdibleObject.Energy > 0)
                    {
                        being.Energy += EdibleObject.Energy;
                        being.EdibleObjects.Remove(EdibleObject);
                    }
                }
                else
                {
                    SearchTask searchTask = new SearchTask(new EdibleObject());
                    searchTask.Waited = this;
                    WaitFor = searchTask;
                    being.Tasks.Add(searchTask);
                }


            }
            else
            {
                SearchTask searchTask = new SearchTask(new EdibleObject());
                searchTask.Waited = this;
                WaitFor = searchTask;
                being.Tasks.Add(searchTask);
            }


            if (steps >= 3)
            {
                IsCompleted = true;
            }
        }

        public void ForceStop()
        {
            throw new NotImplementedException();
        }

        private void HandleWaitForCompleted(ITask completedTask)
        {
            Console.WriteLine($"تسک {Name} دیگر منتظر {completedTask.Name} نیست!");
            WaitFor = null; // خالی کردن پراپرتی WaitFor      // اجرای تسک بعدی
        }
    }
}
