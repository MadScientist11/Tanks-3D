using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay
{
    public struct CreateFloatingTextRequest : IRequestData
    {
        public Vector3 WorldPosition;
        public string Message;
    }
}