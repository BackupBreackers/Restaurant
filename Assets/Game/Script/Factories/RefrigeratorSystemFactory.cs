namespace Game.Script.Factories
{
    public class RefrigeratorSystemFactory
    {
        PickableService _pickableService;
        
        public RefrigeratorSystemFactory(PickableService pickableService) => this._pickableService = pickableService;
        
        public RefrigeratorSystem CreateProtoSystem()
        {
            return new RefrigeratorSystem(_pickableService);
        }
    }
}