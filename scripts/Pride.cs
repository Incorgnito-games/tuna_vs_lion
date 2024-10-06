using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
namespace TunaVsLion.scripts;


public partial class Pride : Node2D
{
	[Export] private int _prideRadius;
	private Lion _lionPlayer;
	private CollisionShape2D _prideInfluence;
	private readonly List<Lion> _lionPride = new List<Lion>();
	private Random _rand = new Random();
	private Vector2 _initialPlayerPos;
	
	public override void _Ready()
	{
		 _lionPlayer = GetNode<Lion>("lionPlayer");
		 _prideInfluence = GetNode<CollisionShape2D>("lionPlayer/Area2D/CollisionShape2D");
		 
		_Initiate();
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		//fired 60 times a second

		foreach(var lion in _lionPride)
		{
			if (!lion.GetSelected())
			{
				lion.SlowMove(delta);	
			}
		}
	}
	
	
	//TODO: fix overlap
	private void _Initiate()
	{
		_lionPlayer.SetSelected(true);
		_initialPlayerPos = _lionPlayer.Position;
		_prideInfluence.
		HashSet<Vector2> randCoord = new HashSet<Vector2>();

		string[] lionNames = { "john", "joe", "jack", "maya", "arthur", "suzy", "sarah", "sam", "puma", "Maya2" };
		for (var i = 0; i < 5; i++)
		{
			_lionPride.Add((Lion)ResourceLoader.Load<PackedScene>("res://scenes/meat/playable/lion.tscn").Instantiate());
			_lionPride[i].SetSelected(false);
			_lionPride[i].SetRandomBearing();
			var bearingTimer = new Timer();
			bearingTimer.SetAutostart(true);
			bearingTimer.WaitTime = 0.5f;
			bearingTimer.Timeout += _lionPride[i].OnBearingTimerTimeout;
			AddChild(bearingTimer);
			
			
			
			randCoord.Add(new Vector2(_lionPlayer.Position.X + _rand.Next(-150, 150),
				_lionPlayer.Position.Y + _rand.Next(-150, 150)));
		
			_lionPride[i].SetLionName(lionNames[i]);
			
			 // _lionPride[i].GetNode<Sprite2D>("lionPlayer/Sprite2D").Modulate = new Color(1, 0, 0 ,0.5f);
		}

		int index = 0;
		foreach(Vector2 coord in randCoord)
		{
			_lionPride[index].Position = coord;
			AddChild(_lionPride[index]);
				
			index++;

		}
		
	}
}
