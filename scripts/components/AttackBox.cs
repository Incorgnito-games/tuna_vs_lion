using Godot;
namespace TunaVsLion.scripts.components;

public partial class AttackBox : Area2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += OnAttackBoxBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnAttackBoxBodyEntered(Node2D body)
	{
		GD.Print("Owe lets fight!");
	}
}
