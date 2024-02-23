using Gameplay.ECS.Healthcare;
using Infrastructure;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct CreateHealthBarRequest : IComponent
    {
    }
    public class CreateHealthBarSystem : SimpleSystem<CreateHealthBarRequest, HealthComponent>, IUpdateSystem
    {
        private GameObject _healthbarPrefab;
        public static GameObject _healthbarCanvas;
        
        private IGameFactory _gameFactory;

        public CreateHealthBarSystem(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public override void OnAwake()
        {
            _healthbarPrefab = Resources.Load<GameObject>("Healthbar");
            _healthbarCanvas = Resources.Load<GameObject>("HealthbarCanvas");
            _healthbarCanvas = _gameFactory.CreatePrefabInjected(_healthbarCanvas);
            base.OnAwake();
        }

        protected override void Process(Entity entity, ref CreateHealthBarRequest request, ref HealthComponent health, in float deltaTime)
        {
            GameObject healthBar = _gameFactory.CreatePrefabInjected(_healthbarPrefab, _healthbarCanvas.transform);

            W.TryGetEntity(healthBar, out Entity healthBarEntity);
            ref HealthBarUiComponent barUi = ref healthBarEntity.GetComponent<HealthBarUiComponent>();
            barUi.OwnerId = entity.ID;

            entity.RemoveComponent<CreateHealthBarRequest>();
        }
    }
}