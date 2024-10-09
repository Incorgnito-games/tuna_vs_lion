using Godot;
using System;
using TunaVsLion.scripts.components;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	//Character Fields
	[Export]public int MeatValue = 1;
	[Export]public float BaseSpeed = 10.0f;
	[Export] public float PlayerBaseSpeed = 50.0f;
	
	[Export]public bool IsPlayer = false;
	
	//Mechanic Fields
	private double _moveTimer;
	
	//Physics Fields
	private Vector2 _bearing;
	public Vector2 newDir;
	
	//Signal Fields
	protected AttackBox attackBox;
	protected DetectionArea detectionArea;

	//Debug Fields
	
	
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
	}
	

	//**************************
	// Abstract Methods
	//**************************
	
	public abstract void FastMove();
	public abstract void Spawn();
	 public abstract void SetSelected(bool isSelected);

	
	//***************************
	//Getters + Setters
	//***************************
	 public virtual string toString()
	 {
		 return "PlayableMeat";
		 
	 }
	 public void SetBearing(Vector2 newBearing)
	 {
		 this._bearing = newBearing;
	 }

	 public Vector2 GetBearing()
	 {
		 return this._bearing;
	 }
	 
	 private Rect2 GetCollisionRect()
	 {
		 var collisionBodyNode = GetNode<CollisionShape2D>("/CollisionShap2D");
			
		 return collisionBodyNode.GetShape().GetRect();
	 }
	

	 /*
	  * return vector containing character size Vector2(width,height)
	  */
	 private Vector2 GetCharacterDimensions()
	 {
		 return GetCollisionRect().Size;
	 }

	 public bool GetSelected()
	 {
		 return IsPlayer;
		 
	 }
	 
	 
	 
	 //****************************
	 // Signal Callbacks
	 //****************************
	
}
