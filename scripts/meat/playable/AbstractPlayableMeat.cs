using Godot;
using System;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	//Character Fields
	protected int MeatValue = 1;
	public float BaseSpeed = 300.0f;
	private float _slowSpeed = 3500.0f;
	
	//Mechanic Fields
	protected bool Selected = false;
	private double _moveTimer;
	
	//Physics Fields
	private Vector2 _bearing;
	public Vector2 newDir;
	public Vector2 newPos;
	
	//Debug Fields

	 
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

	//********************************
	// Mechanic Logic
	//********************************

	public void Automate(Vector2 worldDim) { }
	
	public void SetRandomBearing()
	{
		var random = new Random(); 
			
			var direction = random.Next(10);

			_bearing = direction switch
			{
				0 => new Vector2(0, -1), //up
				1 => new Vector2(0, 1), //down
				2 => new Vector2(-1, 0), //left
				3 => new Vector2(1, 0), //right
				4 => new Vector2(-1, -1), //up-left
				5 => new Vector2(1, -1), //up-right
				6 => new Vector2(-1, 1), //down-left
				7 => new Vector2(1, 1), //down-right
				_ => new Vector2(0, 0)
			};

	}
	//**************************
	// Signal Callbacks
	//**************************
	
	public void OnBearingTimerTimeout()
	{
		this.SetRandomBearing();
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
	 
	 
}
