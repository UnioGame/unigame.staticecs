namespace UniGame.StaticEcs
{
    using Cysharp.Threading.Tasks;
    using FFS.Libraries.StaticEcs;
    using UniGame.Core.Runtime;

    /// <summary>Base class for programmatic Static ECS feature composition.</summary>
    public abstract class StaticEcsFeature<TWorld> : IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <inheritdoc />
        public virtual string FeatureName => GetType().Name;

        /// <inheritdoc />
        public abstract UniTask InitializeAsync(ILifeTime lifeTime);
    }
}
