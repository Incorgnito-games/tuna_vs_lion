
using TunaVsLion.scripts.components;
using Godot;
using TunaVsLion.scripts.meat.nonplayable;

namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{

	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	[Export] public double Health;
	
	//Debug Fields
    private string _name;
	private Label _nameLabel;
	private Label _meatMeter;
	
    
    //**************************
    // Setup
    //**************************

    public override void _Ready()
    {
	    attackBox = GetNode<AttackBox>("AttackBox");
	    detectionArea = GetNode < DetectionArea>("DetectionArea");
	    _nameLabel = GetNode<Label>("nameLabel");
	    _meatMeter = GetNode<Label>("meatMeter");
	    
	    _nameLabel.Text = _name;
	    _meatMeter.Text = MeatValue.ToString();
	    

	    attackBox.BodyEntered += OnAttackBoxBodyEntered;
	    detectionArea.BodyEntered += OnDetectionAreaBodyEntered; 
	    Initialize(); 
	    
    }
    public void Initialize()
    {
	    
	  
	  
    }

    public override void _Process(double delta)
    {
	    this._meatMeter.Text = MeatValue.ToString();
	    // GD.Print(MeatValue);
    }

    //***************************
    // Getters and Setters
    //***************************
    public override string  toString()
    {
	    return this._name;
    }

    public void SetLionName(string name)
    { 
	    this._name = name;
	    this._nameLabel.Text = name;
    }
    
    public override void SetSelected(bool isSelected)
    {
	    IsPlayer = isSelected;
    }
    
    //*******************************
    // Signals
    //*******************************
    public void OnAttackBoxBodyEntered(Node2D body)
    {
	    // GD.Print($"{_name}==> lets dance {((Lion)body).toString()}!");
	    //naive and doesnt make sense
	    if (body is Rabbit)
	    {
		    MeatValue++;
		    GD.Print("Chomp!");
	    }
    }

    public void OnDetectionAreaBodyEntered(Node2D body)
    {
	    // GD.Print($"{_name} ==> i see {((Lion)body).toString()}!");
    }
	
    //********************************
    // Mechanics Logic
    //********************************
    void Run() {}
    void Climb() {}
    void Swim() {}
    void Hunt() {}
    public override void Spawn() {}
	public override void FastMove() {}
	
  
}