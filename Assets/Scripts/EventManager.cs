using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager;

    public delegate void damageHandler(int target, int damage);
    public event damageHandler hit;

    private void Awake()
    {
        if (EventManager.eventManager == null)
            EventManager.eventManager = this;
        else
        {
            if(EventManager.eventManager != this)
            {
                Destroy(EventManager.eventManager.gameObject);
                EventManager.eventManager = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void CallHitEvent(int target, int damage)
    {
        hit?.Invoke(target, damage);
    }
}
