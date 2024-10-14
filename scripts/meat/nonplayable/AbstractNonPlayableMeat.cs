namespace TunaVsLion.scripts.meat.nonplayable;

using Godot;
using playable;


public abstract partial class AbstractNonPlayableMeat: CharacterBody2D, IMeat
{

	[Export] public float BaseSpeed = 20.0f;
	[Export] public int MeatValue = 1;
	[Export] public float Stamina = 100.0f;
	[Export] public float FleeSpeedMultiplier = 1.5f;
	public AbstractPlayableMeat CurrentAttacker;
    public virtual string toString()
    {
        return "NonplayableMeat";
    }
    
}