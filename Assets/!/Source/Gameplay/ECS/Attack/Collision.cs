using System;
using Gameplay.ECS.Healthcare;
using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public static class Collision
    {
        public static void OnContact(in ContactEvent contactEvent, Action<ContactEvent, Entity, Entity> onContact)
        {
            Entity source = W.GetSafe(contactEvent.CollisionSourceId);
            Entity collidee = W.GetSafe(contactEvent.CollideeId);

            onContact?.Invoke(contactEvent, source, collidee);
        }
    }
}