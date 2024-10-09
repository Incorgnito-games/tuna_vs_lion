namespace TunaVsLion.scripts;
using Godot;
public class Global
{
    public static int WORLD_WIDTH = DisplayServer.ScreenGetSize().X;
    public  static int WORLD_HEIGHT = DisplayServer.ScreenGetSize().Y;

    public static Vector2 GetRandomPointOnLand()
    {
        return new Vector2(GD.RandRange(0,WORLD_WIDTH), GD.RandRange(0, WORLD_HEIGHT));
    }


}