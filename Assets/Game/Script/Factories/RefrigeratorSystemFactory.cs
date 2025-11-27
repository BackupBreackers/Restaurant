namespace Game.Script.Factories
{
    public class RefrigeratorSystemFactory
    {
        GameResources _gameResources;
        
        public RefrigeratorSystemFactory(GameResources resources) => this._gameResources = resources;
        
        public RefrigeratorSystem CreateProtoSystem()
        {
            return new RefrigeratorSystem(_gameResources.VisualLink.meat);
        }
    }
}