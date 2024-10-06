using Godot;

namespace TunaVsLion.scripts.meat;

public interface IMeat
{
   void Spawn();
   void SlowMove(double delta);
   void FastMove();
   void Automate(Vector2 worldDim);

   void SetRandomBearing();

   
}