using System.Collections.Generic;
using System.Linq;
using Gameplay;
using Gameplay.ECS.Healthcare;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Repos
{
    public class EnemyRepository : IInitializable
    {
        private Dictionary<EnemyTypeId, EnemyDefinition> _enemyDefinition;

        public void Initialize()
        {
            _enemyDefinition = Resources.LoadAll<EnemyDefinition>("Enemies/")
                .ToDictionary(def => def.EnemyTypeId, def => def);
        }

        public EnemyDefinition GetById(EnemyTypeId enemyTypeId)
        {
            return _enemyDefinition[enemyTypeId];
        }
    }
}