using System.Collections.Generic;
using Gameplay.ECS;
using Gameplay.ECS.UnityLayer;
using Infrastructure;
using Scellecs.Morpeh;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public enum FloatingTextStyle
    {
        Damage,
        
    }
    public class CreateFloatingTextSystem : ILateSystem
    {
        public World World { get; set; }
        
        private TextMeshProUGUI _textPrefab;
        private Canvas _floatingTextCanvas;
        
        private Request<CreateFloatingTextRequest> _createRequest;
        private FloatingTextComponent _floatingText;
        
        private readonly IGameFactory _gameFactory;

        public CreateFloatingTextSystem(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public  void OnAwake()
        {
            _createRequest = World.GetRequest<CreateFloatingTextRequest>();
            _floatingTextCanvas = Resources.Load<Canvas>("UI/Canvas");
            _textPrefab = Resources.Load<TextMeshProUGUI>("UI/Floating Text");
            
            _floatingTextCanvas = _gameFactory.CreatePrefabInjected(_floatingTextCanvas);
        }

        public  void OnUpdate(float deltaTime)
        {
            foreach (CreateFloatingTextRequest createRequest in _createRequest.Consume())
            {
                TextMeshProUGUI text = _gameFactory.CreatePrefabInjected(_textPrefab, _floatingTextCanvas.transform);

                W.TryGetEntity(text.gameObject, out Entity entity);
                ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
                text.GetComponent<RectTransform>().anchoredPosition = _floatingTextCanvas.ConvertWorldToCanvasPoint(createRequest.WorldPosition);

                text.text = createRequest.Message;
                text.color = Color.red;
                entity.SetComponent(new FloatingTextComponent
                {
                    AnimDuration = 1,
                    CurrentDuration = 1,
                    TargetPositon = transform.Value.position + Vector3.up * 100,
                    TargetScale = transform.Value.localScale *= 0.5f,
                    InitialPosition = transform.Value.position,
                    InitialScale = transform.Value.localScale
                });

            }
        }

        public void Dispose()
        {
        }
    }
}