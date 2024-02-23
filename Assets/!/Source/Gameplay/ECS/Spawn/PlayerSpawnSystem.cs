using Gameplay.ECS.UnityLayer;
using Infrastructure;
using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public class PlayerSpawnSystem : IInitializer
    {
        public World World { get; set; }

        private readonly IGameFactory _gameFactory;

        public PlayerSpawnSystem(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        //TODO: World Initialization
        public void OnAwake()
        {
            Entity playerSpawnPoint = World.Filter.With<PlayerSpawnPointMarker>().Build().First();
            ref TransformComponent transform = ref playerSpawnPoint.GetComponent<TransformComponent>();

            _gameFactory.CreateAvatar(transform.Value.position, transform.Value.rotation);
        }

        public void Dispose()
        {
        }
    }
}