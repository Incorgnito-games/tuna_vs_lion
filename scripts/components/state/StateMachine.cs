using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

namespace TunaVsLion.scripts.components.state;

public partial class StateMachine : Node
{
    public Dictionary StateDict = new Dictionary();

    public override void _Ready()
    {
        foreach (var child in this.GetChildren())
        {
            StateDict.Add(child.Name, child);
        }
        
    }
    
}