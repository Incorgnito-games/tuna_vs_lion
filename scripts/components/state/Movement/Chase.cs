namespace TunaVsLion.scripts.components.state.Movement;

using System.Linq;
using Godot;

using meat.playable;
using meat.nonplayable;
using state;

public partial class Chase: State
{
	//character modifiers
	[Export] private AbstractPlayableMeat _meat;
	[Export] private float _chaseMultiplier = 1.5f;
	[Export] private float _staminaUsagePerFrame = 2.0f;
	
	
	//debug
	
	//Signal Fields
	[Export]private Area2D _detectionArea;
	private float _distanceToTarget;
	
	public override void Enter()
	{
		if (_meat is null)
			return;

		if (!_meat.IsPlayer)
		{
			GD.Print($"{_meat} ==> Chasing");
			_autoChase();
		}
	
	}

    public override void Exit()
    {
    }

    public override void _Ready()
    {
        base._Ready();
	    _detectionArea.BodyExited += OnBodyExitedDetectionArea;
    }

    //************************
    // Mechanics
    //************************
    
    private void _autoChase()
    {
	    if (_meat.CurrentTarget is not null && _meat.CurrentTarget.IsInsideTree())
	    {
		    //reduce stamina
		    if (_meat.Stamina > 0)
		    {
			    _meat.Stamina -= _staminaUsagePerFrame;
		    }
		    else
		    {
				_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "rest");
		    }
			//distance debug	
			var targetPosition = _meat.CurrentTarget.GlobalPosition;
			_distanceToTarget = (targetPosition - _meat.GlobalPosition).Length();
			
			//positioning
			var newDir = (targetPosition - _meat.GlobalPosition).Normalized();
			_meat.Velocity = newDir * _meat.BaseSpeed * _chaseMultiplier;

			// GD.Print(_distanceToTarget);
		    
			//free and remove attack npc --> move to attackarea callback
			// if (_distanceToTarget < 5f) 
			// {
			// 	if (_character.ChaseTargets.Contains(_character.CurrentTarget))
			// 	{
			// 		_character.ChaseTargets.Remove(_character.CurrentTarget);
			// 		_character.CurrentTarget.Free();
			// 		GD.Print("Chomp!");
			// 		if (_character.ChaseTargets.Count != 0)
			// 		{
			// 			_character.CurrentTarget = _character.GetClosetTarget();
			// 		}
			// 		else
			// 		{
			// 			_character.CurrentTarget = null;
			// 			_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "lionidle");
			// 			
			// 		}
				// }
				
				
			// }
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
		    if (!_meat.IsPlayer)
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

	    if (_meat.ChaseTargets.Count == 0)
	    {
		    _meat.CurrentTarget = null;
		    _stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "idle");
	    }

	    if (_meat.ChaseTargets.Contains(body))
	    {
		    _meat.ChaseTargets.Remove((AbstractNonPlayableMeat)body);
		    if (_meat.ChaseTargets.Count != 0)
		    {
			    _meat.CurrentTarget = _meat.GetClosetTarget();
		    }
	    }

	    // if (CurrentTarget.GetInstanceId() == body.GetInstanceId())
	    // {
	    //  CurrentTarget = null;
	    // }
	    
    }
    
}