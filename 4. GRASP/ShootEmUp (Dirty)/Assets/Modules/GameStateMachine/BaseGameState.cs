using Zenject;

namespace ShootEmUp.Modules.GameStateMachine
{
    public abstract class BaseGameState
    {
        protected GameStateMachine StateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
        
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}