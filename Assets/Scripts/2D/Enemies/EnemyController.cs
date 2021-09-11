using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable<float>, IKillable
{
    public float maxHp;
    protected float hp;
    public float spd;
    public enum enemystates { chase, attack, retreat, leave }
    protected enemystates curState;
    //public GameObjectList targets;
    [SerializeField]
    protected Transform target;
    protected GameController cont;
    protected Rigidbody2D bod;

    //More stats
    public float attackRange;
    public float attackSpd;
    protected float attackCools;
    public float aimSpd;

    //UI
    public GameObject uiParent;
    public Image healthImg;
    public float uiLerp;

    public GameObject spriteObj;

    protected float distance;
    public float xMin;
    public float yMin;
    public float accuracy;

    //Death
    //public AnimationClip deathClip;
    public float deathTime = 0f;

    bool deaded = false;

    private void Awake()
    {
        //startRot = uiParent.transform.rotation;
        //deathTime = deathClip.length;
        bod = GetComponent<Rigidbody2D>();
        cont = FindObjectOfType<GameController>();
    }

    public void ChangeState(enemystates nextState)
    {
        curState = nextState;
    }

    public virtual void OnEnable()
    {
        cont.DeadEnemy(1, gameObject);
        deaded = false;
        hp = maxHp;
        //curState = enemystates.chase;
        //GetTarget();
    }

    public virtual void GetTarget() { }

    private void Update()
    {
        switch(curState)
        {
            case enemystates.chase:
                Chase();
                break;
            case enemystates.attack:
                Attack();
                break;
            case enemystates.retreat:
                Retreat();
                break;
            case enemystates.leave:
                Leave();
                break;
        }

        if (attackCools > 0) attackCools -= Time.deltaTime;
        healthImg.fillAmount = Mathf.Lerp(healthImg.fillAmount, hp / maxHp, uiLerp * Time.deltaTime);
    }

    void LateUpdate()
    {
        //uiParent.transform.rotation = startRot;
        uiParent.transform.rotation = Quaternion.identity;
    }

    public virtual void Damage(float amt) { }

    public void Die()
    {
        Invoke("Disable", deathTime);
    }

    void OnDisable()
    {
        GameController cont = FindObjectOfType<GameController>();
        if (cont != null) cont.DeadEnemy(-1, gameObject);
        CancelInvoke();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public virtual void Chase() { }
    public virtual void Attack() { }
    public virtual void Retreat() { }
    public virtual void Leave() { }
}
