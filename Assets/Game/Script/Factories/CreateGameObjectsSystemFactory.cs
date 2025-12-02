namespace Game.Script.Factories
{
    public class CreateGameObjectsSystemFactory
    {
        private PlacementGrid worldGrid;
        private GameResources gameResources;

        public CreateGameObjectsSystemFactory(PlacementGrid placementGrid, GameResources gameResources)
        {
            worldGrid = placementGrid;
            this.gameResources = gameResources;
        }

        public CreateGameObjectsSystem CreateProtoSystem()
        {
            return new CreateGameObjectsSystem(worldGrid,gameResources);
        }
    }
}