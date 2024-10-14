using TunaVsLion.scripts.meat.nonplayable;

namespace TunaVsLion.scripts.components.state;
using Godot;

public partial class CustomStateSignals : Node
{
    [Signal]
    public delegate void TransitionStateEventHandler(State state, string stateName);


}