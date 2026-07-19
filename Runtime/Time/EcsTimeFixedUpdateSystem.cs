using FFS.Libraries.StaticEcs;
using UnityTime = UnityEngine.Time;

namespace UniGame.StaticEcs.Time {
    public sealed class EcsTimeFixedUpdateSystem<TWorld> : ISystem
        where TWorld : struct, IWorldType {
        public void Update() {
            ref var time = ref World<TWorld>.GetResource<EcsTime>();
            var unscaledFixedDelta = UnityTime.fixedUnscaledDeltaTime;
            var scaledFixedDelta = unscaledFixedDelta * time.TimeScale;

            time.FixedDeltaTime = scaledFixedDelta;
            time.FixedTime += scaledFixedDelta;
        }
    }
}
