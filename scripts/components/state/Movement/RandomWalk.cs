namespace TunaVsLion.scripts.components.state.Movement;

using Godot;

using meat.nonplayable;
using meat.playable;

public partial class RandomWalk: State
{
    //character modifiers
    [Export] private AbstractNonPlayableMeat _meat;
    private Vector2 _currentBearing;
    [Export] private float _staminaIdleIncrease = 0.1f;
    
    //signal fields
    [Export] private Area2D _detectionArea;
    private Timer randSpeedAdjustementTimer = new Timer();
    
    public override void Enter()
    {
        if (_meat is null)
        {
            return;
        }
        _setRandomBearing();
    }

    public override void Exit()
    {
    }

    public override void UpdateProcess(double delta)
    {
    }
    public override void _Ready()
    {
        base._Ready();
        //state transition setup
        _detectionArea.BodyEntered += OnDetectionAreaBodyEntered;
        
        //Timer Setup
        randSpeedAdjustementTimer.SetAutostart(true);
        randSpeedAdjustementTimer.SetWaitTime(GD.RandRange(1,8));
        randSpeedAdjustementTimer.Timeout += OnBearingAdjustmentTimeout;
        AddChild(randSpeedAdjustementTimer);

        _currentBearing = new Vector2(GD.RandRange(-1,1),GD.RandRange(-1,1));
         // _meat.Velocity = _currentBearing * (float)_meat.baseSpeed;
    } 

    public override void UpdatePhysicsProcess(double delta)
    {
        if (_meat.Stamina < _meat.MaxStamina)
        {
            _meat.Stamina += _staminaIdleIncrease;
        }
        if (_meat is not null)
        {
            _randomWalk();
        }
    }

    private void _randomWalk()
    {
        _meat.Velocity = _currentBearing * _meat.BaseSpeed;

        _meat.MoveAndSlide();
    }
    private void _setRandomBearing()
    {
        
        var direction = GD.Randi() % 20;

        _currentBearing = direction switch
        {
            0 => new Vector2(0, -1), //up
            1 => new Vector2(0, 1), //down
            2 => new Vector2(-1, 0), //left
            3 => new Vector2(1, 0), //right
            4 => new Vector2(-1, -1), //up-left
            5 => new Vector2(1, -1), //up-right
            6 => new Vector2(-1, 1), //down-left
            7 => new Vector2(1, 1), //down-right
            _ => new Vector2(0, 0)
        };

        
    }
    
    //**************************
    // Signal Callbacks
    //**************************
	
    public void OnBearingAdjustmentTimeout()
    {
        this._setRandomBearing();
    }

    public void OnDetectionAreaBodyEntered(Node2D body)
    {
        if (body is null)
            return;
        
        if (body is AbstractPlayableMeat)
        {
			_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "flee");
        }
        
    }
}