using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class EventDispatcher
    {
        private HashSet<IEventListener> listeners;
        private Dictionary<IEventListener, HashSet<string>> listenerEvents;

        public EventDispatcher()
        {
            listeners = new HashSet<IEventListener>();
            listenerEvents = new Dictionary<IEventListener, HashSet<string>>();
        }

        public void AddEventListener(IEventListener listener, string eventType)
        {
            
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
                listenerEvents.Add(listener, new HashSet<string>());
            }

            listenerEvents[listener].Add(eventType);

        }

        public void RemoveEventListener(IEventListener listener, string eventType)
        {
            if (!listeners.Contains(listener))
                return;

            listenerEvents[listener].Remove(eventType);
        }

        public void DispatchEvent(Event e)
        {
            foreach (IEventListener el in listeners)
                if (listenerEvents[el].Contains(e.EventType))
                    el.HandleEvent(e);
        }

        public bool HasEventListener(IEventListener listener, string eventType)
        {
            if (!listeners.Contains(listener))
                return false;

            return listenerEvents[listener].Contains(eventType);
        }
    }

    public interface IEventListener
    {
        void HandleEvent(Event e);
    }

    public class Event
    {
        public string EventType { get; private set; }

        public EventDispatcher Source { get; private set; }

        public Event(string eventType, EventDispatcher src)
        {
            EventType = eventType;
            Source = src;
        }
    }
}
