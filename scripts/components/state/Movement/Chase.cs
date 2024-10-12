using TunaVsLion.scripts.meat.playable;

namespace TunaVsLion.scripts.components.state.Movement;
using Godot;
using meat.nonplayable;
using System.Linq;
using state;

public partial class Chase: State
{
	[Export] private AbstractPlayableMeat _character;
	[Export] private float _chaseMultiplier = 1.5f;
	[Export] private float _staminaUsagePerFrame = 1.0f;
	
	//Signal Fields
	[Export]private DetectionArea _detectionArea;
	private CustomStateSignals _stateTransitionSignal;
	
	public override void Enter()
	{
		GD.Print("Entering chase state");
		if (_character is null)
			return;

		if (!_character.IsPlayer)
		{
			GD.Print("Chasing");
			_autoChase();
		}
	
	}

    public override void Exit()
    {
    }

    public override void _Ready()
    {
	    _stateTransitionSignal = GetNode<CustomStateSignals>("/root/CustomStateSignals");
	    _detectionArea.BodyExited += OnBodyExitedDetectionArea;
    }

    //************************
    // Mechanics
    //************************
    
    private void _autoChase()
    {
	    if (_character.CurrentTarget is not null)
	    {
		    
	    var targetPosition = _character.CurrentTarget.Position;

	    var newDir = (targetPosition - _character.Position).Normalized();
	    _character.Velocity = newDir * _character.BaseSpeed * _chaseMultiplier;

	    _character.MoveAndSlide();
	    }

    }
    
    public override void UpdateProcess(double delta)
    {
    }

    public override void UpdatePhysicsProcess(double delta)
    {
	    if (_character is not null)
	    {
		    if (!_character.IsPlayer)
		    {
			    _autoChase();
		    }
	
	    }
    }
    
    
    //*******************************
    // Signals
    //*******************************
    
    

    private void OnBodyExitedDetectionArea(Node2D body)
    {
	    if (body is null)
		    return;

	    if (_character.ChaseTargets.Count == 0)
	    {
		    _stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "lionidle");
	    }

	    if (_character.ChaseTargets.Contains(body))
	    {
		    _character.ChaseTargets.Remove((AbstractNonPlayableMeat)body);
		    if (_character.ChaseTargets.Count != 0)
		    {
			    _character.CurrentTarget = _character.GetClosetTarget();
		    }
	    }

	    // if (CurrentTarget.GetInstanceId() == body.GetInstanceId())
	    // {
	    //  CurrentTarget = null;
	    // }
	    
    }
    
}