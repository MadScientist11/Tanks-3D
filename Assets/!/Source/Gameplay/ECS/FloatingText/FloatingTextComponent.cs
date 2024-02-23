using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public struct FloatingTextComponent : IComponent
    {
        public float CurrentDuration;
        
        public float AnimDuration;

        public Vector3 InitialPosition;
        public Vector3 InitialScale;
        
        public Vector3 TargetPositon;
        public Vector3 TargetScale;
    }
}