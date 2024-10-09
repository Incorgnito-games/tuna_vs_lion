using Godot;

namespace TunaVsLion.scripts.meat.nonplayable;

public abstract partial class AbstractNonPlayableMeat: CharacterBody2D, IMeat
{

    
	[Export] public double baseSpeed = 20;

    public virtual string toString()
    {
        return "NonplayableMeat";
    }
    public void Spawn()
    {
      
    }

    private void _initiate()
    {
      
        
    }
    public void SlowMove(double delta)
    {
    }

    public void FastMove()
    {
    }

    //********************************
    // Mechanic Logic
    //********************************
    
    public void Automate(Vector2 worldDim) { }
	
    
   
  
    
      
    
}