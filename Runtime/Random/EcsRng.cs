using System;
using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs.Random {
    public struct EcsRng : IResource {
        public uint State;

        public static EcsRng FromSeed(uint seed) {
            if (seed == 0u) {
                seed = 0x9E3779B9u;
            }

            return new EcsRng { State = seed };
        }

        public static EcsRng FromCurrentTime() {
            unchecked {
                return FromSeed((uint)DateTime.UtcNow.Ticks);
            }
        }

        public void Reseed(uint seed) {
            if (seed == 0u) {
                seed = 0x9E3779B9u;
            }

            State = seed;
        }

        public uint Next() {
            unchecked {
                var x = State;
                if (x == 0u) {
                    x = 0x9E3779B9u;
                }

                x ^= x << 13;
                x ^= x >> 17;
                x ^= x << 5;
                State = x;
                return x;
            }
        }

        public int NextInt(int minInclusive, int maxExclusive) {
            if (maxExclusive <= minInclusive) {
                return minInclusive;
            }

            var range = (uint)(maxExclusive - minInclusive);
            return minInclusive + (int)(Next() % range);
        }

        public float NextFloat() {
            const float scale = 1f / 4294967296f;
            return Next() * scale;
        }

        public float NextFloat(float min, float max) {
            if (max <= min) {
                return min;
            }

            return min + NextFloat() * (max - min);
        }

        public bool NextBool(float chance) {
            if (chance <= 0f) {
                return false;
            }

            if (chance >= 1f) {
                return true;
            }

            return NextFloat() < chance;
        }
    }
}
