using TunaVsLion.scripts.meat.playable;

namespace TunaVsLion.scripts.meat.nonplayable;
using Godot;

using TunaVsLion.scripts.components;
using TunaVsLion.scripts.meat.nonplayable;

public partial class Rabbit : AbstractNonPlayableMeat
{
	public string RabbitName;
	private Area2D _attackBox;
	private Area2D _detectionArea;
	private Label _meatMeter;

	[Export]
	public Enviroment Enviroment
	{
		get;
		set;
	}
	
	public override void _Ready()
	{
		_attackBox = GetNode<Area2D>("AttackBox");
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_meatMeter = GetNode<Label>("MeatMeter");

		_detectionArea.BodyEntered += OnBodyEnteredDetectionArea;
		_attackBox.BodyEntered += Enviroment.OnRabbitEaten;
		_attackBox.BodyEntered += OnBodyEnterAttackBox;
		
		_meatMeter.Text = MeatValue.ToString();
	}
	
	//****************
	// Signal Callback
	//****************
	public void OnBodyEnterAttackBox(Node2D body)
	{
		if (body is AbstractPlayableMeat)
		{
			
			MeatValue = -1;
			if (MeatValue <= 0)
			{
				if (((AbstractPlayableMeat)body).ChaseTargets.Remove(this))
				{
					GD.Print("rabbit removed from chase possibilities removed");
				}
				else
				{
					GD.Print("item not removed");
				}
				this.QueueFree();
				_meatMeter.Text = MeatValue.ToString();
			}
			body.MeatValue++ as AbstractNonPlayableMeat;
			GD.Print($"{Name} ==> Chomp!");
		}
	}

	public void OnBodyEnteredDetectionArea(Node2D body)
	{
		
	}

	public override string ToString()
	{
		return RabbitName;
	}
	
	
}
