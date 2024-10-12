using Godot;

namespace TunaVsLion.scripts.meat.nonplayable;

public abstract partial class AbstractNonPlayableMeat: CharacterBody2D, IMeat
{

    
	[Export] public double baseSpeed = 20;
	[Export] public int MeatValue = 1;
	[Export] public float Stamina = 100.0f;
	[Export] public float FleeSpeedMultiplier = 1.5f;
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