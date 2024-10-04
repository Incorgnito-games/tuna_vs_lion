
using System;
using Godot;

namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{

    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
    
    
	
    public void Initialize(int meatValue, double health)
    {
	    base.MeatValue = meatValue;
	    base.Health = health;
    }

    public override void SetSelected(bool isSelected)
    {
	    Selected = isSelected;
    }

    void Run()
    {
		// throw new NotImplementedException();
    }

    void Climb()
    {
		// throw new NotImplementedException();
        
    }

    void Swim()
    {
		// throw new NotImplementedException();
	    
    }

    void Hunt()
    {
		// throw new NotImplementedException();
	    
    }

    public override void Spawn()
    {
	    // throw new NotImplementedException();
    }

    // public override void SlowMove()
    // {
	   //  
    // }

    public override void FastMove()
    {
	    // throw new NotImplementedException();
    }

    // public override void Automate()
    // {
	   //
    // }
}