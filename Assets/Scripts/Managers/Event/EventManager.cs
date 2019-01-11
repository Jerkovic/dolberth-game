using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, Action<IEventParam>> _eventDictionary;
    private static EventManager _eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError("There can only exist one  EventManger script on a GameObject.");
                }
                else
                {
                    _eventManager.Init();
                }
            }
            return _eventManager;
        }
    }

    /// <summary>
    /// Init this instance.
    /// </summary>
    void Init()
    {
        if (_eventDictionary == null)
        {
            _eventDictionary = new Dictionary<string, Action<IEventParam>>();
        }
    }

    /// <summary>
    /// Starts new listener.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StartListening(string eventName, Action<IEventParam> listener)
    {
        Action<IEventParam> thisEvent;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance._eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stops a listener.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StopListening(string eventName, Action<IEventParam> listener)
    {
        if (_eventManager == null) return;
        Action<IEventParam> thisEvent;
        if ((listener != null) && Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance._eventDictionary[eventName] = thisEvent;
        }
    }

    /// <summary>
    /// Triggers an event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="eventParam">Event parameter.</param>
    public static void TriggerEvent(string eventName, IEventParam eventParam)
    {
        Action<IEventParam> thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}