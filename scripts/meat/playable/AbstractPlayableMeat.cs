using Godot;
using System.Collections.Generic;
using System.Linq;
using TunaVsLion.scripts.meat.nonplayable;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	//Character Fields
	[Export]public int MeatValue = 1;
	[Export]public float BaseSpeed = 10.0f;
	[Export] public float PlayerBaseSpeed = 50.0f;
	[Export] public float Stamina = 100.0f;
	[Export]public bool IsPlayer { get; set; }
	[Export] public string CharacterName { get; set; } = "The King";

	//Mechanic Fields
	private double _moveTimer;
	public List<AbstractNonPlayableMeat> ChaseTargets = new List<AbstractNonPlayableMeat>();
	public AbstractNonPlayableMeat CurrentTarget { get;  set; }

	//Physics Fields

	//Debug Fields
	protected Label _meatMeter;
	protected Label _nameLabel;
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		if (IsPlayer)
		{
			this.BaseSpeed = PlayerBaseSpeed;
		}
	    _meatMeter = GetNode<Label>("meatMeter");
		_nameLabel = GetNode<Label>("nameLabel"); 
	    
		//play hud labeling
		_meatMeter.Text = MeatValue.ToString();
		_nameLabel.Text = CharacterName;

	}
		
	//**************************
	// Physics
	//**************************
	public override void _PhysicsProcess(double delta)
	{
		if (IsPlayer)
		{
			CharacterControls();
		}
	}
	private void CharacterControls(){
		var velocity = Velocity;

		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * PlayerBaseSpeed;
			velocity.X = direction.X * PlayerBaseSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, PlayerBaseSpeed );
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, PlayerBaseSpeed );
		}
		Velocity = velocity;
		MoveAndSlide();
		// GD.Print(GlobalPosition);
	}
	//***********************
	// Mechanics
	//**********************
	
	public override void _Process(double delta)
    {
	    if(_meatMeter is not null)
			_meatMeter.Text = MeatValue.ToString();
    }
	public AbstractNonPlayableMeat GetClosetTarget()
	{
		return ChaseTargets.OrderBy(vec => this.Position.DistanceTo(vec.Position)).First();
	}

	//**************************
	// Abstract Methods
	//**************************
    
	
	//***************************
	//Getters + Setters
	//***************************

	 public virtual string toString()
	 {
		 return CharacterName;
		 
	 }
	 
	 //****************************
	 // Signal Callbacks
	 //****************************
	
}
