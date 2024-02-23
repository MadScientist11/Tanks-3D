using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Scellecs.Morpeh.Helpers.OneFrame;

namespace Gameplay.ECS.Healthcare
{
    public class CollisionInitSystem : SimpleSystem<InitCollisionsRequest>, IUpdateSystem
    {
        public override void OnAwake()
        {
            World.RegisterOneFrame<ContactEvent>();
            base.OnAwake();
        }

        protected override void Process(Entity entity, ref InitCollisionsRequest _, in float deltaTime)
        {
            GameObjectComponent gameObject = entity.GetComponent<GameObjectComponent>();
            CollisionDetector collisionDetector = gameObject.Value.AddComponent<CollisionDetector>();
            collisionDetector.Init(World, entity);
            entity.RemoveComponent<InitCollisionsRequest>();
        }
    }
}