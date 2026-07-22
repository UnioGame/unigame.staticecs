using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs.Random
{
    /// <summary>Registers a world-scoped random number generator resource.</summary>
    public class EcsRngFeature<TWorld> : StaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Uses the serialized seed instead of a time-derived seed.</summary>
        public bool useFixedSeed;

        /// <summary>Deterministic seed used when <see cref="useFixedSeed"/> is enabled.</summary>
        public uint seed = 1;

        /// <summary>Creates a time-seeded random feature.</summary>
        public EcsRngFeature()
        {
            useFixedSeed = false;
        }

        /// <summary>Creates a deterministically seeded random feature.</summary>
        public EcsRngFeature(uint seed)
        {
            this.seed = seed;
            useFixedSeed = true;
        }

        /// <inheritdoc />
        public override void RegisterTypes(World<TWorld>.TypeRegistrar types)
        {
            if (World<TWorld>.HasResource<EcsRng>())
            {
                return;
            }

            var rng = useFixedSeed ? EcsRng.FromSeed(seed) : EcsRng.FromCurrentTime();
            World<TWorld>.SetResource(rng);
        }
    }
}
