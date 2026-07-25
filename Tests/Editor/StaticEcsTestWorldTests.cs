namespace UniGame.StaticEcs.Tests
{
    using Cysharp.Threading.Tasks;
    using FFS.Libraries.StaticEcs;
    using NUnit.Framework;
    using UniGame.Core.Runtime;

    [TestFixture]
    public sealed class StaticEcsTestWorldTests
    {
        [Test]
        public void FeatureReceivesLifetimeOwnedByIsolatedWorld()
        {
            var feature = new RecordingFeature();
            var world = new StaticEcsTestWorld<TestWorld>();

            feature.InitializeForTest(world);
            world.Initialize();

            Assert.AreSame(world.LifeTime, feature.LifeTime);
            Assert.IsFalse(feature.LifeTime.IsTerminated);

            world.Dispose();
            world.Dispose();

            Assert.IsTrue(feature.LifeTime.IsTerminated);
            Assert.AreEqual(1, feature.CleanupCount);
        }

        [Test]
        public void NestedFeatureReceivesSameWorldLifetime()
        {
            var child = new RecordingFeature();
            var feature = new CompositeFeature(child);
            using var world = new StaticEcsTestWorld<TestWorld>();

            feature.InitializeForTest(world);

            Assert.AreSame(world.LifeTime, feature.LifeTime);
            Assert.AreSame(world.LifeTime, child.LifeTime);
        }

        private struct TestWorld : IWorldType
        {
        }

        private sealed class RecordingFeature : StaticEcsFeature<TestWorld>
        {
            public ILifeTime LifeTime { get; private set; }

            public int CleanupCount { get; private set; }

            public override UniTask InitializeAsync(ILifeTime lifeTime)
            {
                LifeTime = lifeTime;
                lifeTime.AddCleanUpAction(this, static feature => feature.CleanupCount++);
                return UniTask.CompletedTask;
            }
        }

        private sealed class CompositeFeature : StaticEcsFeature<TestWorld>
        {
            private readonly RecordingFeature _child;

            public CompositeFeature(RecordingFeature child)
            {
                _child = child;
            }

            public ILifeTime LifeTime { get; private set; }

            public override UniTask InitializeAsync(ILifeTime lifeTime)
            {
                LifeTime = lifeTime;
                return _child.InitializeAsync(lifeTime);
            }
        }
    }
}
