using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using Zenject;

namespace WarToolkit.Managers
{
    public interface ITurnManager
    {
        Phase CurrentPhase { get; }
        
        Player CurrentPlayer { get; }
        
        void NextPhase();

        void NextTurn();
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        #region EVENTS
        public event EventHandler<GameCompleteEventArgs> OnGameComplete;
        #endregion
        
        [Inject]
        private FactionData[] _factions;

        [field: SerializeField]
        private GameType _gameType;

        private List<Player> _players = new List<Player>();
        private int _currentPlayerIndex = 0;
        private int _currentTurn = 1;

        public Phase CurrentPhase { get; private set; } = Phase.DEPLOYMENT;

        public Player CurrentPlayer { get => _players[_currentPlayerIndex]; }

        [Inject]
        private IEventManager _eventManager;

        private void Awake()
        {
            foreach (FactionData factionData in _factions)
            {
                _players.Add(new Player(factionData));
            }
        }

        public void NextPhase()
        {
            //CurrentPlayer.ClearSelection();
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

            _eventManager.TriggerEvent(Constants.EventNames.GAME_STATE_CHANGED, new GameStateChangeArgs(_currentPlayerIndex, CurrentPhase));
        }

        public void NextTurn()
        {
            //ClearHighlight();
            //CurrentPlayer.ClearSelection();

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

            CurrentPhase = Phase.DEPLOYMENT;

            //CurrentPlayer.ResetState();

            _eventManager.TriggerEvent(Constants.EventNames.GAME_STATE_CHANGED, new GameStateChangeArgs(_currentPlayerIndex, CurrentPhase));
        }


        private GameCompleteEventArgs? CheckCaptureVictory(Player prevOwner, Player newOwner)
        {
            if (prevOwner == null || newOwner == null)
                return null;

            if (prevOwner == newOwner)
                return new GameCompleteEventArgs(VictoryType.CAPTURE, newOwner);

            return null;
        }

        private GameCompleteEventArgs CheckCombatVictory()
        {
            var remainingTeams = new List<Player>();
            var eliminatedTeams = new List<Player>();
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
        private IPlayer<Unit> GetFortressOwner()
        {
            var teamCounts = new Dictionary<IPlayer<Unit>, int>();
            for (var i = 0; i < _players.Count; i++)
                teamCounts.Add(_players[i], 0);

            var tiles = MapManager.instance.GetFortressTiles().Where(t => t.Occupier != null);
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
