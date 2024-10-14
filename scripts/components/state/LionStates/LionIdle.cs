namespace TunaVsLion.scripts.components.state.lionStates;

using System.Linq;
using Godot;

using meat.nonplayable;
using meat.playable;

public partial class LionIdle: State
{
    [Export] private Pride _currentPride;
    [Export] private Lion _lion;
    
    private int randSpeedMultiplier; 
    private float randRunWalkVelocity; 
    
    private Vector2 _newPos;
    private bool GetNewPos = false;
	private CustomStateSignals _stateTransitionSignal;

    [Export]private Area2D _detectionArea;

    private Timer randSpeedAdjustementTimer = new Timer();
    private Timer newPosTimer = new Timer();
    public override void Enter()
    {
        if (_currentPride is null)
        {
            return;
        }
        
        _newPos = Pride.GetRandomPointInPrideInfluence(_currentPride);
        // GD.Print(_newPos);

        if (_lion is null)
        {
            return;
        }
        
        GD.Print($"{_lion} ==> Idling!");
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
        _detectionArea.BodyEntered += OnDetectionAreaBodyEntered;
         
	    _stateTransitionSignal = GetNode<CustomStateSignals>("/root/CustomStateSignals");
        //Timer Setup
        randSpeedAdjustementTimer.SetAutostart(true);
        randSpeedAdjustementTimer.SetWaitTime(GD.RandRange(1,8));
        randSpeedAdjustementTimer.Timeout += OnRandSpeedAdjustmentTimeout;
        AddChild(randSpeedAdjustementTimer);
        
        //Timer Setup
        newPosTimer.SetAutostart(false);
        newPosTimer.SetWaitTime(10);
        newPosTimer.Timeout += OnNewPosTimeout;
        AddChild(newPosTimer);

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
        newPosTimer.Start();
        if (_lion.Position.DistanceTo(_newPos) < 10f || GetNewPos)
        {
            _newPos = Pride.GetRandomPointInPrideInfluence(_currentPride);
            GetNewPos = false;
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
    public void OnNewPosTimeout()
    {
        GetNewPos = true;
        

    }
    
    public void OnDetectionAreaBodyEntered(Node2D body)
    {
        if (_lion.IsPlayer)
        {
            return;
        }
        if (body is null)
            return;
	    
        if (body is AbstractNonPlayableMeat)
        {
            GD.Print($"{_lion} ==> Spotted Rabbit {body}");
            _lion.ChaseTargets.Add((AbstractNonPlayableMeat)body);
            string result = "[" + string.Join(", ", _lion.ChaseTargets.Select(node => node.ToString())) + "]";
            GD.Print($"{_lion} Targetlist ==> {result}");
            _lion.CurrentTarget = _lion.GetClosetTarget(); 
            GD.Print($"{_lion} ==> current target ==> {_lion.CurrentTarget}");
            _stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), this, "chase");
		   
        }

        if (_lion.ChaseTargets.Count > 0)
        {
        }	
    }
    
    
    //********************
    // Getters and Setters
    //********************
    public void SetPride(Pride pride)
    {
        this._currentPride = pride;
        
    }
}