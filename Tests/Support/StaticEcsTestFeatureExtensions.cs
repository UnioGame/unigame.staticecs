namespace UniGame.StaticEcs
{
    using FFS.Libraries.StaticEcs;
    using Tests;

    /// <summary>Explicit test-only helpers for isolated feature composition.</summary>
    public static class StaticEcsTestFeatureExtensions
    {
        /// <summary>Initializes one feature in its already-created isolated test world.</summary>
        public static void InitializeForTest<TWorld>(
            this IStaticEcsFeature<TWorld> feature,
            StaticEcsTestWorld<TWorld> world)
            where TWorld : struct, IWorldType
        {
            feature.InitializeAsync(world.LifeTime)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Scans the owning assembly for concrete ECS types and initializes one isolated feature.
        /// Closed generic types remain the responsibility of the test.
        /// </summary>
        public static void InstallResourcesAndRegisterTypesForTest<TWorld>(
            this IStaticEcsFeature<TWorld> feature,
            StaticEcsTestWorld<TWorld> world)
            where TWorld : struct, IWorldType
        {
            world.Types.RegisterAll(feature.GetType().Assembly);
            feature.InitializeForTest(world);
        }

        /// <summary>Initializes one feature without implicit type scanning.</summary>
        public static void InstallTestResources<TWorld>(
            this IStaticEcsFeature<TWorld> feature,
            StaticEcsTestWorld<TWorld> world)
            where TWorld : struct, IWorldType
        {
            feature.InitializeForTest(world);
        }
    }
}
