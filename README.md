# UniGame Static ECS

Pure Static ECS infrastructure shared by the Unity integration and gameplay feature packages. This package has no Unity or project dependency and does not define a default world.

## Capabilities

- Feature identity and synchronous type-registration contracts.
- Generic system-group markers and `StaticEcsSystemsBuilder<TWorld, TSystemsType>`.
- Runtime data such as `EcsTime`, `EcsRng`, and modifier registries.
- Struct and class system registration without constructor or reference-type constraints.

## Usage

```csharp
public sealed class InventoryFeature<TWorld> : StaticEcsFeature<TWorld>
    where TWorld : struct, IWorldType
{
    public override void RegisterTypes(World<TWorld>.TypeRegistrar types)
    {
        types.Component<InventoryComponent>()
             .Event<InventoryChangedEvent>();
    }
}
```

Use `StaticEcsSystemsBuilder<TWorld, TSystemsType>` from a Unity-facing feature to add either struct or class systems. Lifecycle methods that a system does not need must be omitted.

## Configuration

This package contains no ScriptableObject configuration and no `Main` aliases. Async feature lifecycle, feature assets, the default world, and PlayerLoop integration belong to `unigame.staticecs.unity`.

Type registration may combine explicit calls with upstream `RegisterAll`. Closed generic types, resources, handlers, and custom registries still require explicit registration. See the repository Static ECS knowledge pages for upstream API details.
