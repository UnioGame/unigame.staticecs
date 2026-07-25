# UniGame Static ECS

## Capabilities

This package provides world-generic feature contracts, modifier registries,
test-world helpers, and shared resource contracts. It uses UniTask and the
UniCore `ILifeTime` contract, but does not call Unity APIs directly or reference
`Main`, `IContext`, or gameplay families.

See the [shared Static ECS documentation](../../../docs/knowledge/static-ecs/)
for world, query, component, event, resource, and systems concepts.

## Usage

A programmatic feature has one asynchronous initialization entry point. It
publishes resources directly and keeps systems parameterless:

```csharp
public sealed class CombatFeature<TWorld> : StaticEcsFeature<TWorld>
    where TWorld : struct, IWorldType
{
    public override UniTask InitializeAsync(ILifeTime lifeTime)
    {
        var configuration = new CombatConfiguration();

        World<TWorld>.SetResource(configuration);
        lifeTime.AddDispose(CreateSubscription());
        return UniTask.CompletedTask;
    }
}
```

Ordinary ECS types are discovered by the Unity bootstrap from enabled feature
assemblies:

| Marker | Automatic registration |
|---|---|
| `IComponent` | component |
| `ITag` | tag |
| `IEvent` | event |
| `ILinkType` | `Link<T>` |
| `ILinksType` | `Links<T>` |
| `IMultiComponent` | `Multi<T>` |
| `IEntityType` | entity type |

Open generic definitions and required closed generic constructions are not
discovered. Neither are resources, systems, system groups, feature assets,
converters, abstract types, unmarked classes, or assemblies without an enabled
feature.

## Configuration

ECS Resources are the realtime dependency mechanism. Features use
`World<TWorld>.SetResource(...)`; systems and operations use
`World<TWorld>.GetResource<T>()`. The base package does not wrap resource access
in an installation or validation context.

Base APIs stay generic on `TWorld`. Main-default aliases belong to packages that
reference `unigame.staticecs.unity`.

Tests should create an isolated `StaticEcsTestWorld<TWorld>`, register only the
required closed generic types, initialize features with the test world's lifetime,
and dispose the test world after the test. Use
`EcsService` only for bootstrap-owned lifecycle behavior.
