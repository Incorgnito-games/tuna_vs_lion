using Godot;
namespace TunaVsLion.scripts.components;

public partial class DetectionArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		this.BodyEntered += OnDetectionAreaEntered;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnDetectionAreaEntered(Node2D Body)
	{
		GD.Print("Hey i see you in my personal space");
	}   
	
}
