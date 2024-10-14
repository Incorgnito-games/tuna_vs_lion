namespace TunaVsLion.scripts.meat.nonplayable;

using Godot;

using playable;


public partial class Rabbit : AbstractNonPlayableMeat
{
	public string RabbitName = "Dudley";
	private Area2D _attackBox;
	private Area2D _detectionArea;
	private Label _meatMeter;
	private Label _staminaMeter;
	private Label _healthMeter;

	
	[Export]
	public Enviroment Enviroment
	{
		get;
		set;
	}
	
	public override void _Ready()
	{
		base._Ready();
		_attackBox = GetNode<Area2D>("AttackBox");
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_meatMeter = GetNode<Label>("BoxContainer/MeatContainer/MeatMeter");
		_staminaMeter = GetNode<Label>("BoxContainer/Stamina/StaminaMeter");
		_healthMeter = GetNode<Label>("BoxContainer/Health/HealthMeter");
		
		_detectionArea.BodyEntered += OnBodyEnteredDetectionArea;
		_attackBox.BodyEntered += Enviroment.OnRabbitEaten;
		_attackBox.BodyEntered += OnBodyEnterAttackBox;
		
		
		
		_meatMeter.Text = MeatValue.ToString();
		_staminaMeter.Text = Stamina.ToString();
		_healthMeter.Text = Health.ToString();
	}

	//****************
	//Mechanics
	//****************
	public override void _Process(double delta)
	{
		if(_meatMeter is not null)
			_meatMeter.Text = MeatValue.ToString();
		if(_staminaMeter is not null)
			_staminaMeter.Text = Stamina.ToString();
		if(_healthMeter is not null)
			_healthMeter.Text = Health.ToString();
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
					// GD.Print($"{this.ToString()} removed from {body} chasetargets list");
				}
				else
				{
					// GD.Print($"{this.ToString()} not removed from {body} chase targets list");
				}
				this.QueueFree();
				_meatMeter.Text = MeatValue.ToString();
			}
			((AbstractPlayableMeat)body).MeatValue++;
			((AbstractPlayableMeat)body).CurrentTarget = null;
			if (Enviroment.LandMeat.Remove(this))
			{
				// GD.Print($"{this.ToString()} removed from enviroment meat list");
			}
			else
			{
				// GD.Print($"{this.ToString()} was notremoved from enviroment meat list");
				
			}
			
			GD.Print($"{body} ==> Chomp!");
		}
	}

	public void OnBodyEnteredDetectionArea(Node2D body)
	{
		if (body is AbstractPlayableMeat)
		{
			this.CurrentAttacker = (AbstractPlayableMeat)body;
		}
		
	}

	public override string ToString()
	{
		return RabbitName;
	}
	
	
}
