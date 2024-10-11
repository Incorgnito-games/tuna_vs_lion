using Godot;
using TunaVsLion.scripts.meat.nonplayable;
using System.Collections.Generic;
using System.Linq;
using TunaVsLion;
using TunaVsLion.scripts.components.state.Movement;

namespace TunaVsLion.scripts;

public partial class Enviroment : Node
{
	[Export] public int maxPopulation = 50;
	private readonly List<AbstractNonPlayableMeat> _landMeat = new List<AbstractNonPlayableMeat>();
	private static int _currentPop = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_initiate();	
	}

	private void _initiate()
	{
		for (var i = 0; i < maxPopulation; i++)
		{
			SpawnRabbit();
		}
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_currentPop < 50)
		{
			SpawnRabbit();
		}
		
	}

	public void SpawnRabbit()
	{
		_landMeat.Add((Rabbit)ResourceLoader.Load<PackedScene>("res://scenes/meat/nonplayable/land/rabbit.tscn").Instantiate());
		_landMeat.Last().Position = Global.GetRandomPointOnLand();
		// var meatState = _landMeat[i].GetNode<RandomWalk>("Rabbit/StateMachine/RandomWalk");
		AddChild(_landMeat.Last());
		
		_currentPop++;
	}
	
	//*************
	// Signals
	//**************
	public static void OnRabbitEaten(Node node)
	{
		_currentPop--;
	}
}
