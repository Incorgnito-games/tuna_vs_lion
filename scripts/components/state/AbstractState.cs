using Godot;

namespace TunaVsLion.scripts.components.state;

public partial class AbstractState : Node, IState
{
    public virtual void Enter()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void Exit()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void UpdateProcess()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void UpdatePhysicsProcess()
    {
        // throw new System.NotImplementedException();
    }
}