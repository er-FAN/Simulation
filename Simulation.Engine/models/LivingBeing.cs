﻿using Simulation.Engine.events;
using Simulation.Engine.tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simulation.Engine.models
{
    public class LivingBeing : PhysicalObject
    {
        public string Name { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public int Age { get; set; }
        public int VisualRange { get; set; } = 500;
        public bool IsAlive { get; set; }

        private int _Energy;
        public int Energy
        {
            get => _Energy;
            set
            {
                int addedEnergy = value - _Energy;
                _Energy = value;
                EnergyChanged.Invoke(this, new EnergyChangedEventArgs(addedEnergy, _Energy));
            }
        }

        private void LivingBeing_EnergyChanged(object? sender, EnergyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private int _Sleep;
        public int Sleep
        {
            get => _Sleep;
            set
            {
                int addedSleep = value - _Sleep;
                _Sleep = value;
                SleepChanged.Invoke(this, new SleepChangedEventArgs(addedSleep, _Sleep));
            }
        }
        public bool IsSleep { get; set; }
        public List<ITask> Tasks { get; set; } = new List<ITask>();
        public List<EdibleObject> EdibleObjects { get; set; } = new List<EdibleObject>();
        public EventManager EventManager { get; set; } = new EventManager();
        public event EventHandler<EnergyChangedEventArgs> EnergyChanged;
        public event EventHandler<SleepChangedEventArgs> SleepChanged;

        public LivingBeing(string name, Location location)
        {
            SetProperties(name, location);

            EventManager.RegisterEvent("Hungry", () => Tasks.Add(new EatTask(this)));
            EventManager.RegisterEvent("Tired", () => Tasks.Add(new RestTask(this)));
            EventManager.RegisterEvent("Reproduce", () => Tasks.Add(new ReproduceTask(this)));
            EventManager.RegisterEvent("Die", () => Tasks.Add(new DieTask(this)));
            EnergyChanged += LivingBeing_EnergyChanged;
            SleepChanged += LivingBeing_SleepChanged;
        }

        private void SetProperties(string name, Location location)
        {
            Guid id = Guid.NewGuid();
            Name = name;
            Age = 0;
            IsAlive = true;
            Energy = 100;
            Sleep = 0;
            Location = location;
        }

        private void LivingBeing_SleepChanged(object? sender, SleepChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Update(World world)
        {
            if (!IsAlive)
            {
                return;
            }

            Age++;
            Sleep++;
            Energy -= IsSleep ? 1 : 2;


            CheckConditions(world);
            ExecuteTasks(world);

        }

        private void ExecuteTasks(World world)
        {
            if (Tasks.Count > 0)
            {
                for (int i = Tasks.Count - 1; i >= 0; i--)
                {
                    ITask task = Tasks[i];

                    task.ExecuteStep(world);
                    RemoveTaskFromBeingTaskListIfCompleted(i, task);
                }
            }
        }

        private void RemoveTaskFromBeingTaskListIfCompleted(int i, ITask task)
        {
            if (task.IsCompleted)
            {
                Tasks.RemoveAt(i);
            }
        }

        private void CheckConditions(World world)
        {
            //if (Sleep > 20) EventManager.TriggerEvent("Tired");
            if (!IsSleep)
            {
                if (Energy < 30) EventManager.TriggerEvent("Hungry");
                //if (Age > 20 && Age % 50 == 0) EventManager.TriggerEvent("Reproduce");
            }
            if (Age > 10000000 || Energy == 0)
            {
                //EventManager.TriggerEvent("Die");
            }

        }

        public override string ToString()
        {
            return $"{Name} | سن: {Age} | انرژی: {Energy} | مکان: {Location} | زنده: {IsAlive}";
        }
    }
}
