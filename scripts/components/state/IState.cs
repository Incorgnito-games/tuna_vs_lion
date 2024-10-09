namespace TunaVsLion.scripts.components.state;
using Godot;

public interface IState
{

    
    
    public void Enter();

    public void Exit();

    public void UpdateProcess(double delta);

    public void UpdatePhysicsProcess(double delta);

   }