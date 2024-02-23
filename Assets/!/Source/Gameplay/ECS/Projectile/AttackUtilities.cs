using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    public static class AttackUtilities
    {
        public static bool CheckForCooldown(ref Entity entity)
        {
            ref AttackRateComponent attackRate = ref entity.GetComponent<AttackRateComponent>(out bool hasAttackRate);

            bool cooldownElapsed = Time.time - attackRate.LastAttackTime >= attackRate.Rate;
            return cooldownElapsed;
        }  
        
        public static bool CheckForTarget(ref Entity entity)
        {
            ref TargetHolderComponent targetHolder = ref entity.GetComponent<TargetHolderComponent>(out bool hasTarget);

            return hasTarget;
        } 
        
        public static void AttackHappened(ref Entity entity)
        {
            ref AttackRateComponent attackRate = ref entity.GetComponent<AttackRateComponent>(out bool hasAttackRate);

            if (hasAttackRate)
            {
                attackRate.LastAttackTime = Time.time;
            }
        }
    }
}