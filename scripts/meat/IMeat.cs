using Godot;

namespace TunaVsLion.scripts.meat;

public interface IMeat
{
   void Spawn();
   void SlowMove(Vector2 direction);
   void FastMove();
   void Automate(Vector2 worldDim);

   void RandomWalk(double delta);
}