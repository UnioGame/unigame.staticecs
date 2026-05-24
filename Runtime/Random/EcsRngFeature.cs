using FFS.Libraries.StaticEcs;

namespace unigame.staticecs.Random {
    public sealed class EcsRngFeature<TWorld> : StaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType {
        private readonly uint _seed;
        private readonly bool _useFixedSeed;

        public EcsRngFeature() {
            _useFixedSeed = false;
        }

        public EcsRngFeature(uint seed) {
            _seed = seed;
            _useFixedSeed = true;
        }

        public override void RegisterTypes(World<TWorld>.TypeRegistrar types) {
            if (World<TWorld>.HasResource<EcsRng>()) {
                return;
            }

            var rng = _useFixedSeed ? EcsRng.FromSeed(_seed) : EcsRng.FromCurrentTime();
            World<TWorld>.SetResource(rng);
        }
    }
}
