using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;

namespace WarToolkit.Controllers
{
    public class TurnController : MonoBehaviour
    {
        #region EVENTS
        public event EventHandler<GameStateChangedEventArgs> OnGameStateChanged;
        public event EventHandler<GameCompleteEventArgs> OnGameComplete;
        #endregion
        
        [Tooltip("Static data defining the factions currently playing.")]
        [field: SerializeField]
        private FactionData[] _factions;

        [field: SerializeField]
        private GameType _gameType;

        private List<IPlayer> _players = new List<IPlayer>();
        private int _currentPlayerIndex = 0;
        private int _currentTurn = 1;

        public Phase CurrentPhase { get; private set; } = Phase.DEPLOYMENT;

        public IPlayer CurrentPlayer { get => _players[_currentPlayerIndex]; }

        private void Awake()
        {
            foreach (FactionData factionData in _factions)
            {
                _players.Add(new Player<Unit>(factionData));
            }
        }

        public void NextPhase()
        {
            CurrentPlayer.ClearSelection();
            switch (CurrentPhase)
            {
                case Phase.DEPLOYMENT:
                    CurrentPhase = Phase.MOVEMENT;
                    break;
                case Phase.MOVEMENT:
                    CurrentPhase = Phase.COMBAT;
                    break;
                case Phase.COMBAT:
                    NextTurn();
                    break;
            }
            OnGameStateChanged.Invoke(this, new GameStateChangedEventArgs(CurrentPhase, CurrentPlayer));
        }

        public void NextTurn()
        {
            ClearHighlight();
            CurrentPlayer.ClearSelection();

            //ResolveBattles();
            
            /* 
            if (_gameType == GameType.SEIGE)
            {
                var prevOwner = _fortressOwner;
                var newOwner = GetFortressOwner();

                var captureVictoryResult = CheckCaptureVictory(prevOwner, newOwner);
                if (captureVictoryResult != null)
                {
                    OnGameComplete.Invoke(this, captureVictoryResult);
                    return;
                }
                _fortressOwner = newOwner;
            } 
            */

            var combatVictoryResult = CheckCombatVictory();
            if (combatVictoryResult != null)
            {
                OnGameComplete.Invoke(this, combatVictoryResult);
                return;
            }

            int prevPlayerIndex = _currentPlayerIndex;

            if (_currentPlayerIndex + 1 >= _players.Count)
            {
                _currentPlayerIndex = 0;
            }
            else
            {
                _currentPlayerIndex++;
            }

            CurrentPhase = CurrentPlayer.CanDeploy() ? Phase.DEPLOYMENT : Phase.MOVEMENT;

            CurrentPlayer.ResetState();

            OnGameStateChanged.Invoke(this, new GameStateChangedEventArgs(CurrentPhase, CurrentPlayer, _players[prevPlayerIndex], _fortressOwner));
        }

        public int CheckForWinner()
        {
            return -1;
        }


        public GameCompleteEventArgs? CheckCaptureVictory(IPlayer prevOwner, IPlayer newOwner)
        {
            if (prevOwner == null || newOwner == null)
                return null;

            if (prevOwner == newOwner)
                return new GameCompleteEventArgs(VictoryType.CAPTURE, newOwner);

            return null;
        }

        public GameCompleteEventArgs CheckCombatVictory()
        {
            var remainingTeams = new List<IPlayer>();
            var eliminatedTeams = new List<IPlayer>();
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].IsEliminated)
                    eliminatedTeams.Add(_players[i]);
                else
                    remainingTeams.Add(_players[i]);
            }

            if (remainingTeams.Count == 1)
                return new GameCompleteEventArgs(VictoryType.DESTRUCTION, remainingTeams[0]);
            else if (eliminatedTeams.Count == _players.Count)
                return new GameCompleteEventArgs(VictoryType.MUTUAL_DESTRUCTION);
            else
                return null;
        }
        
        /* 
        public IPlayer<Unit> GetFortressOwner()
        {
            var teamCounts = new Dictionary<IPlayer<Unit>, int>();
            for (var i = 0; i < _players.Count; i++)
                teamCounts.Add(_players[i], 0);

            var tiles = MapController.instance.GetFortressTiles().Where(t => t.Occupier != null);
            foreach (var tile in tiles)
            {
                teamCounts[tile.Occupier.Team]++;
            }

            var occupyingTeams = teamCounts.Where(t => t.Value > 0).Select(t => t.Key).ToList();

            if (occupyingTeams.Count == 1)
                return occupyingTeams[0];

            return null;
        }
         */
    }
}
