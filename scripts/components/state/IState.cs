namespace TunaVsLion.scripts.components.state;

public interface IState
{
    public void Enter();

    public void Exit();

    public void UpdateProcess();

    public void UpdatePhysicsProcess();

}