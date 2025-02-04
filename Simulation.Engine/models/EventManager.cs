﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Engine.models
{
    public class EventManager
    {
        private Dictionary<string, Action> events = new Dictionary<string, Action>();
        //public List<LivingBeing> SimulationEntities { get; set; } = new List<LivingBeing>();
        public void RegisterEvent(string eventName, Action action)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = action;
            }
        }

        public void TriggerEvent(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                //form.WriteLine($"🔔 رویداد '{eventName}' فعال شد.");
                events[eventName]?.Invoke();
            }
        }
    }
}
