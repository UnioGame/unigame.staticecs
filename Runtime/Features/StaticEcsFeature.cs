using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    /// <summary>Base class for features that own Static ECS type registration.</summary>
    public abstract class StaticEcsFeature<TWorld> : IStaticEcsTypeFeature<TWorld>
        where TWorld : struct, IWorldType {
        /// <inheritdoc />
        public virtual string FeatureName => GetType().Name;

        /// <inheritdoc />
        public abstract void RegisterTypes(World<TWorld>.TypeRegistrar types);
    }
}
