using NUnit.Framework;
using unigame.staticecs.Random;

namespace unigame.staticecs.Tests {
    public sealed class EcsRngTests {
        [Test]
        public void FromSeed_Same_Seed_Produces_Same_Sequence() {
            var a = EcsRng.FromSeed(42u);
            var b = EcsRng.FromSeed(42u);

            for (var i = 0; i < 100; i++) {
                Assert.AreEqual(a.Next(), b.Next());
            }
        }

        [Test]
        public void FromSeed_Different_Seeds_Diverge() {
            var a = EcsRng.FromSeed(1u);
            var b = EcsRng.FromSeed(2u);

            var sameCount = 0;
            for (var i = 0; i < 100; i++) {
                if (a.Next() == b.Next()) {
                    sameCount++;
                }
            }

            Assert.Less(sameCount, 5);
        }

        [Test]
        public void NextFloat_Stays_In_Unit_Range() {
            var rng = EcsRng.FromSeed(1234u);

            for (var i = 0; i < 1000; i++) {
                var v = rng.NextFloat();
                Assert.GreaterOrEqual(v, 0f);
                Assert.Less(v, 1f);
            }
        }

        [Test]
        public void NextFloat_Range_Stays_In_Bounds() {
            var rng = EcsRng.FromSeed(1234u);

            for (var i = 0; i < 1000; i++) {
                var v = rng.NextFloat(-5f, 7f);
                Assert.GreaterOrEqual(v, -5f);
                Assert.Less(v, 7f);
            }
        }

        [Test]
        public void NextInt_Range_Stays_In_Bounds() {
            var rng = EcsRng.FromSeed(1234u);

            for (var i = 0; i < 1000; i++) {
                var v = rng.NextInt(10, 20);
                Assert.GreaterOrEqual(v, 10);
                Assert.Less(v, 20);
            }
        }

        [Test]
        public void NextInt_Empty_Range_Returns_Min() {
            var rng = EcsRng.FromSeed(1u);

            Assert.AreEqual(5, rng.NextInt(5, 5));
            Assert.AreEqual(5, rng.NextInt(5, 4));
        }

        [Test]
        public void NextBool_Zero_Chance_Always_False() {
            var rng = EcsRng.FromSeed(1u);

            for (var i = 0; i < 100; i++) {
                Assert.IsFalse(rng.NextBool(0f));
            }
        }

        [Test]
        public void NextBool_One_Chance_Always_True() {
            var rng = EcsRng.FromSeed(1u);

            for (var i = 0; i < 100; i++) {
                Assert.IsTrue(rng.NextBool(1f));
            }
        }

        [Test]
        public void Reseed_Resets_State() {
            var rng = EcsRng.FromSeed(1u);
            for (var i = 0; i < 10; i++) {
                rng.Next();
            }

            rng.Reseed(1u);
            var fresh = EcsRng.FromSeed(1u);

            Assert.AreEqual(fresh.Next(), rng.Next());
        }

        [Test]
        public void Zero_Seed_Falls_Back_To_Constant() {
            var a = EcsRng.FromSeed(0u);
            var b = EcsRng.FromSeed(0u);

            Assert.AreEqual(a.Next(), b.Next());
            Assert.AreNotEqual(0u, a.State);
        }
    }
}
