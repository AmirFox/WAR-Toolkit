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

    public class UnitSelectedEventArgs : IArguements
    {
        public int OwningPlayerIndex { get; }
        public Movable MovableComponent{ get; }
        public Combatant CombatantComponent { get; }
        public UnitSelectedEventArgs(int playerIndex, Movable movable, Combatant combatant)
        {
            MovableComponent = movable;
            CombatantComponent = combatant;
            OwningPlayerIndex = playerIndex;
        }
    }

    public class SelectionEventArgs<T> : IArguements
    {
        public T Selection { get; }

        public SelectionEventArgs(T selection)
        {
            Selection = selection;
        }
    }

    public class GameCompleteEventArgs : IArguements
    {
        public Player? Winner;
        public VictoryType VictoryType;
        public GameCompleteEventArgs(VictoryType victoryType, Player winner = null)
        {
            Winner = winner;
            VictoryType = victoryType;
        }
    }
}