using System.Collections.Generic;
using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    /// <summary>Runs the synchronous type-registration phase for feature collections.</summary>
    public static class StaticEcsFeatureRunner {
        /// <summary>Registers types for every feature that implements the type-registration contract.</summary>
        public static void RegisterTypes<TWorld>(IReadOnlyList<IStaticEcsFeature<TWorld>> features)
            where TWorld : struct, IWorldType {
            if (features == null) {
                return;
            }

            var types = World<TWorld>.Types();
            for (var i = 0; i < features.Count; i++) {
                var feature = features[i];
                if (feature is not IStaticEcsTypeFeature<TWorld> typeFeature) {
                    continue;
                }

                typeFeature.RegisterTypes(types);
            }
        }
    }
}
