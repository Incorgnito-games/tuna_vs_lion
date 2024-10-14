namespace TunaVsLion.scripts.components.state.Movement;

using Godot;
using meat.nonplayable;
using state;

public partial class Flee: State
{
	//character modifiers
	[Export] private AbstractNonPlayableMeat _meat;
	[Export] private float _fleeMultiplier = 1.5f;
	[Export] private float _staminaUsagePerFrame = 1.0f;
	
	
	//debug
	
	//Signal Fields
	[Export]private Area2D _detectionArea;
	private CustomStateSignals _stateTransitionSignal;
	private float _distanceFromAttacker;
	
	public override void Enter()
	{
		if (_meat is null)
			return;

		_autoFlee();
	
	}

    public override void Exit()
    {
    }

    public override void _Ready()
    {
	    _stateTransitionSignal = GetNode<CustomStateSignals>("/root/CustomStateSignals");
	    _detectionArea.BodyExited += OnDetectionAreaBodyExited;
    }

    //************************
    // Mechanics
    //************************
    
    private void _autoFlee()
    {
	    if (_meat.CurrentAttacker is not null && _meat.CurrentAttacker.IsInsideTree())
	    {
			//distance debug	
			var targetPosition = _meat.CurrentAttacker.GlobalPosition;
			_distanceFromAttacker = (targetPosition - _meat.GlobalPosition).Length();
			
			//positioning
			var newDir = -(targetPosition - _meat.GlobalPosition).Normalized();
			_meat.Velocity = newDir * _meat.BaseSpeed * _fleeMultiplier;

			_meat.MoveAndSlide(); 
	    }

    }
    
    public override void UpdateProcess(double delta)
    {
    }

    public override void UpdatePhysicsProcess(double delta)
    {
	    if (_meat is not null)
	    {
			    _autoFlee();
	    }
    }
    
    
    //*******************************
    // Signals
    //*******************************
    
    

    private void OnDetectionAreaBodyExited(Node2D body)
    {
	    if (body is null)
		    return;
	    if (body == _meat.CurrentAttacker)
	    {
			_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "randomWalk");
	    } 
    }
    
}