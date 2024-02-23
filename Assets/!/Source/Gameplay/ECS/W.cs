using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Gameplay.ECS
{
    public static class W
    {
        public static bool Has(Entity entity)
        {
            return World.Default.TryGetEntity(entity.ID, out _);
        }

        public static bool Has(EntityId entityId)
        {
            return World.Default.TryGetEntity(entityId, out _);
        }

        public static Entity Get(EntityId entityId, out bool exists)
        {
            exists = World.Default.TryGetEntity(entityId, out Entity result);
            return result;
        }

        public static bool TryGetEntity(GameObject obj, out Entity entity)
        {
            entity = null;
            if (EntityProvider.map.TryGetValue(obj.GetInstanceID(), out EntityProvider.MapItem mapItem))
            {
                entity = mapItem.entity;
                return true;
            }

            return false;
        }

        public static Entity GetSafe(EntityId entityId)
        {
            Entity result = Get(entityId, out bool exists);

            if (!exists)
            {
                throw new Exception("Entity doesn't exist!");
            }

            return result;
        }
    }
}