using TunaVsLion.scripts.meat.playable;

namespace TunaVsLion.scripts.meat.nonplayable;
using Godot;

using TunaVsLion.scripts.components;
using TunaVsLion.scripts.meat.nonplayable;

public partial class Rabbit : AbstractNonPlayableMeat
{
	public string RabbitName;
	private AttackBox _attackBox;
	private DetectionArea _detectionArea;
	private Label _meatMeter;
	public override void _Ready()
	{
		_attackBox = GetNode<AttackBox>("AttackBox");
		_detectionArea = GetNode<DetectionArea>("DetectionArea");
		_meatMeter = GetNode<Label>("MeatMeter");

		_meatMeter.Text = MeatValue.ToString();

		_attackBox.BodyEntered += OnBodyEnterAttackBox;
		_attackBox.BodyEntered += Enviroment.OnRabbitEaten;
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
		// if (body is Lion)
		// {
		// 	
		// 	MeatValue = -1;
		// 	if (MeatValue <= 0)
		// 	{
		// 		this.QueueFree();
		// 		_meatMeter.Text = MeatValue.ToString();
		// 	}
		// }
	}

	public void OnBodyEnteredDetectionArea(Node2D body)
	{
		
	}

	public override string ToString()
	{
		return RabbitName;
	}
	
	
}
