using Leopotam.EcsProto;

public class PhysicsSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private PhysicsAspect _physicsAspect;
    private ProtoIt _iterator;
    
    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _physicsAspect = (PhysicsAspect)world.Aspect(typeof(PhysicsAspect));
        
        _iterator = new(new[] { typeof(Rigidbody2DComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        foreach (var e in _iterator)
        {
            ref Rigidbody2DComponent rigidbody2DComponent = ref _physicsAspect.Rigidbody2DPool.Get(e);
            //rigidbody2DComponent.Rigidbody2D.linearVelocity *= 0.95f;
            //positionComponent.Position.x += 1;
        }
    }

    public void Destroy()
    {
        _physicsAspect =  null;
        _iterator = null;
    }
}