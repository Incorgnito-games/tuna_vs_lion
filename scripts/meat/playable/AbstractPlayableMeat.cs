using Godot;


namespace TunaVsLion.scripts.meat.playable;

public abstract partial class AbstractPlayableMeat : CharacterBody2D, IMeat
{
	 private float _baseSpeed = 300.0f;
	 protected int MeatValue = 1;
	 protected bool Selected = false;
	 protected double Health = 100;

	
	 
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

	public abstract void SlowMove();

	public abstract void FastMove();

	public abstract void Automate();
}
