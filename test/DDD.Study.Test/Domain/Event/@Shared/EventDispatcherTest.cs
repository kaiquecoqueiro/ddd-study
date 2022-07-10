using DDD.Study.Test.Mocks;
using FluentAssertions;
using src.Domain.Event.Shared;
using Xunit;

namespace DDD.Study.Test.Domain.Event.@Shared
{
    public class EventDispatcherTest
    {
        readonly EventDispatcher _dispatcher = new();
        readonly MockEventHandler _handler = new();

        [Fact]
        public void Register_ShouldRegisterAnEvent()
        {
            _dispatcher.Register("MockEvent", _handler);

            _dispatcher.GetEventHandlers().Should().ContainKey("MockEvent");
            _dispatcher.GetEventHandlers()["MockEvent"].Should().HaveCount(1);
            _dispatcher.GetEventHandlers()["MockEvent"][0].Should().Be(_handler);
        }

        [Fact]
        public void Unregister_ShouldUnregisterAndEvent()
        {
            _dispatcher.Register("MockEvent", _handler);
            _dispatcher.GetEventHandlers()["MockEvent"][0].Should().Be(_handler);

            _dispatcher.Unregister("MockEvent", _handler);

            _dispatcher.GetEventHandlers()["MockEvent"].Should().BeNullOrEmpty();
        }
    }
}