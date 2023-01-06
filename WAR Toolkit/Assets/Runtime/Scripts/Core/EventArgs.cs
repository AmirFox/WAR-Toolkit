using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.ObjectData;

namespace WarToolkit.Core.EventArgs
{
    public class GameCompleteEventArgs
    {
        public IPlayer? Winner;
        public VictoryType VictoryType;
        public GameCompleteEventArgs(VictoryType victoryType, IPlayer winner = null)
        {
            Winner = winner;
            VictoryType = victoryType;
        }
    }

    public class GameStateChangedEventArgs
    {
        public IPlayer NewTeam;
        public IPlayer PrevTeam;
        public Phase NewPhase;
        public IPlayer FortOwner;

        public GameStateChangedEventArgs(Phase newPhase, IPlayer newTeam, IPlayer prevTeam = null, IPlayer fortOwner = null)
        {
            NewTeam = newTeam;
            PrevTeam = prevTeam;
            NewPhase = newPhase;
            FortOwner = fortOwner;
        }
    }
}