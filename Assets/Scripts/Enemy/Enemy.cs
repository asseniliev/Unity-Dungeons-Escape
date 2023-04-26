using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected float distanceForCombat;

    private Vector3 moveTarget;
    private bool isInCombat = false;
    private bool isBeingHit = false;
    private bool canMove = false;
    private Enemy_Animation enemyAnimation;
    private float idleAnimLen;
    private float hitAnimLen;
    private EventManager eventManager;
    private int instanceId;
    private Transform player;

    private void Awake()
    {
        this.eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()    {
        
        this.eventManager.hit += TakeDamage;
        this.eventManager.animStart += SetCanMoveFalse;
        this.eventManager.animEnd += SetCanMoveTrue;
        this.eventManager.flipSprite += FlipSprite;
    }

    private void OnDisable()
    {
        this.eventManager.hit -= TakeDamage;
        this.eventManager.animStart -= SetCanMoveFalse;
        this.eventManager.animEnd -= SetCanMoveTrue;
        this.eventManager.flipSprite -= FlipSprite;
    }

    public virtual void Start()
    {
        this.instanceId = this.gameObject.GetInstanceID();
        //Debug.Log(this.gameObject.name + " - instance id: " + this.instanceId);
        this.moveTarget = this.pointB.position;
        this.enemyAnimation = this.GetComponent<Enemy_Animation>();
        this.idleAnimLen = this.enemyAnimation.GetIdleAnimationLength();
        this.hitAnimLen = this.enemyAnimation.GetHitAnimationLength();
        //StartCoroutine(WalkCycle());
    }

    public virtual void Update()
    {
        if (this.canMove) Move();

        CheckForTargetPositionReached();

        CheckIfPlayerAround();
    }


    private void CheckForTargetPositionReached()
    {
        if (IsTargetPositionReached())
        {
            SwapMoveTarget();
            this.enemyAnimation.PlayIdle();
        }
    }


    private void SwapMoveTarget()
    {
        if (this.moveTarget == this.pointA.position)
            this.moveTarget = this.pointB.position;
        else
            this.moveTarget = this.pointA.position;
    }

    public virtual void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.moveTarget, this.speed * Time.deltaTime);
    }

    private bool IsTargetPositionReached()
    {
        return Vector3.Distance(this.transform.position, this.moveTarget) < 0.01f;
    }

    public virtual void Attack()
    {
        Debug.Log(this.transform.name + " is attacking");
    }

    //IEnumerator WalkCycle()
    //{
    //    while (!this.isInCombat && !this.isBeingHit)
    //    {
    //        //Debug.Log("Step1: Play idle animation");            
    //        yield return new WaitForSeconds(idleAnimLen);
    //        //yield return new WaitForSeconds(0.06f);

    //        //Debug.Log("Step2: Trigger move animation");
    //        this.enemyAnimation.PlayMove();
    //        yield return new WaitForSeconds(0.06f);
    //        this.mustMove = true;

    //        //Debug.Log("Step3: do nothing while moving");
    //        while (!IsTargetPositionReached() || this.isInCombat) yield return null;

    //        //Debug.Log("Step4: trigger idle animation and wait until it is played");
    //        this.enemyAnimation.PlayIdle();
    //        this.mustMove = false;
    //        yield return new WaitForSeconds(idleAnimLen);

    //        //Debug.Log("Step5: Flip sprite and re-trigger idle animation");
    //        this.enemySprite.flipX = !this.enemySprite.flipX;
    //        this.enemyAnimation.PlayMove();
    //        this.enemyAnimation.PlayIdle();
    //        yield return new WaitForSeconds(0.05f);

    //        //Debug.Log("Step6: Swap target positions");
    //        SwapMoveTarget();
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}

    public virtual void TakeDamage(int targetId, int damageAmount)
    {
        if(this.instanceId == targetId)
        {            
            this.health -= damageAmount;
            if (this.health <= 0) 
                Die();
            else
            {
                this.canMove = false;
                this.enemyAnimation.PlayHit();
            }   
        }
    }

    public void CheckIfPlayerAround()
    {
        if(Mathf.Abs(this.transform.position.x - this.player.position.x) < this.distanceForCombat)
        {
            this.isInCombat = true;
            this.enemyAnimation.SetInCombatMode(true);
        }
        else
        {
            this.isInCombat = false;
            this.enemyAnimation.SetInCombatMode(false);
        }
    }

    public void Die()
    {
        Debug.Log(this.gameObject.name + " is dead");
        Destroy(this.gameObject, 2);
    }

    //private IEnumerator hitMode()
    //{
    //    this.isBeingHit = true;
    //    yield return new WaitForSeconds(this.hitAnimLen);
    //    //yield return new WaitForSeconds(this.idleAnimLen);
    //    this.isBeingHit = false;
    //}

    private void SetCanMoveTrue(string amimationName, string objectName)
    {
        if(this.transform.name == objectName && amimationName == "Idle")
            this.canMove = true;
    }

    private void SetCanMoveFalse(string amimationName, string objectName)
    {
        if (this.transform.name == objectName && amimationName == "Idle")
            this.canMove = false;
    }

    private void FlipSprite()
    {
        this.enemySprite.flipX = !this.enemySprite.flipX;
    }
}
