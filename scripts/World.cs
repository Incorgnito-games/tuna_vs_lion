using System;
using Godot;
using TunaVsLion.scripts.meat.playable;
using System.Collections.Generic;
namespace TunaVsLion.scripts;


public partial class World : Node2D
{
	private Lion _lionPlayer;
	private readonly List<Lion> _lionPride = new List<Lion>();
	private Random rand = new Random();
	private Vector2 _initialPlayerPos;
	private double walkTimer = 0.0;
	private double walkInterval = 0.02;
	
	public override void _Ready()
	{
		 _lionPlayer = GetNode<Lion>("lionPlayer");
		 
		_Initiate();
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		
			foreach(var lion in _lionPride)
			{
				if (!lion.GetSelected())
				{
					
				lion.RandomWalk(delta, _lionPlayer.Position);
				}
				// lion.Automate(new Vector2(Global.WORLD_WIDTH,Global.WORLD_HEIGHT));
				// /lion.Automate(new Vector2(Global.WORLD_WIDTH,Global.WORLD_HEIGHT));
			}

		
	}
	
	//TODO: fix overlap
	private void _Initiate()
	{
		_lionPlayer.SetSelected(true);
		_initialPlayerPos = _lionPlayer.Position;
			
		HashSet<Vector2> randCoord = new HashSet<Vector2>();

		
		for (var i = 0; i < 10; i++)
		{
			_lionPride.Add((Lion)ResourceLoader.Load<PackedScene>("res://scenes/meat/playable/lion.tscn").Instantiate());
			_lionPride[i].SetSelected(false);
			
			randCoord.Add(new Vector2(_lionPlayer.Position.X + rand.Next(-150, 150),
				_lionPlayer.Position.Y + rand.Next(-150, 150)));
		
			
			
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
