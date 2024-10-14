namespace TunaVsLion.scripts;

using Godot;

public partial class Main : Node2D
{
	private Label _meatMeter;
	private Label _resources;
	public override void _Ready()
	{
		// seed set for debugging
		GD.Seed(12345);
		_meatMeter = GetNode<Label>("WorldStats/MeatMeterCont/MeatMeterValue");
		_resources = GetNode<Label>("WorldStats/ResourceCont/ResourceValue");
	
		_meatMeter.Text = Global.MeatMeter.ToString();
		_resources.Text = Global.Resources.ToString();
	}
	//************
	// Mechanics
	//************
	public override void _Process(double delta)
	{
		//change to signal
		_meatMeter.Text = Global.MeatMeter.ToString();
		_resources.Text = Global.Resources.ToString();
		
	}
	public override void _PhysicsProcess(double delta)
	{
		var viewPortSize = GetViewportRect().Size;
		
		/*
		 * Wrapping the x and y boundaries
		 */
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
