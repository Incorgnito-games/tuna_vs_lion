namespace TunaVsLion.scripts.components.state.lionStates;
using meat.playable;
using Godot;
public partial class LionIdle: State
{
    [Export] private Pride _currentPride;
    [Export] private Lion _lion;
    
    private int randSpeedMultiplier; 
    private float randRunWalkVelocity; 
    
    private Vector2 _newPos;

    private Timer randSpeedAdjustementTimer = new Timer();
    public override void Enter()
    {
        if (_currentPride is null)
        {
            return;
        }
        
        _newPos = Pride.GetRandomPointInPrideInfluence(_currentPride);
        GD.Print(_newPos);

        if (_lion is null)
        {
            return;
        }
        
        _idleRandomWalk();
    }

    public override void Exit()
    {
        // throw new System.NotImplementedException();
    }

    public override void UpdateProcess(double delta)
    {
        // throw new System.NotImplementedException();
    }
    
    public override void UpdatePhysicsProcess(double delta)
    {
        if (_lion is not null)
        {
            if(!_lion.IsPlayer)
                _idleRandomWalk();
        }
        
    }

    public override void _Ready()
    {
        //Timer Setup
        randSpeedAdjustementTimer.SetAutostart(true);
        randSpeedAdjustementTimer.SetWaitTime(GD.RandRange(1,8));
        randSpeedAdjustementTimer.Timeout += OnRandSpeedAdjustmentTimeout;
        AddChild(randSpeedAdjustementTimer);

    } 
    //*****************
    // Mechanics
    // ****************

    private void _idleRandomWalk()
    {
        
        var lionDirection = (_newPos - _lion.Position).Normalized();
        if (randRunWalkVelocity >= 0.2)
        {
            if (randSpeedMultiplier == 0)
            {
                randSpeedMultiplier = 1;
            }
            _lion.Velocity = lionDirection * _lion.BaseSpeed / randSpeedMultiplier;
        }
        else
        {
            _lion.Velocity = lionDirection * _lion.BaseSpeed * randSpeedMultiplier;
            
        }

        _lion.MoveAndSlide();
        if (_lion.Position.DistanceTo(_newPos) < 10f)
        {
            _newPos = Pride.GetRandomPointInPrideInfluence(_currentPride);
        }
        
    }
    
    //****************
    // Signal Callbacks
    //*****************
    public void OnRandSpeedAdjustmentTimeout()
    {
        randSpeedMultiplier = GD.RandRange(0,2);
        randRunWalkVelocity = GD.Randf();

    }
    
    //********************
    // Getters and Setters
    //********************

    public void SetPride(Pride pride)
    {
        this._currentPride = pride;
    }
}