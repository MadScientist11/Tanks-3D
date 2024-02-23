using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public struct DetectionRangeComponent : IComponent
    {
        public int SqrRange => Range* Range;
        
        public int Range;
    }
}