namespace TunaVsLion.scripts.meat.nonplayable;
using Godot;

using TunaVsLion.scripts.components;
using TunaVsLion.scripts.meat.nonplayable;

public partial class Rabbit : AbstractNonPlayableMeat
{
	
	private AttackBox _attackBox;
	private DetectionArea _detectionArea;
	public override void _Ready()
	{
		_attackBox = GetNode<AttackBox>("AttackBox");
		_detectionArea = GetNode<DetectionArea>("DetectionArea");

		_attackBox.BodyEntered += OnBodyEnterAttackBox;
		_detectionArea.BodyEntered += OnBodyEnteredDetectionArea;
	}

	public override void _Process(double delta)
	{
	}
	
	//****************
	// Signal Callback
	//****************
	public void OnBodyEnterAttackBox(Node2D body)
	{
		
	}

	public void OnBodyEnteredDetectionArea(Node2D body)
	{
		
	}
	
	
}
