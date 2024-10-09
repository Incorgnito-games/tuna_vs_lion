using Godot;
using TunaVsLion.scripts.meat.nonplayable;
using System.Collections.Generic;
namespace TunaVsLion.scripts;

public partial class Enviroment : Node
{
	[Export] public int maxPopulation = 50;
	private readonly List<AbstractNonPlayableMeat> _landMeat = new List<AbstractNonPlayableMeat>();
	private int _currentPop = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_initiate();	
	}

	private void _initiate()
	{
		// for (var i = 0; i < maxPopulation; i++)
		// {
		// 	_landMeat.Add((Rabbit)ResourceLoader.Load<PackedScene>("res://scenes/meat/nonplayable/land/rabbit.tscn").Instantiate());
		// 	
		// }
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
