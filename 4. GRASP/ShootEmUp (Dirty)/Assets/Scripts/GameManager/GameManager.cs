using ShootEmUp.Modules.GameStateMachine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : IInitializable
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.SetState(GameStatesNames.InitializationStateName);
        }
    }
}