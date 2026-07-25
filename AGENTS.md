# AGENTS — unigame.staticecs

## Layer

- Keep this package free of direct Unity API, project, `Main`, `IContext`, and
  `unigame.staticecs.unity` usage. UniTask and `unigame.unicore.runtime` are approved;
  UniCore supplies `ILifeTime` for feature initialization.
- Do not reference the default `Main` world here. Generic-on-world APIs remain generic.
- Put only reusable contracts, builders, resources, and registries in this package.

## Features and systems

- `IStaticEcsFeature<TWorld>` has one `InitializeAsync(ILifeTime)` lifecycle entry point.
- The supplied lifetime is the exact lifetime of the current world. Use its token only
  for cancellable operations and register initialization-owned cleanup on it.
- A concrete `StaticEcsFeature<TWorld>` publishes resources directly and adds only the systems it owns.
- Keep systems parameterless. Do not introduce `class` or `new()` constraints.
- Implement only the `ISystem` lifecycle methods a system uses. Reflection discovers omitted methods and skips them.
- Ordinary concrete ECS marker types are discovered by `RegisterAll` from enabled feature assemblies. Closed generic constructions use assembly registrars in Unity-facing packages.
- At registration boundaries, write every closed type and registry registration as a separate statement. Do not use fluent registration chains. Query construction follows its own formatting rules.
- Construct every resource in a named local before `SetResource`. Do not put constructors,
  object initializers, conditional expressions, or factory calls inside the registration call.

## Documentation

- Public documentation is English and follows Capabilities / Usage / Configuration.
- Every public type and member has at least a one-line XML summary.
- Link to repository/upstream Static ECS documentation instead of duplicating it.
