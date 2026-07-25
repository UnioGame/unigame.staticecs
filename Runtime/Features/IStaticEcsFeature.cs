namespace UniGame.StaticEcs
{
    using Cysharp.Threading.Tasks;
    using FFS.Libraries.StaticEcs;
    using UniGame.Core.Runtime;

    /// <summary>Initializes one composable Static ECS feature for a world.</summary>
    public interface IStaticEcsFeature<TWorld>
        where TWorld : struct, IWorldType
    {
        /// <summary>Gets the diagnostic name of the feature.</summary>
        string FeatureName { get; }

        /// <summary>Publishes resources and adds systems before world initialization.</summary>
        UniTask InitializeAsync(ILifeTime lifeTime);
    }
}
