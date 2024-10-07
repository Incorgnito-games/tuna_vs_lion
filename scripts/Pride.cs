using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
namespace TunaVsLion.scripts;


public partial class Pride : Node2D
{
	//Mechanics Fields
	private double _prideRadius;
	private CollisionShape2D _prideInfluence;
	
	//Character Fields
	private Vector2 _initialPlayerPos;
	private Lion _lionPlayer;
	private readonly List<Lion> _lionPride = new List<Lion>();
	
	//Utility Fields
	private readonly Random _rand = new Random();
	
	//**************************
	// Setup
	//**************************
	public override void _Ready()
	{
		 _lionPlayer = GetNode<Lion>("lionPlayer");
		 _prideInfluence =
			 GetNode<CollisionShape2D>("lionPlayer/Area2D/CollisionShape2D");
		 _prideRadius = ((CircleShape2D)_prideInfluence.Shape).Radius;
		_Initiate();
	}
	private void _Initiate()
	{
		_lionPlayer.SetSelected(true);
		_initialPlayerPos = _lionPlayer.Position;
		
		//attempt to prevent identical start locations for lion nodes
		var randCoord = new HashSet<Vector2>();

		//used for debuging
		string[] lionNames = { "john", "joe", "jack", "maya", "arthur", "suzy", "sarah", "sam", "puma", "Maya2" };
		
		for (var i = 0; i < 5; i++)
		{
			//pride setup
			_lionPride.Add((Lion)ResourceLoader.Load<PackedScene>("res://scenes/meat/playable/lion.tscn").Instantiate());
			_lionPride[i].SetSelected(false);
			_lionPride[i].SetRandomBearing();
			
			//Timer for pride members location change
			var prideTimer = new Timer();
			prideTimer.SetAutostart(true);
			prideTimer.WaitTime = 2.0f;
			prideTimer.Timeout += OnPrideMemberMovementTimeout;
			AddChild(prideTimer);

			//assign random position for pride member
			randCoord.Add(GetRandomPointInPrideInfluence(new Vector2(0,0)));
			//set name for debugging from predefined list
			_lionPride[i].SetLionName(lionNames[i]);
			
		}
		
		var index = 0;
		foreach(var coord in randCoord)
		{
			//assign initial starting coord and add member to scene
			_lionPride[index].Position = coord;
			AddChild(_lionPride[index]);
				
			index++;
		}
		
	}
	
	//*****************************
	//Physics
	//*****************************
	public override void _PhysicsProcess(double delta)
	{
		foreach(var lion in _lionPride)
		{
			if (!lion.GetSelected()) //not currently selected as player memebr
			{
				var newVel = lion.newDir * (float)delta * 3500;
				lion.Velocity = newVel;
				lion.MoveAndSlide();
			}
		}
	}
	
	//*************************
	// Mechanic Logic
	//*************************
	public Vector2 GetRandomPointInPrideInfluence(Vector2 offset)
	{
			var angle = GD.Randf() * Mathf.Pi * 2;
			var r = Mathf.Sqrt(GD.Randf()) * (float)_prideRadius;
			var x = r * Mathf.Cos(angle);
			var y = r * Mathf.Sin(angle);

			return offset + new Vector2(_lionPlayer.Position.X + x, _lionPlayer.Position.Y + y);
	}
	
	//*********************
	// Signals Callbacks
	//*********************
	public void OnPrideMemberMovementTimeout()
	{
		foreach(var lion in _lionPride)
		{
			var newPos = GetRandomPointInPrideInfluence(_prideInfluence.GlobalPosition);

			lion.newDir = (newPos - lion.GlobalPosition).Normalized();
			if (lion.newMoveMarker != null)
			{
				lion.newMoveMarker.Free();
			}
			lion.newMoveMarker = new ColorRect();
			lion.newMoveMarker.Size = new Vector2(5, 5);
			lion.newMoveMarker.Color = new Color(1, 0, 0);
			lion.newMoveMarker.Position = newPos;
			lion.AddChild(lion.newMoveMarker);
		}
	}
}
