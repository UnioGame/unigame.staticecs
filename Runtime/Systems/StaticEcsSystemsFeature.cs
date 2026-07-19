using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    public abstract class StaticEcsSystemsFeature<TWorld, TSystemsType> :
        StaticEcsFeature<TWorld>,
        IStaticEcsSystemsFeature<TWorld, TSystemsType>
        where TWorld : struct, IWorldType
        where TSystemsType : struct, ISystemsType {
        public abstract void RegisterSystems(StaticEcsSystemsBuilder<TWorld, TSystemsType> systems);
    }
}
