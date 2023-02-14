using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.Core.Enums
{
    public enum Direction { NORTH, EAST, SOUTH, WEST }

    public enum VictoryType { DESTRUCTION, MUTUAL_DESTRUCTION, CAPTURE}
    
    public enum GameType { SEIGE, ELIMINATION, DOMINATION }

    public enum Phase { DEPLOYMENT, MOVEMENT, COMBAT }

    public enum ObjectiveType { CAPTURE, DESTROY, SURVIVE, HOLD }
}
