using Godot;
using TunaVsLion.scripts.meat.nonplayable;
using System.Collections.Generic;
using System.Linq;
using TunaVsLion;
using TunaVsLion.scripts.components.state.Movement;

namespace TunaVsLion.scripts;

public partial class Enviroment : Node
{
	private readonly List<AbstractNonPlayableMeat> _landMeat = new List<AbstractNonPlayableMeat>();
	

	//rabbits
	[Export] public int maxRabbitPopulation = 5;
	private static int _currentRabbitPop = 1;

	[Signal]
	public delegate void UpdateRabbitPopEventHandler(int rabbitPop);	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_initiate();	
	}
	public void EmitSignalsInReady()
	{
		EmitSignal(SignalName.UpdateRabbitPop, _currentRabbitPop);
		
	}
	private void _initiate()
	{
		for (var i = _currentRabbitPop; i < maxRabbitPopulation; i++)
		{
			SpawnRabbit();
		}
		CallDeferred(nameof(EmitSignalsInReady));

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void SpawnRabbit()
	{
		Rabbit newBunny = (Rabbit)ResourceLoader.Load<PackedScene>("res://scenes/meat/nonplayable/land/rabbit.tscn").Instantiate();
		newBunny.RabbitName = $"Rabbit-{_currentRabbitPop}";
		newBunny.Enviroment = this;
		_landMeat.Add(newBunny);
		_landMeat.Last().Position = Global.GetRandomPointOnLand();
		// var meatState = _landMeat[i].GetNode<RandomWalk>("Rabbit/StateMachine/RandomWalk");
		AddChild(_landMeat.Last());
		
		_currentRabbitPop++;
		EmitSignal(SignalName.UpdateRabbitPop, _currentRabbitPop);
	}
	
	//*************
	// Signals
	//**************
	public void OnRabbitEaten(Node body)
	{
		_currentRabbitPop--;
		EmitSignal(SignalName.UpdateRabbitPop, _currentRabbitPop);
	}

}
