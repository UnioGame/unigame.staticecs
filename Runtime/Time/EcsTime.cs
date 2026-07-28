using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs.Time {
    public struct EcsTime : IResource {
        public float DeltaTime;
        public float UnscaledDeltaTime;
        public float FixedDeltaTime;
        public float Time;
        public float UnscaledTime;
        public float FixedTime;
        public float Now;
        public float TimeScale;
        public int FrameCount;

        public static EcsTime Default() {
            return new EcsTime {
                DeltaTime = 0f,
                UnscaledDeltaTime = 0f,
                FixedDeltaTime = 0f,
                Time = 0f,
                UnscaledTime = 0f,
                FixedTime = 0f,
                Now = 0f,
                TimeScale = 1f,
                FrameCount = 0
            };
        }

        public void Reset() {
            DeltaTime = 0f;
            UnscaledDeltaTime = 0f;
            FixedDeltaTime = 0f;
            Time = 0f;
            UnscaledTime = 0f;
            FixedTime = 0f;
            Now = 0f;
            TimeScale = 1f;
            FrameCount = 0;
        }

        public void SetTimeScale(float value) {
            if (value < 0f)
                value = 0f;

            TimeScale = value;
        }
    }
}
