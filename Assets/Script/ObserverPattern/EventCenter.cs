using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : MonoBehaviour
{
    private static EventCenter instance;
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();
    public static EventCenter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<EventCenter>();
                if (instance == null)
                {
                    var go = new GameObject("EventCenter");
                    instance = go.AddComponent<EventCenter>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            _eventDic.Add(name, new EventInfo<T>(action));
        }
    }
    public void EventTrigger<T>(string name, T info)
    {
        if (_eventDic.TryGetValue(name, out var eventInfo))
        {
            if (eventInfo is EventInfo<T> infoT && infoT.actions != null)
            {
                infoT.actions.Invoke(info);
            }
            else
            {
                Debug.LogError($"EventCenter: El tipo de evento para '{name}' no coincide. Esperado: {eventInfo.GetType()}, Usado: {typeof(EventInfo<T>)}");
            }
        }
    }
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.TryGetValue(name, out var eventInfo) && eventInfo is EventInfo<T> info)
        {
            info.actions -= action;
            if (info.actions == null)
            {
                _eventDic.Remove(name);
            }
        }
    }
    public void Clear()
    {
        _eventDic.Clear();
    }

    public void DebugAllEventNames()
    {
    }
    private void Update()
    {
        DebugAllEventNames();
    }
    public interface IEventInfo
    {
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
}
