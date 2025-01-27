using System.Collections.Generic;
using ShootEmUp.Modules.GameStateMachine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InitializingGameState>().AsSingle();
            Container.Bind<MenuGameState>().AsSingle();
            Container.Bind<CountdownState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<PauseState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle()
                .OnInstantiated<GameStateMachine>((context, storyStateMachine) =>
                {
                    var states = new Dictionary<string, BaseGameState>
                    {
                        { GameStatesNames.InitializationStateName, Container.Resolve<InitializingGameState>()},
                        { GameStatesNames.MenuStateName,  Container.Resolve<MenuGameState>()},
                        { GameStatesNames.CountdownStateName,  Container.Resolve<CountdownState>()},
                        { GameStatesNames.GameplayStateName,  Container.Resolve<GameplayState>()},
                        { GameStatesNames.PauseStateName,  Container.Resolve<PauseState>()},
                        { GameStatesNames.GameOverStateName,  Container.Resolve<GameOverState>()},
                    };
                    storyStateMachine.Init(states); 
                }).NonLazy();
        }
    }
}
