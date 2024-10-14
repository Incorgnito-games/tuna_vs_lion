namespace TunaVsLion.scripts.meat.playable;

using Godot;
using System.Collections.Generic;
using System.Linq;
using nonplayable;


public abstract partial class AbstractPlayableMeat : AbstractMeat, IMeat
{
	//Character Fields
	[Export] public float PlayerBaseSpeed = 50.0f;
	[Export]public bool IsPlayer { get; set; }
	[Export] public string CharacterName { get; set; } = "The King";

	//Mechanic Fields
	private double _moveTimer;
	public List<AbstractNonPlayableMeat> ChaseTargets = new List<AbstractNonPlayableMeat>();
	public AbstractNonPlayableMeat CurrentTarget { get;  set; }

	//Physics Fields

	//Debug Fields
	
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
	    
		_nameLabel = GetNode<Label>("nameLabel"); 
	    
		//play hud labeling
		_nameLabel.Text = CharacterName;
		BaseSpeed = 10.0f;
		Stamina = MaxStamina;

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

	 public override string ToString()
	 {
		 return CharacterName;
		 
	 }
	 
	 //****************************
	 // Signal Callbacks
	 //****************************
	 
}
