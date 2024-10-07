using Godot;


namespace TunaVsLion.scripts;
public partial class Main : Node2D
{
	public override void _Ready()
	{
		// seed set for debugging
		GD.Seed(12345);
	}
	
}
