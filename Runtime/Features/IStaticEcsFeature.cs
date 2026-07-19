using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    public interface IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType {
        string FeatureName { get; }

        bool IsEnabled { get; }

        void RegisterTypes(World<TWorld>.TypeRegistrar types);
    }
}
