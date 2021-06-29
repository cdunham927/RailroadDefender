using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : EnemyController
{
    public float timeToTake;
    [SerializeField]
    float totalTime;
    [SerializeField]
    float curTime;
    Crate t;

    public override void OnEnable()
    {
        base.OnEnable();
        Invoke("GetTarget", 0.5f);
    }

    public override void GetTarget()
    {
        target = cont.GetRandomTarget(cont.trainTargets);
        //Debug.Log(target);
        curState = enemystates.chase;
        t = target.GetComponent<Crate>();
        totalTime = timeToTake * target.GetComponent<Crate>().weight;
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
                curTime = 0;
                curState = enemystates.attack;
            }
        }
    }

    public override void Attack()
    {
        //Start taking the crate
        if (curTime < totalTime) curTime += Time.deltaTime;
        else
        {
            Cargo car = target.GetComponent<Crate>().GetCargo();
            car.TakeCrate(t);
            //Change state to leave the map
            curState = enemystates.retreat;
        }
    }

    public override void Retreat()
    {

    }

    public override void Leave()
    {
        //Take crate
        target.GetComponent<Crate>().Damage();
    }
}
