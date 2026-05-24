namespace unigame.staticecs.Modifiers {
    public static class ModifierFlagCache<TWorld, TStat> {
        private static ModifierRegistry _registry;
        private static ulong _flag;

        public static ulong EnsureRegistered(ModifierRegistry registry, ulong flag, ModifierSourceCleanup cleanup) {
            if (!ReferenceEquals(_registry, registry) || _flag == 0) {
                _registry = registry;
                _flag = flag;
                registry.Register(flag, cleanup);
            }

            return _flag;
        }

        public static bool TryGetFlag(ModifierRegistry registry, out ulong flag) {
            if (ReferenceEquals(_registry, registry) && _flag != 0) {
                flag = _flag;
                return true;
            }

            flag = 0;
            return false;
        }

        public static void Reset() {
            _registry = null;
            _flag = 0;
        }
    }
}
