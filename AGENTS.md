# AGENTS — unigame.staticecs

## Layer

- Keep this package pure Static ECS: no Unity, UniTask, project, or `unigame.staticecs.unity` dependency.
- Do not reference the default `Main` world here. Generic-on-world APIs remain generic.
- Put only reusable contracts, builders, resources, and registries in this package.

## Features and systems

- `IStaticEcsFeature<TWorld>` owns identity; `IStaticEcsTypeFeature<TWorld>` owns synchronous type registration.
- A concrete `StaticEcsFeature<TWorld>` must implement `RegisterTypes`; do not add empty virtual lifecycle hooks.
- `StaticEcsSystemsBuilder` must accept both struct and class systems. Do not introduce `class` or `new()` constraints.
- Implement only the `ISystem` lifecycle methods a system uses. Reflection discovers omitted methods and skips them.

## Documentation

- Public documentation is English and follows Capabilities / Usage / Configuration.
- Every public type and member has at least a one-line XML summary.
- Link to repository/upstream Static ECS documentation instead of duplicating it.
