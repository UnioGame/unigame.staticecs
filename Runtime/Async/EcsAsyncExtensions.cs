using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs.Async {
    public static class EcsAsyncExtensions {
        public static async UniTask<bool> WaitForComponent<TWorld, TComponent>(
            EntityGID gid,
            CancellationToken cancellationToken = default)
            where TWorld : struct, IWorldType
            where TComponent : struct, IComponent {
            while (true) {
                if (cancellationToken.IsCancellationRequested) {
                    return false;
                }

                if (!gid.TryUnpack<TWorld>(out var entity, out var status)) {
                    if (status == GIDStatus.NotActual) {
                        return false;
                    }

                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken)
                        .SuppressCancellationThrow();
                    continue;
                }

                if (entity.Has<TComponent>()) {
                    return true;
                }

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken)
                    .SuppressCancellationThrow();
            }
        }

        public static async UniTask<bool> WaitUntil(
            Func<bool> predicate,
            CancellationToken cancellationToken = default) {
            if (predicate == null) {
                return false;
            }

            while (!predicate()) {
                if (cancellationToken.IsCancellationRequested) {
                    return false;
                }

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken)
                    .SuppressCancellationThrow();
            }

            return true;
        }

        public static async UniTask DelayFrames(
            int count,
            CancellationToken cancellationToken = default) {
            if (count <= 0) {
                return;
            }

            for (var i = 0; i < count; i++) {
                if (cancellationToken.IsCancellationRequested) {
                    return;
                }

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken)
                    .SuppressCancellationThrow();
            }
        }
    }
}
