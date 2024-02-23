using Scellecs.Morpeh;

namespace Gameplay.ECS.Camera
{
    public class One<TSingle> where TSingle : struct, IComponent
    {
        public static One<TSingle> Get => _get ??= new();

        private static One<TSingle> _get;

        public bool Exists => World.Default.Filter.With<TSingle>().Build().FirstOrDefault() != null;
        public ref TSingle Value => ref For<TSingle>();

        public ref TComponent For<TComponent>() where TComponent : struct, IComponent
        {
            return ref World.Default.Filter.With<TSingle>().Build().First().GetComponent<TComponent>();
        }
    }
}