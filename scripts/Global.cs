namespace TunaVsLion.scripts;
using Godot;
public class Global
{
    public static int WORLD_WIDTH = DisplayServer.ScreenGetSize().X;
    public  static int WORLD_HEIGHT = DisplayServer.ScreenGetSize().Y;
	public static int MeatMeter = 0;
	public static int Resources = 0;

    public static Vector2 GetRandomPointOnLand()
    {
        int gridSize = 5;
        int cols = WORLD_WIDTH / gridSize;
        int rows = WORLD_HEIGHT / gridSize;

        long randomCol = GD.Randi() % cols;
        long randomRow = GD.Randi() % rows;

        float x = randomCol * gridSize + GD.RandRange(0, gridSize);
        float y = randomRow * gridSize + GD.RandRange(0, gridSize);

        return new Vector2(x, y);
        
      //  return new Vector2(GD.RandRange(0,WORLD_WIDTH), GD.RandRange(0, WORLD_HEIGHT));
    }


}