using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp.Modules.GameStateMachine
{
    public sealed class GameStateMachine : ITickable, IFixedTickable
    {
        private Dictionary<string, BaseGameState> _states;
        private BaseGameState _currentState;
        private string _currentStateName;
        
        private readonly Dictionary<string, StateEvent> _stateEvents = new();

        public void Init(Dictionary<string, BaseGameState> states)
        {
            _states = states;
        }

        public void SetState(string stateName)
        {
            if (_states == null || !_states.ContainsKey(stateName))
                throw new KeyNotFoundException($"The state with name {stateName} was not found.");
            
            if (_currentState != null)
                NotifyListeners(_currentState.GetType().Name, false);
            
            _currentState?.Exit();
            _currentState = _states[stateName];
            _currentState.Enter();
            _currentStateName = stateName;
            
            NotifyListeners(stateName, true);
        }

        public void Tick()
        {
            _currentState?.Update();
            
            foreach (var stateEvent in _stateEvents)
            {
                if(stateEvent.Key == _currentStateName)
                    stateEvent.Value.OnUpdate?.Invoke();
            }
        }

        public void FixedTick()
        {
            _currentState?.FixedUpdate();
            
            foreach (var stateEvent in _stateEvents)
            {
                if(stateEvent.Key == _currentStateName)
                    stateEvent.Value.OnFixedUpdate?.Invoke();
            }
        }

        public void AddListener(string stateName, Action onEnter = null, Action onExit = null, Action onUpdate = null, Action onFixedUpdate = null)
        {
            if (!_stateEvents.ContainsKey(stateName))
                _stateEvents[stateName] = new StateEvent();

            if (onEnter != null)
                _stateEvents[stateName].OnEnter += onEnter;

            if (onExit != null)
                _stateEvents[stateName].OnExit += onExit;
            
            if (onUpdate != null)
                _stateEvents[stateName].OnUpdate += onUpdate;

            if (onFixedUpdate != null)
                _stateEvents[stateName].OnFixedUpdate += onFixedUpdate;
        }
        
        public void RemoveListener(string stateName, Action onEnter = null, Action onExit = null, Action onUpdate = null, Action onFixedUpdate = null)
        {
            if (!_stateEvents.ContainsKey(stateName))
                return;

            var stateEvent = _stateEvents[stateName];

            if (onEnter != null)
                stateEvent.OnEnter -= onEnter;

            if (onExit != null)
                stateEvent.OnExit -= onExit;

            if (onUpdate != null)
                stateEvent.OnUpdate -= onUpdate;

            if (onFixedUpdate != null)
                stateEvent.OnFixedUpdate -= onFixedUpdate;

            // Если все обработчики для состояния удалены, можно удалить саму запись
            if (stateEvent.OnEnter == null && stateEvent.OnExit == null && stateEvent.OnUpdate == null && stateEvent.OnFixedUpdate == null)
                _stateEvents.Remove(stateName);
        }

        
        private void NotifyListeners(string stateName, bool isEntering)
        {
            if (string.IsNullOrEmpty(stateName) || !_stateEvents.ContainsKey(stateName)) return;

            var events = _stateEvents[stateName];
            if (isEntering)
                events.OnEnter?.Invoke();
            else
                events.OnExit?.Invoke();
        }

        private class StateEvent
        {
            public Action OnEnter;
            public Action OnExit;
            public Action OnUpdate;
            public Action OnFixedUpdate;
        }
    }
}
