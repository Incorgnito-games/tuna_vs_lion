using Godot;
using System;

namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	 private float _baseSpeed = 350.0f;
	 protected int MeatValue = 1;
	 protected bool Selected = false;
	 protected double Health = 100;



	 public bool GetSelected()
	 {
		 return Selected;}
	 public abstract void SetSelected(bool isSelected);
	 
	public override void _PhysicsProcess(double delta)
	{
		if (Selected)
		{
			CharacterControls(delta);
		}
	}
	void CharacterControls(double delta){
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * _baseSpeed;
			velocity.X = direction.X * _baseSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _baseSpeed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, _baseSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
	}

	public abstract void Spawn();

	public void SlowMove(Vector2 direction)
	{
		Vector2 velocity = Velocity;

		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * _baseSpeed/2;
			velocity.X = direction.X * _baseSpeed/2;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _baseSpeed/2);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, _baseSpeed/2);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public abstract void FastMove();

	public void Automate(Vector2 worldDim)
	{
		// throw new NotImplementedException();
	}

	//random walk
	public void RandomWalk(double delta, Vector2 charPos)
	{
		Random random = new Random();
			 int WALKDISTANCE = random.Next(0,100);
			
			int direction = random.Next(4);

			var currentPosition = Position;
			switch (direction)
			{
				case 0: // Up
					currentPosition.Y += (charPos.Y - WALKDISTANCE*2);
					break;
				case 1: // Down
					currentPosition.Y -=(charPos.Y - WALKDISTANCE);
					break;
				case 2: // Left
					currentPosition.X -=(charPos.X - WALKDISTANCE);
					break;
				case 3: // Right
					currentPosition.X +=(charPos.X - WALKDISTANCE*2);
					break;
			}

			Position = Position.MoveToward(currentPosition, _baseSpeed * (float)delta);
	}
}
