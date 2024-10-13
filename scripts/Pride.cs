using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
using TunaVsLion.scripts.components.state.lionStates;

namespace TunaVsLion.scripts;

public partial class Pride : Node2D
{
	//Component Fields
	[Export] public int prideSize = 2;
	[Export] public Lion PrideLeader { get; set; }
	
	//Mechanics Fields
	private double _prideRadius { get; set; }
	private CollisionShape2D _prideInfluence;

	//Character Fields
	private readonly List<Lion> _lionPride = new List<Lion>();

	//Utility Fields
	private Timer positionChangeTimer = new Timer();
	private string[] _debugNames = {
		"lucy",
		"bob",
		"sarah",
		"chuck",
		"Christine",
		"Paul",
		"nancy",
		"puma"
	};
	
	
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		 _prideInfluence =
			 GetNode<CollisionShape2D>("lionPlayer/Area2D/CollisionShape2D");
		 _prideRadius = ((CircleShape2D)_prideInfluence.Shape).Radius;
		_Initiate();
	}
	private void _Initiate()
	{
		GD.Print(PrideLeader.CharacterName);
		//Timer Setup
		positionChangeTimer.SetAutostart(true);
		positionChangeTimer.SetWaitTime(2);
		positionChangeTimer.Timeout += OnPrideMemberMovementTimeout;
		AddChild(positionChangeTimer);
		
		//pride setup
		for (var i = 0; i < prideSize; i++)
		{
			_lionPride.Add((Lion)ResourceLoader.Load<PackedScene>("res://scenes/meat/playable/lion.tscn").Instantiate());
			_lionPride[i].IsPlayer =false;
			_lionPride[i].Position = GetRandomPointInPrideInfluence(this);
			try
			{
				_lionPride[i].CharacterName = _debugNames[i];
				
			}catch (IndexOutOfRangeException e)
			{
				_lionPride[i].CharacterName = $"Lion-{i.ToString()}";
				GD.Print(e);
			}
			
			var lionState = _lionPride[i].GetNode<LionIdle>("./StateMachine/LionIdle");
			lionState.SetPride(this);
			
			AddChild(_lionPride[i]);
		}
		
	}
	
	//*************************
	// Mechanic Logic
	//*************************
	public static Vector2 GetRandomPointInPrideInfluence(Pride pride)
	{
		//uniform random distribution of circles radius
		var uniformRandRadius = Mathf.Sqrt( GD.RandRange(0, (pride._prideRadius * pride._prideRadius)));
		//random angle in radians (0 - 2pi)
		var randAngle = GD.RandRange(0, 2 * Mathf.Pi);

		var randPoint = new Vector2();

		//w/o center --> (old)this is assuming the cdnter of the pride influende is (0,0) aka center of local scene
		//w/ center --> (current)using player as centerpoint via prideLeader
		//global or local --> this still fucks me up and will take a bit
		randPoint.X = pride.PrideLeader.Position.X + (float)(uniformRandRadius * Mathf.Cos(randAngle));
		randPoint.Y = pride.PrideLeader.Position.Y + (float)(uniformRandRadius * Mathf.Sin(randAngle));

		return randPoint;
	}
	
	//**********************************
	// Signals Callbacks + Notifications
	//**********************************
	public void OnPrideMemberMovementTimeout()
	{
		
	}

	//*********************
	// Getters + Setters
	//*********************


}
