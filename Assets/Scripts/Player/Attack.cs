using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private EventManager eventManager;
    private void Awake()
    {
        this.eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        eventManager.CallCausedDamageEvent(other.gameObject.GetInstanceID(), 1);
    }
}
