using Godot;

namespace TunaVsLion.scripts.meat.nonplayable;

public abstract class AbstractNonPlayableMeat: IMeat
{

    [Export] public int maxPopulation = 50;
    private int currentPop = 0;
    private Vector2 _bearing;
    private Vector2 _startingPos;
   
    
    public void Spawn()
    {
        if (currentPop < maxPopulation)
        {
            //if tile is land and not water ...
            
        }
    }

    private void _initiate()
    {
        for(var i = 0; i < this.maxPopulation; i++)
        {
            Spawn();
        }
        
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
	
    public void SetRandomBearing()
    {
        
        var direction = GD.Randi() % 20;

        _bearing = direction switch
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
	
    public void OnBearingTimerTimeout()
    {
        this.SetRandomBearing();
    }
  
    
      
    
}