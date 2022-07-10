using DDD.Study.Test.Mocks;
using FluentAssertions;
using src.Domain.Event.Shared;
using Xunit;

namespace DDD.Study.Test.Domain.Event.@Shared
{
    public class EventDispatcherTest
    {
        [Fact]
        public void Register_ShouldRegisterAnEvent()
        {
            var dispatcher = new EventDispatcher();
            var handler = new MockEventHandler();

            dispatcher.Register("MockEvent", handler);

            dispatcher.GetEventHandlers().Should().ContainKey("MockEvent");
            dispatcher.GetEventHandlers()["MockEvent"].Should().HaveCount(1);
            dispatcher.GetEventHandlers()["MockEvent"][0].Should().Be(handler);
        }
    }
}