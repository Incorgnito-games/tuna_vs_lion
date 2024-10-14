namespace TunaVsLion.scripts;

using System.Collections.Generic;
using System.Linq;
using Godot;

using meat.nonplayable;


public partial class Enviroment : Node
{
	public readonly List<AbstractNonPlayableMeat> LandMeat = new List<AbstractNonPlayableMeat>();
	
	
	//rabbits
	[Export] public int maxRabbitPopulation = 5;
	private static int _currentRabbitPop = 1;
	private Rabbit _dudleyTheRabbit;
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
		_dudleyTheRabbit = GetNode<Rabbit>("Rabbit");
		LandMeat.Add(_dudleyTheRabbit);
		
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
		LandMeat.Add(newBunny);
		LandMeat.Last().Position = Global.GetRandomPointOnLand();
		// var meatState = _landMeat[i].GetNode<RandomWalk>("Rabbit/StateMachine/RandomWalk");
		AddChild(LandMeat.Last());
		
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
