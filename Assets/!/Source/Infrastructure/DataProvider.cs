using Gameplay;
using Gameplay.ECS;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure
{
    public class DataProvider : IDataProvider
    {
        public AvatarDefinition PlayerConfig { get; private set; }
        public WeaponConfiguration ProjectileConfig { get; private set; }

        public void Initialize()
        {
            PlayerConfig = Resources.Load<AvatarDefinition>("PlayerConfiguration");
            ProjectileConfig = Resources.Load<WeaponConfiguration>("ProjectileConfig");
        }
    }

    public interface IDataProvider : IInitializable
    {
        AvatarDefinition PlayerConfig { get; }
        WeaponConfiguration ProjectileConfig { get; }
    }
}