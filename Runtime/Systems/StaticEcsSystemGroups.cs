using FFS.Libraries.StaticEcs;

namespace UniGame.StaticEcs {
    public struct StaticEcsUpdateSystems : ISystemsType { }

    public struct StaticEcsFixedUpdateSystems : ISystemsType { }

    public struct StaticEcsLateUpdateSystems : ISystemsType { }

    public struct StaticEcsCleanupSystems : ISystemsType { }
}
