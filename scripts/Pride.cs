using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
namespace TunaVsLion.scripts;


public partial class Pride : Node2D
{
	//Component Fields
	[Export] public int prideSize = 2;

	//Mechanics Fields
	private double _prideRadius;
	private CollisionShape2D _prideInfluence;
	private Vector2 _prideLeaderPos;

	//Character Fields
	private Vector2 _initialPlayerPos;
	private Lion _lionPlayer;
	private readonly List<Lion> _lionPride = new List<Lion>();

	//Utility Fields
	private readonly Random _rand = new Random();
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
	
	
	//Debug
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		
		 _lionPlayer = GetNode<Lion>("lionPlayer");
		 _prideLeaderPos = _lionPlayer.Position;
		 _prideInfluence =
			 GetNode<CollisionShape2D>("lionPlayer/Area2D/CollisionShape2D");
		 _prideRadius = ((CircleShape2D)_prideInfluence.Shape).Radius;
		_Initiate();
	}
	private void _Initiate()
	{
		_lionPlayer.SetLionName("Player");
		_lionPlayer.SetSelected(true);
		_initialPlayerPos = _lionPlayer.Position;
		
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
			_lionPride[i].Position = GetRandomPointInPrideInfluence();
			_lionPride[i].newPos = GetRandomPointInPrideInfluence();
			AddChild(_lionPride[i]);
			try
			{
				_lionPride[i].SetLionName(_debugNames[i]);
				
			}catch (IndexOutOfRangeException e)
			{
				_lionPride[i].SetLionName(i.ToString());
			}
		}
		
	}
	
	//*****************************
	//Physics
	//*****************************
	public override void _PhysicsProcess(double delta)
	{
	
		_prideLeaderPos = _lionPlayer.Position;
		makePrideMemberMovements(delta);
	}

	private void makePrideMemberMovements(double delta)
	{
		foreach(var lion in _lionPride)
		{
			if (!lion.GetSelected()) //not currently selected as player memebr
			{
				
				var lionDirection = (lion.newPos - lion.Position).Normalized();
				lion.Velocity = lionDirection * lion.BaseSpeed/2; 
				
				lion.MoveAndSlide();
				if(lion.Position.DistanceTo(lion.newPos) < 10f){	
					lion.newPos = GetRandomPointInPrideInfluence();
				}
			}
		}
		
	}
	
	//*************************
	// Mechanic Logic
	//*************************
	public Vector2 GetRandomPointInPrideInfluence()
	{
		//uniform random distribution of circles radius
		var uniformRandRadius = Mathf.Sqrt( GD.RandRange(0, (_prideRadius * _prideRadius)));
		//random angle in radians (0 - 2pi)
		var randAngle = GD.RandRange(0, 2 * Mathf.Pi);

		var randPoint = new Vector2();

		//w/o center --> (old)this is assuming the cdnter of the pride influende is (0,0) aka center of local scene
		//w/ center --> (current)using player as centerpoint via prideLeader
		randPoint.X = _prideLeaderPos.X + (float)(uniformRandRadius * Mathf.Cos(randAngle));
		randPoint.Y = _prideLeaderPos.Y + (float)(uniformRandRadius * Mathf.Sin(randAngle));

		return randPoint;
	}
	
	//*********************
	// Signals Callbacks
	//*********************
	public void OnPrideMemberMovementTimeout()
	{
		
	}
}
