namespace TunaVsLion.scripts.components.state;
using Godot;
public abstract partial class State : Node, IState
{
    [Signal]
    public delegate void OnChildTransitionEventHandler(State state, string newStateName);


    public abstract void Enter();
    public abstract void Exit();
    public abstract void UpdateProcess(double delta);
    public abstract void UpdatePhysicsProcess(double delta);
}