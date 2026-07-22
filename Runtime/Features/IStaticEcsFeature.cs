using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs
{
    /// <summary>Identifies a composable Static ECS feature for a world.</summary>
    public interface IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Gets the diagnostic name of the feature.</summary>
        string FeatureName { get; }
    }

    /// <summary>Registers the ECS types and resources owned by a feature.</summary>
    public interface IStaticEcsTypeFeature<TWorld> : IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Registers types while the world is in the Created state.</summary>
        void RegisterTypes(World<TWorld>.TypeRegistrar types);
    }

    /// <summary>Releases feature-owned runtime state before its world is destroyed.</summary>
    public interface IStaticEcsDestroyFeature<TWorld> : IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Releases runtime state while the feature world is still available.</summary>
        void Destroy();
    }
}
