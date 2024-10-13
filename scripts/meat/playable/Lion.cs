
using TunaVsLion.scripts.components;
using Godot;
using TunaVsLion.scripts.meat.nonplayable;
using System.Collections.Generic;
using System.Linq;


namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{
	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	[Export] public double Health;

	//signals
	protected AttackBox _attackBox;
	
	//Debug Fields
    
    //**************************
    // Setup
    //**************************

    public override void _Ready()
    {
	    base._Ready();
	    _attackBox = GetNode<AttackBox>("AttackBox");
	    _attackBox.BodyEntered += OnAttackBoxBodyEntered;
	    
    }
    
    //***************************
    // Getters and Setters
    //***************************

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