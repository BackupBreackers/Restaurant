using System;
using Game.Script.DISystem;
using Leopotam.EcsProto;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Script.Infrastructure
{
    public class GameStateManager : IStartable, ITickable, IFixedTickable, IDisposable
    {
        private IProtoSystems _mainSystems;
        private IProtoSystems _physicsSystems;
        private InputService _inputService;
        
        private UIController _uiController;

        public GameStateManager(
            [Key(GameLifetimeScope.IProtoSystemsType.MainSystem)] IProtoSystems mainSystems,
            [Key(GameLifetimeScope.IProtoSystemsType.PhysicsSystem)] IProtoSystems physicsSystems,
            InputService inputService,
            UIController uiController)
        {
            _mainSystems = mainSystems;
            _physicsSystems = physicsSystems;
            _inputService = inputService;
            _uiController = uiController;
        }
        
        public bool IsPaused { get; private set; }
        
        
        public void Start()
        {
            Debug.Log("START FROM GameStateManager");
            
            _inputService.OnPausePressed += OnPausePressed;
            
        }

        private void LoseGameHandler()
        {
            _uiController.ShowLose();
        }

        private void OnPausePressed()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                _inputService.SwitchAllActionMapsTo("UI");
                _uiController.OpenPauseMenu();
                Time.timeScale = 0;
                Debug.Log("Game Paused. Input Map switched to UI.");
            }
            else
            {
                IsPaused = false;
                _inputService.SwitchAllActionMapsTo("Player");
                _uiController.ClosePauseMenu();
                Time.timeScale = 1;
                Debug.Log("Game Unpaused. Input Map switched to Player.");
            }
        }

        public void Tick()
        {
            if(IsPaused) return;
            _mainSystems.Run();
        }

        public void FixedTick()
        {
            if (IsPaused) return;
            _physicsSystems.Run();
        }

        public void Dispose()
        {
            _mainSystems.Destroy();
            _physicsSystems.Destroy();
            _inputService.OnPausePressed -= OnPausePressed;
        }
    }
}