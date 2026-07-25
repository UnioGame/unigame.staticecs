namespace UniGame.StaticEcs.Random
{
    using Cysharp.Threading.Tasks;
    using FFS.Libraries.StaticEcs;
    using UniGame.Core.Runtime;

    /// <summary>Registers a world-scoped random number generator resource.</summary>
    public class EcsRngFeature<TWorld> :
        StaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Whether startup uses the explicit seed.</summary>
        public bool useFixedSeed;

        /// <summary>Seed used when fixed seeding is enabled.</summary>
        public uint seed = 1;

        /// <inheritdoc />
        public override UniTask InitializeAsync(ILifeTime lifeTime)
        {
            if (World<TWorld>.HasResource<EcsRng>())
            {
                return UniTask.CompletedTask;
            }

            if (World<TWorld>.HasResource<EcsRngConfig>())
            {
                var configured = World<TWorld>.GetResource<EcsRngConfig>();
                var configuredRng = configured.UseFixedSeed
                    ? EcsRng.FromSeed(configured.Seed)
                    : EcsRng.FromCurrentTime();

                World<TWorld>.SetResource(configuredRng);
                return UniTask.CompletedTask;
            }

            var config = new EcsRngConfig
            {
                UseFixedSeed = useFixedSeed,
                Seed = seed,
            };

            var rng = useFixedSeed
                ? EcsRng.FromSeed(seed)
                : EcsRng.FromCurrentTime();

            World<TWorld>.SetResource(config);
            World<TWorld>.SetResource(rng);
            return UniTask.CompletedTask;
        }
    }

    /// <summary>Controls deterministic or time-derived ECS random initialization.</summary>
    public sealed class EcsRngConfig : IResource
    {
        /// <summary>Whether startup uses the explicit seed.</summary>
        public bool UseFixedSeed;

        /// <summary>Seed used when fixed seeding is enabled.</summary>
        public uint Seed = 1;
    }
}
