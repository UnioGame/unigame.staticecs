using FFS.Libraries.StaticEcs;
using UnityTime = UnityEngine.Time;

namespace unigame.staticecs.Time {
    public sealed class EcsTimeUpdateSystem<TWorld> : ISystem
        where TWorld : struct, IWorldType {
        public void Update() {
            ref var time = ref World<TWorld>.GetResource<EcsTime>();
            var unscaledDelta = UnityTime.unscaledDeltaTime;
            var scaledDelta = unscaledDelta * time.TimeScale;

            time.DeltaTime = scaledDelta;
            time.UnscaledDeltaTime = unscaledDelta;
            time.Time += scaledDelta;
            time.UnscaledTime += unscaledDelta;
            time.FrameCount++;
        }
    }
}
