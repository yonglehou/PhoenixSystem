﻿using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectMatchingFamily
    {
        void Init();
        IEnumerable<IAspect> ActiveAspectList { get; }
        IEnumerable<IAspect> EntireAspectList { get; }
        void NewEntity(IEntity e);
        void RemoveEntity(IEntity e);
        void ComponentAddedToEntity(IEntity e, string componentType);
        void ComponentRemovedFromEntity(IEntity e, string componentType);
        void CleanUp();
    }
}