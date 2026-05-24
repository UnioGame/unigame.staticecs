using NUnit.Framework;
using unigame.staticecs.Time;

namespace unigame.staticecs.Tests {
    public sealed class EcsTimeTests {
        [Test]
        public void Default_Has_TimeScale_One() {
            var time = EcsTime.Default();

            Assert.AreEqual(1f, time.TimeScale);
            Assert.AreEqual(0f, time.Time);
            Assert.AreEqual(0f, time.DeltaTime);
            Assert.AreEqual(0, time.FrameCount);
        }

        [Test]
        public void Reset_Restores_Defaults() {
            var time = EcsTime.Default();
            time.Time = 10f;
            time.DeltaTime = 0.1f;
            time.FrameCount = 100;
            time.TimeScale = 0.5f;

            time.Reset();

            Assert.AreEqual(0f, time.Time);
            Assert.AreEqual(0f, time.DeltaTime);
            Assert.AreEqual(0, time.FrameCount);
            Assert.AreEqual(1f, time.TimeScale);
        }

        [Test]
        public void SetTimeScale_Clamps_Negative_To_Zero() {
            var time = EcsTime.Default();

            time.SetTimeScale(-1f);

            Assert.AreEqual(0f, time.TimeScale);
        }

        [Test]
        public void SetTimeScale_Accepts_Positive_Values() {
            var time = EcsTime.Default();

            time.SetTimeScale(2.5f);

            Assert.AreEqual(2.5f, time.TimeScale);
        }
    }
}
