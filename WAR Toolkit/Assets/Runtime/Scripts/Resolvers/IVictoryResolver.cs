using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.ObjectData;

public interface IVictoryResolver
{
    bool TryGetVictor(IPlayer[] players, out IPlayer victor);
}

public class EliminationVictoryResolver : IVictoryResolver
{
    public bool TryGetVictor(IPlayer[] players, out IPlayer victor)
    {
        victor = default;
        List<IPlayer> remaining = players.Where(player=>!player.IsEliminated).ToList();
        
        if (remaining.Count == 1)
        {
            victor = remaining[0];
            return true;
        }
        else if(remaining.Count == 0)
        {
            //mutual destruction
            return true;
        }

        return false;
    }
}

