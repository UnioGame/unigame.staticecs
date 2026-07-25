namespace UniGame.StaticEcs.Tests
{
    using System;
    using FFS.Libraries.StaticEcs;
    using UniGame.Core.Runtime;
    using UniGame.Runtime.DataFlow;

    /// <summary>Owns the lifecycle of one isolated Static ECS test world.</summary>
    public sealed class StaticEcsTestWorld<TWorld> : IDisposable
        where TWorld : struct, IWorldType
    {
        private readonly LifeTime _lifeTime;

        /// <summary>Creates an isolated world in the Created state.</summary>
        public StaticEcsTestWorld()
            : this(WorldConfig.Default())
        {
        }

        /// <summary>Creates an isolated world with the supplied Static ECS configuration.</summary>
        public StaticEcsTestWorld(WorldConfig config)
        {
            if (World<TWorld>.Status != WorldStatus.NotCreated)
            {
                throw new InvalidOperationException(
                    $"Test world `{typeof(TWorld).Name}` is already active.");
            }

            World<TWorld>.Create(config);
            _lifeTime = new LifeTime();
        }

        /// <summary>Gets the lifetime owned by this isolated test world.</summary>
        public ILifeTime LifeTime => _lifeTime;

        /// <summary>Terminates the test-world lifetime before explicit systems teardown.</summary>
        public void TerminateLifeTime()
        {
            _lifeTime.Terminate();
        }

        /// <summary>Gets the registrar used before world initialization.</summary>
        public World<TWorld>.TypeRegistrar Types => World<TWorld>.Types();

        /// <summary>Initializes the world after explicit resource and type setup.</summary>
        public void Initialize(uint baseEntitiesCapacity = 512)
        {
            World<TWorld>.Initialize(baseEntitiesCapacity);
        }

        /// <summary>Destroys the world from either Created or Initialized state.</summary>
        public void Dispose()
        {
            try
            {
                TerminateLifeTime();
            }
            finally
            {
                if (World<TWorld>.Status == WorldStatus.Created)
                {
                    World<TWorld>.Initialize();
                    World<TWorld>.Destroy(withHooks: false);
                }
                else if (World<TWorld>.Status == WorldStatus.Initialized)
                {
                    World<TWorld>.Destroy();
                }
            }
        }
    }
}
