﻿using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Entity
{
    public interface IEntityAspectManager
    {        
        IEnumerable<TAspectType> GetAspectList<TAspectType>() where TAspectType : IAspect, new();
        IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new();
        void RegisterEntity(IEntity e);
        void UnregisterEntity(IEntity e);
        void ComponentRemovedFromEntity(IEntity e, IComponent component);
        void ComponentAddedToEntity(IEntity e, IComponent component);
        void ReleaseAspectList<TAspectType>();              
    }
}
