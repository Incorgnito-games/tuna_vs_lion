using Godot;

namespace TunaVsLion.scripts.meat;

public interface IMeat
{
   void Spawn();
   void SlowMove(double delta);
   void FastMove();

   string toString();




}