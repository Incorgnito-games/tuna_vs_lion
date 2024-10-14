using Godot;
using TunaVsLion.scripts.meat;

namespace TunaVsLion.scripts.components.state.Movement;


public partial class Rest : State
{
    [Export]private AbstractMeat _meat;
    [Export] private int StaminaReplenishBase = 10;
    private Timer _staminaRecoverTimer = new Timer();
    private float _staminaReplenishMultiplier = 1.5f;
    
    
    public override void Enter()
    {
        GD.Print($"{_meat} ==> resting");
        _staminaRecoverTimer.Start();
    }

    public override void Exit()
    {
        //do i need to free instantiated nodes on exit like timer or are they garbage collected--probly arnt <-- LOOK INTO
        _staminaRecoverTimer.Stop();
    }

    public override void _Ready()
    {
        base._Ready();
        //Timer Setup
        _staminaRecoverTimer.SetAutostart(false);
        _staminaRecoverTimer.SetWaitTime(1);
        _staminaRecoverTimer.Timeout += OnStaminaRecoverTimeout;
        AddChild(_staminaRecoverTimer);
    }
    public override void UpdateProcess(double delta)
    {
    }

    public void recoverStamina()
    {
        if (_meat.Stamina >= _meat.MaxStamina)
        {
            _stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "idle");
        }
        _meat.Stamina += StaminaReplenishBase * _staminaReplenishMultiplier;
    }
    public override void UpdatePhysicsProcess(double delta)
    {
        _meat.Velocity = Vector2.Zero;
    }

    public void OnStaminaRecoverTimeout()
    {
        recoverStamina();
    }
}