using FFS.Libraries.StaticEcs;
using NUnit.Framework;

namespace UniGame.StaticEcs.Tests
{
    [TestFixture]
    public sealed class NativeEventTests
    {
        [SetUp]
        public void SetUp()
        {
            World<TestEventWorld>.Create(WorldConfig.Default());
            World<TestEventWorld>.Types().Event<TestEvent>();
            World<TestEventWorld>.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (World<TestEventWorld>.Status != WorldStatus.NotCreated)
                World<TestEventWorld>.Destroy();
        }

        [Test]
        public void SendWithoutReceiverReturnsFalse()
        {
            Assert.IsFalse(World<TestEventWorld>.SendEvent(new TestEvent { Value = 1 }));
        }

        [Test]
        public void ReceiversConsumeTheSameEventIndependently()
        {
            var first = World<TestEventWorld>.RegisterEventReceiver<TestEvent>();
            var second = World<TestEventWorld>.RegisterEventReceiver<TestEvent>();
            try
            {
                Assert.IsTrue(World<TestEventWorld>.SendEvent(new TestEvent { Value = 7 }));
                Assert.AreEqual(7, ReadSingle(ref first));
                Assert.AreEqual(7, ReadSingle(ref second));
            }
            finally
            {
                World<TestEventWorld>.DeleteEventReceiver(ref second);
                World<TestEventWorld>.DeleteEventReceiver(ref first);
            }
        }

        [Test]
        public void DeletedLastReceiverStopsEventRetention()
        {
            var receiver = World<TestEventWorld>.RegisterEventReceiver<TestEvent>();
            World<TestEventWorld>.DeleteEventReceiver(ref receiver);

            Assert.IsFalse(World<TestEventWorld>.SendEvent(new TestEvent { Value = 2 }));
        }

        private static int ReadSingle(ref EventReceiver<TestEventWorld, TestEvent> receiver)
        {
            var count = 0;
            var value = 0;
            foreach (var item in receiver)
            {
                count++;
                value = item.Value.Value;
            }

            Assert.AreEqual(1, count);
            return value;
        }

        private struct TestEventWorld : IWorldType { }

        private struct TestEvent : IEvent
        {
            public int Value;
        }
    }
}
