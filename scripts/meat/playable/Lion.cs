
using TunaVsLion.scripts.components;
using Godot;
using System.Linq;
using TunaVsLion.scripts.components.state;
using TunaVsLion.scripts.meat.nonplayable;

namespace TunaVsLion.scripts.meat.playable;

public partial class Lion: AbstractPlayableMeat
{
	//signals
	// private CustomStateSignals _stateSignal;

	//Character Fields
    [Export] public int HideLevel = 1;
    [Export] public int ClimbLevel = 1;
    [Export] public int SwimLevel = 1;
	[Export] public double Health;

	//State fields
	private State _idleState;
	//Debug Fields
	
	public string CharacterName { get; set; }
	protected Label _nameLabel;
    	protected Label _meatMeter;
    
    //**************************
    // Setup
    //**************************

    public override void _Ready()
    {
		//might be in the wrong scope, maybe move to concrete class??
		_chaseState = GetNode<State>("StateMachine/Chase");
		_stateTransitionSignal = GetNode<CustomStateSignals>("/root/CustomStateSignals");
	    _idleState = GetNode<State>("StateMachine/LionIdle");
	    _attackBox = GetNode<AttackBox>("AttackBox");
	    _detectionArea = GetNode < DetectionArea>("DetectionArea");

	    _attackBox.BodyEntered += OnAttackBoxBodyEntered;
	    _detectionArea.BodyEntered += OnDetectionAreaBodyEntered;
	    _detectionArea.BodyExited += OnBodyExitedDetectionArea;
	   
		 //play hud labeling
	    _nameLabel = GetNode<Label>("nameLabel"); 
	    _meatMeter = GetNode<Label>("meatMeter");
	    
		Initialize();
    }
	public override void _Process(double delta)
    {
	    if(_meatMeter is not null)
			_meatMeter.Text = MeatValue.ToString();
    }

    public virtual void Initialize()
    {
	    _nameLabel.Text = CharacterName;
	    _meatMeter.Text = MeatValue.ToString();
    }
    
    //***************************
    // Getters and Setters
    //***************************
    public override string  toString()
    {
	    return this.CharacterName;
    }

    
    //*******************************
    // Signals
    //*******************************
    public void OnAttackBoxBodyEntered(Node2D body)
    {
	    // GD.Print($"{_name}==> lets dance {((Lion)body).toString()}!");
	    //naive and doesnt make sense
	    if (body is Rabbit)
	    {
		    MeatValue++;
		    GD.Print($"{CharacterName} ==> Chomp!");
	    }
    }

    public void OnDetectionAreaBodyEntered(Node2D body)
    {
	    if (IsPlayer)
	    {
		    return;
	    }
	    if (body is null)
		    return;
	    
	    if (body is Rabbit)
	    {
		    GD.Print("Rabbit Spotted");
		    _chaseTargets.Add((AbstractNonPlayableMeat)body);
		    string result = "[" + string.Join(", ", _chaseTargets.Select(node => node.Name)) + "]";
		    GD.Print(result);
		   
	    }

	    if (_chaseTargets.Count > 0)
	    {
			CurrentTarget = _chaseTargets.OrderBy(vec => Position.DistanceTo(vec.Position)).First();
			GD.Print($"current target ==> {CurrentTarget.Name}");
	    }	
	    _stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), _idleState, "Chase");
	     // GD.Print($"{CharacterName} ==> Chase!");
    }

    private void OnBodyExitedDetectionArea(Node2D body)
    {
	    if (body is null)
		    return;

	    if (_chaseTargets.Count == 0)
	    {
			_stateTransitionSignal.EmitSignal(nameof(CustomStateSignals.TransitionState), _chaseState, "LionIdle");
		    
	    }

	    if (_chaseTargets.Contains(body))
	    {
		    _chaseTargets.Remove((AbstractNonPlayableMeat)body);
	    }

	    // if (CurrentTarget.GetInstanceId() == body.GetInstanceId())
	    // {
		   //  CurrentTarget = null;
	    // }
	    
    }
	
    
    //need to update interface as these will move to state
    //********************************
    // Mechanics Logic
    //********************************
    void Run() {}
    void Climb() {}
    void Swim() {}
    void Hunt() {}
    public override void Spawn() {}
	public override void FastMove() {}
	
  
}