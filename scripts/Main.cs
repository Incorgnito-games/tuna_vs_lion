using Godot;


namespace TunaVsLion.scripts;
public partial class Main : Node2D
{
	public override void _Ready()
	{
		// seed set for debugging
		GD.Seed(12345);
	}

	public override void _PhysicsProcess(double delta)
	{
		var viewPortSize = GetViewportRect().Size;
	
		var wrappableMembers = GetTree().GetNodesInGroup("wrappable");
		foreach(CharacterBody2D  member in wrappableMembers)
		{
			
				if (member.GlobalPosition.Y > viewPortSize.Y)
				{
					member.GlobalPosition = new Vector2(member.GlobalPosition.X, 0);
				}
				else if (member.GlobalPosition.Y < 0) 
				{
					member.GlobalPosition =
						new Vector2(member.GlobalPosition.X, viewPortSize.Y);
				}
				
				if (member.GlobalPosition.X > viewPortSize.X)
				{
					member.GlobalPosition = new Vector2(0,member.GlobalPosition.Y);
				}
				else if (member.GlobalPosition.X < 0) 
				{
					member.GlobalPosition =
						new Vector2(viewPortSize.X, member.GlobalPosition.Y);
				}
			
		}

	}
	
}
