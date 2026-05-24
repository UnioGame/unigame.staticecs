using System;
using FFS.Libraries.StaticEcs;

namespace unigame.staticecs.Modifiers {
    public delegate void ModifierSourceCleanup(EntityGID source, EntityGID target);

    public sealed class ModifierRegistry : IResource {
        public const int MaxSlots = 64;

        private readonly ModifierSourceCleanup[] _slots = new ModifierSourceCleanup[MaxSlots];

        public void Register(ulong flag, ModifierSourceCleanup cleanup) {
            if (cleanup == null) {
                throw new ArgumentNullException(nameof(cleanup));
            }

            if (!IsSingleBit(flag)) {
                throw new ArgumentException("Flag must be a single power of two.", nameof(flag));
            }

            _slots[Log2(flag)] = cleanup;
        }

        public bool IsRegistered(ulong flag) {
            if (!IsSingleBit(flag)) {
                return false;
            }

            return _slots[Log2(flag)] != null;
        }

        public void Invoke(ulong flag, EntityGID source, EntityGID target) {
            if (!IsSingleBit(flag)) {
                return;
            }

            _slots[Log2(flag)]?.Invoke(source, target);
        }

        public void InvokeMask(ulong mask, EntityGID source, EntityGID target) {
            while (mask != 0) {
                var bit = mask & (0UL - mask);
                _slots[Log2(bit)]?.Invoke(source, target);
                mask ^= bit;
            }
        }

        public void Reset() {
            Array.Clear(_slots, 0, _slots.Length);
        }

        private static bool IsSingleBit(ulong value) {
            return value != 0 && (value & (value - 1)) == 0;
        }

        private static int Log2(ulong singleBit) {
            // singleBit is guaranteed to be a power of two by callers.
            var n = 0;
            if ((singleBit & 0xFFFFFFFF00000000UL) != 0) { singleBit >>= 32; n += 32; }
            if ((singleBit & 0x00000000FFFF0000UL) != 0) { singleBit >>= 16; n += 16; }
            if ((singleBit & 0x000000000000FF00UL) != 0) { singleBit >>= 8;  n += 8; }
            if ((singleBit & 0x00000000000000F0UL) != 0) { singleBit >>= 4;  n += 4; }
            if ((singleBit & 0x000000000000000CUL) != 0) { singleBit >>= 2;  n += 2; }
            if ((singleBit & 0x0000000000000002UL) != 0) { n += 1; }
            return n;
        }
    }
}
