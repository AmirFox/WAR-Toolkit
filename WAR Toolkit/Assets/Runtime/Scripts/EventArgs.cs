using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.ObjectData;

namespace WarToolkit.Core.EventArgs
{
    public interface IArguements { }

    public class GameStateChangeArgs : IArguements
    {
        public int NewPlayerIndex { get; }
        public Phase NewPhase { get; }

        public GameStateChangeArgs(int newPlayerIndex, Phase newPhase)
        {
            NewPlayerIndex = newPlayerIndex;
            NewPhase = newPhase;
        }
    }

    public class TileSelectedEventArgs : IArguements
    {
        public Vector2 position;
        
        public TileSelectedEventArgs(Vector2 position)
        {
            this.position = position;
        }
    }

    public class DeployableSelectedEventArgs : IArguements
    {
        public int playerIndex { get; }
        public int deployableIndex { get; }

        public DeployableSelectedEventArgs(int playerIndex, int deployableIndex)
        {
            this.playerIndex = playerIndex;
            this.deployableIndex = deployableIndex;
        }
    }

    public class GameCompleteEventArgs : IArguements
    {
        public IPlayer? Winner;
        public VictoryType VictoryType;
        public GameCompleteEventArgs(VictoryType victoryType, IPlayer winner = null)
        {
            Winner = winner;
            VictoryType = victoryType;
        }
    }
}