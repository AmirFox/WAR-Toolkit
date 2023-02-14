using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;

public interface IObjective 
{
    ObjectiveType ObjectiveType { get; }
    bool IsComplete { get; }
    bool IsFailed { get; }
}
