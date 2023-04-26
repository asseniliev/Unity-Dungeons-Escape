using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager;

    public delegate void damageHandler(int target, int damage);
    public event damageHandler hit;

    public delegate void animationStartEnd(string animationName, string objectName);
    public event animationStartEnd animStart;
    public event animationStartEnd animEnd;

    public delegate void voidDelegate();
    public event voidDelegate flipSprite;

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


    public void CallAnimStartEvent(string animationName, string objectName)
    {
        animStart?.Invoke(animationName, objectName);
    }

    public void CallAnimEndEvent(string animationName, string objectName)
    {
        animEnd?.Invoke(animationName, objectName);
    }

    public void CallFlipSpriteEvent()
    {
        flipSprite?.Invoke();
    }
}
