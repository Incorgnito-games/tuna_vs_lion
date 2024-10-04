using Godot;

namespace TunaVsLion.scripts.meat;

public interface IMeat
{
   void Spawn();
   void SlowMove();
   void FastMove();
   void Automate();
}