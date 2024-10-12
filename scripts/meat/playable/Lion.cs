
using TunaVsLion.scripts.components;
using Godot;
using TunaVsLion.scripts.meat.nonplayable;
using System.Collections.Generic;
using System.Linq;


namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{
	//signals
	// private CustomStateSignals _stateSignal;

	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	[Export] public double Health;

	//State fields
	//Debug Fields
	
	public string CharacterName { get; set; }
	protected Label _nameLabel;
	protected Label _meatMeter;
	protected AttackBox _attackBox;
    
    //**************************
    // Setup
    //**************************

    public override void _Ready()
    {
	    _attackBox = GetNode<AttackBox>("AttackBox");
	    _attackBox.BodyEntered += OnAttackBoxBodyEntered;
		 //play hud labeling
	    _nameLabel = GetNode<Label>("nameLabel"); 
	    _meatMeter = GetNode<Label>("meatMeter");
	    
		Initialize();
    }
	public override void _Process(double delta)
    {
	    if(_meatMeter is not null)
			_meatMeter.Text = MeatValue.ToString();
    }

    public virtual void Initialize()
    {
	    _nameLabel.Text = CharacterName;
	    _meatMeter.Text = MeatValue.ToString();
    }
    
    //***************************
    // Getters and Setters
    //***************************
    public override string  toString()
    {
	    return this.CharacterName;
    }
    
    //*******************************
    // Mechanics
    //*******************************
  
    //********************************
    // Signal callbacks
    //********************************
  
    //will need to implment own state
    public void OnAttackBoxBodyEntered(Node2D body)
    {
	    // GD.Print($"{_name}==> lets dance {((Lion)body).toString()}!");
	    //naive and doesnt make sense
	    if (body is AbstractNonPlayableMeat)
	    {
		    MeatValue++;
		    GD.Print($"{Name} ==> Chomp!");
	    }
    }
}