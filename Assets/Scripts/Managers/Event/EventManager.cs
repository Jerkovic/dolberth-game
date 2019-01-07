using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, Action<IEventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There can only exist one active EventManger script on a GameObject.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    /// <summary>
    /// Init this instance.
    /// </summary>
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<IEventParam>>();
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
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stops a listener.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StopListening(string eventName, Action<IEventParam> listener)
    {
        if (eventManager == null) return;
        Action<IEventParam> thisEvent;
        if ((listener != null) && instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
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
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}