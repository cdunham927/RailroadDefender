using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGunner : EnemyController
{
    public GameObject bullet;

    public override void OnEnable()
    {
        base.OnEnable();
        Invoke("GetTarget", 0.1f);
    }

    public override void GetTarget()
    {
        target = cont.GetRandomTarget(cont.playerTargets);
        //Debug.Log(target);
        curState = enemystates.chase;
    }

    public override void Damage(float amt)
    {
        hp -= amt;
        //Debug.Log(hp);

        if (hp <= 0) Die();
    }

    public override void Chase()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            distance = (transform.position - target.position).sqrMagnitude;
            float distanceX = Mathf.Abs(transform.position.x - target.position.x);
            float distanceY = Mathf.Abs(transform.position.y - target.position.y);

            //Aim at target
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            spriteObj.transform.rotation = Quaternion.Lerp(spriteObj.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.fixedDeltaTime * aimSpd);

            //Debug.Log("Current distance: " + distance + " / " + attackRange * attackRange);

                if (distanceX > xMin)
                {
                    if (target.position.x > transform.position.x)
                    {
                        bod.AddForce(Vector2.right * spd * Time.deltaTime);
                    }
                    else
                    {
                        bod.AddForce(Vector2.right * -spd * Time.deltaTime);
                    }
                }

                if (distanceY > yMin)
                {
                    if (target.position.y > transform.position.y)
                    {
                        bod.AddForce(Vector2.up * spd * Time.deltaTime);
                    }
                    else
                    {
                        bod.AddForce(Vector2.up * -spd * Time.deltaTime);
                    }
                }

            if (distance < attackRange * attackRange && attackCools <= 0)
            {
                curState = enemystates.attack;
            }
        }
    }

    public override void Attack() 
    {
        Instantiate(bullet, transform.position, spriteObj.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
        attackCools = attackSpd;
        curState = enemystates.chase;
    }

    public override void Retreat() 
    { 
        
    }

    public override void Leave() 
    { 
        
    }
}
