using Unity.Entities;

namespace Voxell.GPUVectorGraphics
{
    using static VectorGraphicsWorld;
    using static VectorGraphicsRenderer;

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class VectorGraphicsRendererSystem : SystemBase, System.IDisposable
    {
        protected override void OnStartRunning()
        {
            EntityManager manager = this.EntityManager;
            RenderCompCaches.Add(CreateCache<RectComp>(ref manager, MaterialMap["RectUnlit"], Primitive.Quad));
            RenderCompCaches.Add(CreateCache<EllipseComp>(ref manager, MaterialMap["EllipseUnlit"], Primitive.Quad));
            RenderCompCaches.Add(CreateCache<PolygonComp>(ref manager, MaterialMap["PolygonUnlit"], Primitive.Quad));
        }

        protected override void OnUpdate()
        {
            // render all entities with component that implements IRenderComp
            for (int c = 0, count = RenderCompCaches.Count; c < count; c++)
            {
                RenderCompCache cache = RenderCompCaches[c];
                cache.RenderDelegate(ref cache);
            }
        }

        protected override void OnDestroy()
        {
            this.Dispose();
        }

        public void Dispose() {}
    }
}