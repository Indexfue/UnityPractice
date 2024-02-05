using System;
using Data;
using UnityPractice.Infrastructure.Services.PersistentProgress;
using UnityPractice.Infrastructure.Services.SaveLoad;

namespace UnityPractice.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            _stateMachine.EnterIn<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() => new(initialLevel: "Main");
    }
}