using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    public interface IStaticEcsSystemsFeature<TWorld, TSystemsType> : IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
        where TSystemsType : struct, ISystemsType {
        void RegisterSystems(StaticEcsSystemsBuilder<TWorld, TSystemsType> systems);
    }
}
