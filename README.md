# UniGame Static ECS

Runtime infrastructure for UniGame projects built on top of
`com.felid-force-studios.static-ecs`.

This package contains shared primitives used by `unigame.staticecs.unity` and
`unigame.staticecs.features`. It does not own game-specific gameplay slices.

## Runtime Layout

| Area | Purpose |
| --- | --- |
| `Runtime/Features` | `StaticEcsFeature<TWorld>` base and feature composition contracts. |
| `Runtime/Systems` | Update-group markers and `StaticEcsSystemsBuilder<TWorld, TGroup>` helpers. |
| `Runtime/Time` | `EcsTime` resource plus update/fixed-update systems. `EcsTime.Now` is the scaled virtual-time accumulator used by cooldowns and timed effects. |
| `Runtime/Random` | Deterministic `EcsRng` utilities for tests and resource-backed random providers. |
| `Runtime/Modifiers` | Shared modifier back-reference infrastructure used by features such as characteristics and modification effects. |

## Feature Pattern

Runtime packages register types through explicit feature objects:

```csharp
public sealed class GameFeature<TWorld> : StaticEcsFeature<TWorld>
    where TWorld : struct, IWorldType {
    public override void RegisterTypes(World<TWorld>.TypeRegistrar types) {
        new EcsTimeFeature<TWorld>(registerFixed: false).RegisterTypes(types);
    }
}
```

System registration is separate from type registration. Slices that own systems
implement `IStaticEcsSystemsFeature<TWorld, TGroup>` and are added to a
`StaticEcsSystemsBuilder<TWorld, TGroup>`.

## Tests

EditMode tests live in `Tests/Editor` and compile into
`unigame.staticecs.tests`.

For Unity Test Runner discovery in this project:

- `GameClient/Packages/manifest.json` includes `com.unigame.staticecs` in
  `testables`.
- `Tests/Editor/unigame.staticecs.tests.asmdef` references
  `optionalUnityReferences: ["TestAssemblies"]`.
- Test classes are marked with `[TestFixture]`.

The current core test coverage includes `EcsTime` and deterministic `EcsRng`.
