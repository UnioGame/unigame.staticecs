# AGENTS — unigame.staticecs (base)

## Слой и зависимости

- Базовый пакет: чистый Static ECS, без Unity- и проектных зависимостей.
- НЕ зависит от `unigame.staticecs.unity` и не знает о дефолтном мире `Main`.
- Используется как `unigame.staticecs.unity`, так и `unigame.staticecs.features`.

## World-default aliases — НЕ применяется здесь

Соглашение [world-default-aliases](../../../../docs/knowledge/static-ecs/conventions/world-default-aliases.md) о Main-default формах действует **только** в слоях, которые имеют доступ к `Main` (`unigame.staticecs.unity` и `unigame.staticecs.features`).

В этом пакете generic-on-TWorld API остаётся generic. Не добавляйте сюда `using unigame.staticecs.unity` ради сокращений — это сломает направление зависимостей.

## Что живёт здесь

- Generic-инфраструктура для модификаторов: [`ModifierRegistry`](Runtime/Modifiers/ModifierRegistry.cs), [`ModifierFlagCache<TWorld, TStat>`](Runtime/Modifiers/ModifierIndexCache.cs).
- Контракты ресурсов / систем / фич, не привязанные к Unity.
