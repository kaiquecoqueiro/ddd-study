using System.Collections.Generic;

namespace src.Domain.Event.@Shared
{
    public class EventDispatcher : IEventDispatcher
    {
        private Dictionary<string, IList<IEventHandler<IEvent>>> _eventHandlers;
        public Dictionary<string, IList<IEventHandler<IEvent>>> GetEventHandlers() => _eventHandlers;

        public void Notify(IEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Register(string eventName, IEventHandler<IEvent> eventHandler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
                _eventHandlers[eventName] = new List<IEventHandler<IEvent>>();

            _eventHandlers[eventName].Add(eventHandler);
        }

        public void Unregister(string eventName)
        {
            throw new System.NotImplementedException();
        }

        public void UnregisterAll()
        {
            throw new System.NotImplementedException();
        }
    }
}