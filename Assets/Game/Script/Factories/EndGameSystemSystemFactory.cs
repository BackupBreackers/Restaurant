using Game.Script.Infrastructure;

namespace Game.Script.Factories
{
    public class EndGameSystemSystemFactory
    {
        private UIController _uiController;

        public EndGameSystemSystemFactory(UIController uiController)
            => _uiController = uiController;

        public EndGameSystem CreateProtoSystem() => new(_uiController);
    }
}