namespace TunaVsLion.scripts.meat.playable;

using Godot;

public partial class Lion: AbstractPlayableMeat
{
	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	//signals
	protected Area2D _attackBox;
	
	//Debug Fields
	protected Label _meatMeter;
	protected Label _staminaMeter;
	protected Label _healthMeter; 
    //**************************
    // Setup
    //**************************
    public override void _Ready()
    {
	    base._Ready();
	    _meatMeter = GetNode<Label>("BoxContainer/MeatContainer/MeatMeter");
	    _staminaMeter = GetNode<Label>("BoxContainer/Stamina/StaminaMeter");
	    _healthMeter = GetNode<Label>("BoxContainer/Health/HealthMeter");
	    
	
	    
		_meatMeter.Text = MeatValue.ToString();
		_staminaMeter.Text = Stamina.ToString();
		_healthMeter.Text = Health.ToString();
    }

    //***************************
    // Getters and Setters
    //***************************

    //*******************************
    // Mechanics
    //*******************************
 
    public override void _Process(double delta)
    {
	    if(_meatMeter is not null)
		    _meatMeter.Text = MeatValue.ToString();
	    if(_staminaMeter is not null)
		    _staminaMeter.Text = Stamina.ToString();
	    if(_healthMeter is not null)
		    _healthMeter.Text = Health.ToString();
    }
    //********************************
    // Signal callbacks
    //********************************
  
 
}