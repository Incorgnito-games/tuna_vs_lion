using Godot;
using System;
using System.Collections.Generic;

namespace TunaVsLion.scripts.components.state;

public partial class StateMachine : Node
{
    [Export] private State _initialState;
    public readonly Dictionary <String, State> StateDict = new Dictionary<String, State>();
    private State _currentState;
    private CustomStateSignals _transitionStateSignal;
    
    public override void _Ready()
    {
        _transitionStateSignal = GetNode<CustomStateSignals>("/root/CustomStateSignals");
        foreach (var child in this.GetChildren())
        {
            if (child is State)
            {
                StateDict.Add(child.Name, (State)child);
                // _transitionStateSignal.TransitionState += OnStateTransition;
            }
        }

        if (_initialState is not null)
        {
            _initialState.Enter();
            _currentState = _initialState;
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (_currentState is not null)
        {
           _currentState.UpdatePhysicsProcess(delta); 
        }
    }

    private void OnStateTransition(State state, string stateName)
    {
        if (state != _currentState)
        {
            return;
        }

        var newState = StateDict[stateName.ToLower()];
        GD.Print(StateDict.ToString());
        if (newState is null)
            return;

        if (_currentState is not null)
        {
            _currentState.Exit();
            GD.Print("no t null");
        }
        
        newState.Enter();
        _currentState = newState;

    }
}