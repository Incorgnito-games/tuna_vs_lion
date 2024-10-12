using Godot;
using System.Collections.Generic;
using TunaVsLion.scripts.components;
using TunaVsLion.scripts.components.state;
using TunaVsLion.scripts.meat.nonplayable;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	//Character Fields
	[Export]public int MeatValue = 1;
	[Export]public float BaseSpeed = 10.0f;
	[Export] public float PlayerBaseSpeed = 50.0f;
	[Export] public float Stamina = 100.0f;
	
	[Export]public bool IsPlayer { get; set; } = false;

	//state fields ---> why did i use a dict if i was going to require both the state object and keyword in signal callback????
	protected State _chaseState;

	//Mechanic Fields
	private double _moveTimer;
	public AbstractNonPlayableMeat CurrentTarget { get; protected set; }
	protected List<AbstractNonPlayableMeat> _chaseTargets = new List<AbstractNonPlayableMeat>();
	
	//Physics Fields
	private Vector2 _bearing;
	public Vector2 newDir;
	
	//Signal Fields
	protected AttackBox _attackBox;
	protected DetectionArea _detectionArea;
	protected CustomStateSignals _stateTransitionSignal;

	//Debug Fields
    //labels are cause a weird amount of issues<-----WTF
	
	
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		if (IsPlayer)
		{
			this.BaseSpeed = PlayerBaseSpeed;
		}
		
		

		
	}
	
	
	//***********************
	// Mechanics
	//**********************
	public void StaminaDrain(double drainMultiplier)
	{
			
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
	

	//**************************
	// Abstract Methods
	//**************************
	
	public abstract void FastMove();
	public abstract void Spawn();

    
	
	//***************************
	//Getters + Setters
	//***************************
	
	 //is this needed anymore ??
	public  void SetSelected(bool isSelected)
        {
    	    IsPlayer = isSelected;
        }
	
	 public virtual string toString()
	 {
		 return "PlayableMeat";
		 
	 }
	 
	 //is this needed anymore ??
	 public void SetBearing(Vector2 newBearing)
	 {
		 this._bearing = newBearing;
	 }

	 //is this needed anymore ??
	 public Vector2 GetBearing()
	 {
		 return this._bearing;
	 }
	 
	 //is this needed anymore ??
	 private Rect2 GetCollisionRect()
	 {
		 var collisionBodyNode = GetNode<CollisionShape2D>("/CollisionShap2D");
			
		 return collisionBodyNode.GetShape().GetRect();
	 }
	

	 //is this needed anymore ??
	 /*
	  * return vector containing character size Vector2(width,height)
	  */
	 private Vector2 GetCharacterDimensions()
	 {
		 return GetCollisionRect().Size;
	 }

	 //is this needed anymore ??
	 public bool GetSelected()
	 {
		 return IsPlayer;
		 
	 }
	 
	 
	 
	 //****************************
	 // Signal Callbacks
	 //****************************
	
}
