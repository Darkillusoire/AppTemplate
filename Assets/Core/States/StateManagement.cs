using System.Collections.Generic;
using UnityEngine.Events;


public interface IState<TOwner>
{
    void OnEnter(StateController<TOwner> controller, IState<TOwner> previous);
    void OnUpdate(StateController<TOwner> controller);
}

public class StateController<TOwner>
{
    public event UnityAction<IState<TOwner>, IState<TOwner>> StateChanged = delegate { };

    private List<IState<TOwner>> _states = new List<IState<TOwner>>();

    private TOwner _owner;

    private IState<TOwner> _currentState;

    protected IState<TOwner> currentState {
        get => _currentState;
        set {
            var isUpdated = value != null & value != _currentState;

            if (isUpdated) {
                var previous = _currentState;

                _currentState = value;
                _currentState.OnEnter(this, previous);

                StateChanged(previous, _currentState);
            }
        }
    }

    public StateController(TOwner owner) => _owner = owner;

    public IState<TOwner> GetState<TState>() where TState : IState<TOwner> => _states.Find(s => s is TState);

    public bool Contains(IState<TOwner> state) => _states.Contains(state);

    public void Add(params IState<TOwner>[] states) {
        foreach (var state in states)
            Add(state);
    }

    public void Add(IState<TOwner> state) {
        if (!_states.Contains(state)) {
            _states.Add(state);
        }
    }

    public IState<TOwner> Show(IState<TOwner> state) {
        if (state != null && _states.Contains(state))
            currentState = state;

        return currentState;
    }

    public IState<TOwner> Show<TState>() where TState : IState<TOwner> {
        var state = GetState<TState>();

        return Show(state);
    }

    public void Update() => currentState?.OnUpdate(this);
}


