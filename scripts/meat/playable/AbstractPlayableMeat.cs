using Godot;
using System;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	 private float _baseSpeed = 100.0f;
	 private float _slowSpeed = 25.0f;
	 protected int MeatValue = 1;
	 protected bool Selected = false;
	 protected double Health = 100;
	 private Vector2 _bearing;
	 private double _moveTimer;
	 
	
	 
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
		 return Selected;}
	 public abstract void SetSelected(bool isSelected);
	 
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
			velocity.Y = direction.Y * _baseSpeed;
			velocity.X = direction.X * _baseSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _baseSpeed );
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, _baseSpeed );
		}

		Velocity = velocity;
		MoveAndSlide();
		
	}

	public abstract void Spawn();

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

	public abstract void FastMove();

	public void Automate(Vector2 worldDim) { }
	
	
	public void OnBearingTimerTimeout()
	{
		this.SetRandomBearing();
	}
	//random walk
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
}
