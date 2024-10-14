
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
	protected Area2D _attackBox;
	
	//Debug Fields
    
    //**************************
    // Setup
    //**************************

    //***************************
    // Getters and Setters
    //***************************

    //*******************************
    // Mechanics
    //*******************************
  
    //********************************
    // Signal callbacks
    //********************************
  
 
}