using System.Collections.Generic;

namespace src.Domain.Event.@Shared
{
    public class EventDispatcher : IEventDispatcher
    {
        private Dictionary<string, IList<IEventHandler<IEvent>>> _eventHandlers = new();


        public Dictionary<string, IList<IEventHandler<IEvent>>> GetEventHandlers() => _eventHandlers;

        public void Notify(IEvent @event)
        {
            var eventName = @event.GetType().Name;
            if (_eventHandlers.ContainsKey(eventName))
            {
                var handlers = _eventHandlers[eventName];
                foreach (var handler in handlers)
                    handler.Handle(@event);
            }
        }

        public void Register(string eventName, IEventHandler<IEvent> eventHandler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
                _eventHandlers[eventName] = new List<IEventHandler<IEvent>>();

            _eventHandlers[eventName].Add(eventHandler);
        }

        public void Unregister(string eventName, IEventHandler<IEvent> eventHandler)
        {
            if (_eventHandlers.ContainsKey(eventName) && _eventHandlers[eventName].Contains(eventHandler))
                _eventHandlers[eventName].Remove(eventHandler);
        }

        public void UnregisterAll() => _eventHandlers.Clear();
    }
}