using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs.Time {
    public sealed class EcsTimeFeature<TWorld> :
        StaticEcsFeature<TWorld>,
        IStaticEcsSystemsFeature<TWorld, StaticEcsUpdateSystems>,
        IStaticEcsSystemsFeature<TWorld, StaticEcsFixedUpdateSystems>
        where TWorld : struct, IWorldType {
        public const short DefaultUpdateOrder = short.MinValue;

        private readonly short _updateOrder;
        private readonly short _fixedOrder;
        private readonly bool _registerFixed;

        public EcsTimeFeature(
            short updateOrder = DefaultUpdateOrder,
            short fixedOrder = DefaultUpdateOrder,
            bool registerFixed = true) {
            _updateOrder = updateOrder;
            _fixedOrder = fixedOrder;
            _registerFixed = registerFixed;
        }

        public override void RegisterTypes(World<TWorld>.TypeRegistrar types) {
            if (!World<TWorld>.HasResource<EcsTime>()) {
                World<TWorld>.SetResource(EcsTime.Default());
            }
        }

        public void RegisterSystems(StaticEcsSystemsBuilder<TWorld, StaticEcsUpdateSystems> systems) {
            systems.Add(new EcsTimeUpdateSystem<TWorld>(), _updateOrder);
        }

        public void RegisterSystems(StaticEcsSystemsBuilder<TWorld, StaticEcsFixedUpdateSystems> systems) {
            if (!_registerFixed) {
                return;
            }

            systems.Add(new EcsTimeFixedUpdateSystem<TWorld>(), _fixedOrder);
        }
    }
}
