using Godot;
using System;
namespace TunaVsLion.scripts.ui;

public partial class WorldStats : BoxContainer
{
	private Label _lionPopValue;
	private Label _rabbitPopValue;
	[Export]private Pride _lionPride;
	[Export] private Enviroment _enviroment;

	private int _rabbitPop;
	private int _prideSize;
	public override void _Ready()
	{
		_lionPopValue = GetNode<Label>("LionPopCont/LionPopValue");
		_rabbitPopValue = GetNode<Label>("RabbitPopCont/RabbitPopValue");
		if (_lionPride is not null)
		{
			_lionPride.UpdatePrideSize += OnPrideSizeUpdateSignal;
			GD.Print("linon pride not null");
		}

		if (_enviroment is not null)
		{
			_enviroment.UpdateRabbitPop += OnRabbitPopUpdateSignal;
			
		}
	}

	public override void _Process(double delta)
	{
		_lionPopValue.Text = _prideSize.ToString();
		_rabbitPopValue.Text = _rabbitPop.ToString();
	}

	public void OnPrideSizeUpdateSignal(int prideSize)
	{
		GD.Print($"Pride Signal recieved ==> {prideSize}");
		this._prideSize = prideSize;
	}

	public void OnRabbitPopUpdateSignal(int rabbitPop)
	{
		GD.Print($"Rabbit signal Recieved => {rabbitPop}");
		this._rabbitPop = rabbitPop;
	}
}
