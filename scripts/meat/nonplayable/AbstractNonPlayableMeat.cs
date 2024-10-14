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
    
}