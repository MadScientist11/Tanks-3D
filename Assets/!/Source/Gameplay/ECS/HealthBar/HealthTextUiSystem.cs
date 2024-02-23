using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using TMPro;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct HealthBarUiComponent : IComponent
    {
        public EntityId OwnerId;
        public TextMeshProUGUI Text;
    }

    public class HealthTextUiSystem : SimpleSystem<HealthBarUiComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref HealthBarUiComponent healthBarUi, in float deltaTime)
        {
            Entity e = W.Get(healthBarUi.OwnerId, out bool exists);
            if (!exists)
            {
                entity.AddComponent<DestroySelfRequest>();
                return;
            }
            
            ref GameObjectComponent ownerGameObject = ref e.GetComponent<GameObjectComponent>();
            ref HealthComponent ownerHealth = ref e.GetComponent<HealthComponent>();


            Canvas healthbarCanvas = CreateHealthBarSystem._healthbarCanvas.GetComponent<Canvas>();
           healthBarUi.Text.rectTransform.anchoredPosition =
               healthbarCanvas.ConvertWorldToCanvasPoint(ownerGameObject.Value.transform.position + Vector3.up * 2);
       
            healthBarUi.Text.text = ownerHealth.Amount > 0 ? ownerHealth.Amount.ToString() : "";
        }
     
    }
    
   
}