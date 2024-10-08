
using TunaVsLion.scripts.components;
using Godot;

namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{

	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	[Export] public double Health = 100;

	//Debug Fields
    private string _name;
	private Label nameLabel;
    
    //**************************
    // Setup
    //**************************

    public override void _Ready()
    {
	    attackBox = GetNode<AttackBox>("AttackBox");
	    detectionArea = GetNode < DetectionArea>("DetectionArea");
	    nameLabel = GetNode<Label>("nameLabel");
	    
			nameLabel.Text = _name;
	    

	    attackBox.BodyEntered += OnAttackBoxBodyEntered;
	    detectionArea.BodyEntered += OnDetectionAreaBodyEntered; 
	    Initialize(); 
	    
    }
    public void Initialize()
    {
	    
	  
	  
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
	    nameLabel.Text = name;
    }
    
    public override void SetSelected(bool isSelected)
    {
	    Selected = isSelected;
    }
    
    //*******************************
    // Signals
    //*******************************
    public void OnAttackBoxBodyEntered(Node2D body)
    {
	    // GD.Print($"{_name}==> lets dance {((Lion)body).toString()}!");
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