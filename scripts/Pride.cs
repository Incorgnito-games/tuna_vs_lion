using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
using TunaVsLion.scripts.components.state.lionStates;

namespace TunaVsLion.scripts;
using TunaVsLion.scripts.components.state;

public partial class Pride : Node2D
{
	//Component Fields
	[Export] public int prideSize = 2;
	[Export] public Lion PrideLeader;
	
	//Mechanics Fields
	private double _prideRadius;
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
		PrideLeader.SetLionName("The King");
		//Timer Setup
		positionChangeTimer.SetAutostart(true);
		positionChangeTimer.SetWaitTime(2);
		positionChangeTimer.Timeout += OnPrideMemberMovementTimeout;
		AddChild(positionChangeTimer);
		
		//pride setup
		for (var i = 0; i < prideSize; i++)
		{
			_lionPride.Add((Lion)ResourceLoader.Load<PackedScene>("res://scenes/meat/playable/lion.tscn").Instantiate());
			_lionPride[i].SetSelected(false);
			_lionPride[i].Position = GetRandomPointInPrideInfluence(this);
			var lionState = _lionPride[i].GetNode<LionIdle>("./StateMachine/LionIdle");
			lionState.SetPride(this);
			AddChild(_lionPride[i]);
			try
			{
				_lionPride[i].SetLionName(_debugNames[i]);
				
			}catch (IndexOutOfRangeException e)
			{
				_lionPride[i].SetLionName(i.ToString());
				GD.Print(e);
			}
		}
		
	}
	
	//*************************
	// Mechanic Logic
	//*************************
	public static Vector2 GetRandomPointInPrideInfluence(Pride pride)
	{
		//uniform random distribution of circles radius
		var uniformRandRadius = Mathf.Sqrt( GD.RandRange(0, (pride.GetPrideRadius() * pride.GetPrideRadius())));
		//random angle in radians (0 - 2pi)
		var randAngle = GD.RandRange(0, 2 * Mathf.Pi);

		var randPoint = new Vector2();

		//w/o center --> (old)this is assuming the cdnter of the pride influende is (0,0) aka center of local scene
		//w/ center --> (current)using player as centerpoint via prideLeader
		randPoint.X = pride.GetPrideLeaderPos().X + (float)(uniformRandRadius * Mathf.Cos(randAngle));
		randPoint.Y = pride.GetPrideLeaderPos().Y + (float)(uniformRandRadius * Mathf.Sin(randAngle));

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
	public Vector2 GetPrideLeaderPos()
	{
		return PrideLeader.Position;
	}	

	public double GetPrideRadius()
	{
		return _prideRadius;
	}

}
