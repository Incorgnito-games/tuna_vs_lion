using Godot;
using System;
using TunaVsLion.scripts.components;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	//Character Fields
	protected int MeatValue = 1;
	public float BaseSpeed = 100.0f;
	private float _slowSpeed = 50.0f;
	
	//Mechanic Fields
	protected bool Selected = false;
	private double _moveTimer;
	
	//Physics Fields
	private Vector2 _bearing;
	public Vector2 newDir;
	public Vector2 newPos;
	
	//Signal Fields
	private AttackBox attackBox;

	private DetectionArea detectionArea;
	//Debug Fields

	
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		// attackBox = GetNode<AttackBox>("AttackBox");
			// detectionArea = GetNode<DetectionArea>("DetectionArea");
		// attackBox.BodyEntered += attackBox.OnAttackBoxBodyEntered;
		// detectionArea.BodyEntered += detectionArea.OnDetectionAreaEntered;
	}
	
	
	//**************************
	// Physics
	//**************************
	
	
	 
	public override void _PhysicsProcess(double delta)
	{
		if (Selected)
		{
			CharacterControls();
		}
	}
	private void CharacterControls(){
		var velocity = Velocity;

		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * BaseSpeed;
			velocity.X = direction.X * BaseSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, BaseSpeed );
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, BaseSpeed );
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	
	public void SlowMove(double delta)
	{
		Vector2 currentVel = Velocity;
		if (_bearing != Vector2.Zero)
		{
			currentVel.Y = _bearing.Y * _slowSpeed;
			currentVel.X = _bearing.X * _slowSpeed;
		}
		// Position += _bearing;
		Velocity = currentVel;
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
		 return Selected;
		 
	 }
	 
	 
	 
	 //****************************
	 // Signal Callbacks
	 //****************************
	
}
