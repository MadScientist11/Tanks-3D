using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay
{
    public class AnimateFloatingTextSystem : SimpleLateSystem<FloatingTextComponent>
    {
        protected override void Process(Entity entity, ref FloatingTextComponent floatingText, in float deltaTime)
        {
            floatingText.CurrentDuration -= deltaTime;

            if (floatingText.CurrentDuration <= 0)
            {
                entity.RemoveComponent<FloatingTextComponent>();
                entity.AddComponent<DestroySelfRequest>();
                return;
            }

            ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();

            float t = (floatingText.AnimDuration - floatingText.CurrentDuration) / floatingText.AnimDuration;
            transform.Value.position = Vector3.Lerp(floatingText.InitialPosition, floatingText.TargetPositon, t);
            transform.Value.localScale = Vector3.Lerp(floatingText.InitialScale, floatingText.TargetScale, t);
        }
    }
}