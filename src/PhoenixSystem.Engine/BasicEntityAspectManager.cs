﻿using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public sealed class BasicEntityAspectManager : BaseEntityAspectManager
    {
        private readonly Dictionary<string, IEntityAspectMatchingFamily> _aspectFamilies = new Dictionary<string, IEntityAspectMatchingFamily>();

        public override void ComponentAddedToEntity(IEntity e, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentAddedToEntity(e, component.GetType().Name);
            }
        }

        public override void ComponentRemovedFromEntity(IEntity e, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentRemovedFromEntity(e, component.GetType().Name);
            }
        }

        public override IEntityAspectMatchingFamily CreateAspectMatchingFamily<AspectType>()
        {
            return new BasicAspectMatchingFamily<AspectType>();
        }

        public override IEnumerable<IAspect> GetAspectList<AspectType>()
        {
            var aspectType = typeof (AspectType).Name;

            if (_aspectFamilies.ContainsKey(aspectType))
            {
                return _aspectFamilies[aspectType].ActiveAspectList;
            }

            var aspectFamily = CreateAspectMatchingFamily<AspectType>();

            aspectFamily.Init();
            _aspectFamilies[aspectType] = aspectFamily;

            foreach (var kvp in GameManager.Entities)
            {
                aspectFamily.NewEntity(kvp.Value);
            }

            return aspectFamily.ActiveAspectList;
        }

        public override IEnumerable<IAspect> GetUnfilteredAspectList<AspectType>()
        {
            var type = typeof (AspectType).Name;

            if (!_aspectFamilies.ContainsKey(type))
                throw new ApplicationException("Unable to retrieve unfiltered aspect list until AspectType is registered using GetNodeList");

            return _aspectFamilies[type].EntireAspectList;
        }

        public override void RegisterEntity(IEntity e)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.NewEntity(e);
            }
        }

        public override void ReleaseAspectList<AspectType>()
        {
            var type = typeof (AspectType).Name;

            if (!_aspectFamilies.ContainsKey(type))
                throw new ApplicationException("Aspect Family does not exist for type: " + type);

            var aspectFamily = _aspectFamilies[typeof (AspectType).Name];
            aspectFamily.CleanUp();
            _aspectFamilies.Remove(type);
        }

        public override void UnregisterEntity(IEntity e)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.RemoveEntity(e);
            }
        }
    }
}