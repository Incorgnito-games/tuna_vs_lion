namespace TunaVsLion.scripts.meat.nonplayable;

using Godot;
using playable;


public abstract partial class AbstractNonPlayableMeat: AbstractMeat, IMeat
{

	[Export] public float FleeSpeedMultiplier = 1.5f;
	public AbstractPlayableMeat CurrentAttacker;
	public override void _Ready()
	{
		Init();
	}

	private void Init()
	{
		BaseSpeed = 20.0f;
		MaxStamina = 1000.0f;
		Stamina = MaxStamina;	
	}

	public virtual string toString()
    {
        return "NonplayableMeat";
    }
    
}