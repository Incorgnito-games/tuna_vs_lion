namespace TunaVsLion.scripts.meat;

using Godot;


public abstract partial  class AbstractMeat: CharacterBody2D
{
	[Export] public float BaseSpeed;
    [Export] public float MaxStamina = 1000.0f;
    public float Stamina;
    [Export] public float Health = 100.0f;
	[Export] public int MeatValue = 1;
}