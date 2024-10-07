
using System;
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
    
    //**************************
    // Setup
    //**************************
    public void Initialize(int meatValue)
    {
	    base.MeatValue = meatValue;
	  
    }
    
    //***************************
    // Getters and Setters
    //***************************
    public string GetLionName()
    {
	    return this._name;
    }

    public void SetLionName(string name)
    { 
	    this._name = name;
    }
    
    public override void SetSelected(bool isSelected)
    {
	    Selected = isSelected;
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