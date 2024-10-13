using System.ComponentModel.DataAnnotations;
using TunaVsLion.scripts.meat.playable;

namespace TunaVsLion.scripts.components.state.Movement;
using Godot;
using meat.nonplayable;
using System.Linq;
using state;

public partial class Chase: State
{
	//character modifiers
	[Export] private AbstractPlayableMeat _character;
	[Export] private float _chaseMultiplier = 1.5f;
	[Export] private float _staminaUsagePerFrame = 1.0f;
	
	
	//debug
	
	//Signal Fields
	[Export]private DetectionArea _detectionArea;
	private CustomStateSignals _stateTransitionSignal;
	private float _distanceToTarget;
	
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
			//distance debug	
			var targetPosition = _character.CurrentTarget.GlobalPosition;
			_distanceToTarget = (targetPosition - _character.GlobalPosition).Length();
			
			//positioning
			var newDir = (targetPosition - _character.GlobalPosition).Normalized();
			_character.Velocity = newDir * _character.BaseSpeed * _chaseMultiplier;

			GD.Print(_distanceToTarget);
		    
			//free and remove attack npc --> move to attackarea callback
			if (_distanceToTarget < 5f) 
			{
				if (_character.ChaseTargets.Contains(_character.CurrentTarget))
				{
					_character.ChaseTargets.Remove(_character.CurrentTarget);
					_character.CurrentTarget.Free();
					GD.Print("Chomp!");
					if (_character.ChaseTargets.Count != 0)
					{
						_character.CurrentTarget = _character.GetClosetTarget();
					}
					else
					{
						_character.CurrentTarget = null;
						_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "lionidle");
						
					}
				}
				
				
			}
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