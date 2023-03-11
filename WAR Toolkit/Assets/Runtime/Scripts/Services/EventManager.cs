using System;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;

public interface IEventManager
{
    void StartListening(string eventName, Action<IArguements> listener);
    void StopListening(string eventName, Action<IArguements> listener);
    void TriggerEvent(string eventName, IArguements message);
}

public class EventManager : IEventManager
{
  private Dictionary<string, Action<IArguements>> eventDictionary = new Dictionary<string, Action<IArguements>>();

  public void StartListening(string eventName, Action<IArguements> listener) {
    Action<IArguements> thisEvent;
    
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent += listener;
      eventDictionary[eventName] = thisEvent;
    } else {
      thisEvent += listener;
      eventDictionary.Add(eventName, thisEvent);
    }
  }

  public void StopListening(string eventName, Action<IArguements> listener) {
    Action<IArguements> thisEvent;
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent -= listener;
      eventDictionary[eventName] = thisEvent;
    }
  }

  public void TriggerEvent(string eventName, IArguements message) {
    Action<IArguements> thisEvent = null;
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent.Invoke(message);
    }
  }
}

public class Constants
{
    public readonly struct EventNames
    {
        public const string TILE_SELECTED = "tile selected";
        public const string GAME_STATE_CHANGED = "game state changed";
        public const string MOVABLE_SELECTED = "movable selected";
        public const string DEPLOYABLE_SELECTED = "deployable selected";
        public const string COMBATANT_SELECTED = "combatant selected";
    }
}