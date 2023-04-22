using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private EventManager eventManager;
    private void Awake()
    {
        this.eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        eventManager.CallHitEvent(other.gameObject.GetInstanceID(), damage);
    }
}
