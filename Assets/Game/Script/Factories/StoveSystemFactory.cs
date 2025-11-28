namespace Game.Script.Factories
{
    internal class StoveSystemFactory
    {
        private RecipeService _recipeService;
        
        public StoveSystemFactory(RecipeService recipeService) =>
            this._recipeService = recipeService;
        
        public StoveSystem CreateProtoSystem()
        {
            return new StoveSystem(_recipeService);
        }
    }
}