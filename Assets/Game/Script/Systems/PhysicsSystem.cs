using Leopotam.EcsProto;

public class PhysicsSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private PhysicsAspect _physicsAspect;
    private ProtoIt _iterator;
    
    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _physicsAspect = (PhysicsAspect)world.Aspect(typeof(PhysicsAspect));
        
        _iterator = new(new[] { typeof(PositionComponent), typeof(VelocityComponent), typeof(SpeedComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        
    }

    public void Destroy()
    {
        _physicsAspect =  null;
        _iterator = null;
    }
}