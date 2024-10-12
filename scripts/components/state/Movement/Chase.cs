using TunaVsLion.scripts.meat.playable;

namespace TunaVsLion.scripts.components.state.Movement;
using Godot;
using System.Collections.Generic;
using meat.nonplayable;

public partial class Chase: State
{
	[Export] private AbstractPlayableMeat _character;
	[Export] private float _chaseMultiplier = 1.5f;
	[Export] private float _staminaUsagePerFrame = 1.0f;
	
	public override void Enter()
	{
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

    //************************
    // Mechanics
    //************************
    
    private void _autoChase()
    {
	    var targetPosition = _character.CurrentTarget.Position;

	    var newDir = (targetPosition - _character.Position).Normalized();
	    _character.Velocity = newDir * _character.BaseSpeed * _chaseMultiplier;

	    _character.MoveAndSlide();

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
}