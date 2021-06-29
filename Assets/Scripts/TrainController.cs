using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour, IDamageable<float>
{
    public FloatVariable maxHp;
    float hp;
    float iframes;

    public BezierCurve curve;
    public GameObject[] points;
    int currentPoint = 0;
    public float spd;
    public Vector3 path;
    public float percentPoint = 0;
    float distance;
    public float rotateSpd = 5f;

    Rigidbody bod;

    void Awake()
    {
        bod = GetComponent<Rigidbody>();
        hp = maxHp.Value;
        //points = curve.GetAnchorPoints();
        path = points[currentPoint].transform.position;
        //StartCoroutine(LerpPosition(path, spd));
    }

    IEnumerator LerpPosition(Vector3 pos, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, pos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = pos;
    }

    void Update()
    {
        if (currentPoint < points.Length - 1) path = points[currentPoint].transform.position;

        if (iframes > 0) iframes -= Time.deltaTime;
        //transform.position = Vector3.Lerp(transform.position, path, spd * Time.deltaTime);
        //Quaternion rotDest = Quaternion.LookRotation(path);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotDest, rotateSpd * Time.deltaTime);

        //Rotate train towards news point on the path
        Vector3 targetDirection = new Vector3(path.x, transform.position.y, path.z) - transform.position;
        float singleStep = rotateSpd * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        distance = Vector3.Distance(transform.position, path);

        if (distance <= 0.1f)
        {
            //StopCoroutine(LerpPosition(path, spd));
            //currentPoint = (currentPoint + 1) % points.Length;
            //StartCoroutine(LerpPosition(path, spd));
            //Debug.Log(currentPoint);
        }

        //transform.position = Vector3.MoveTowards(transform.position, path, spd * Time.deltaTime);
        bod.AddForce(transform.forward * spd);
    }

    public void Kill()
    {

    }

    public void Damage(float amt)
    {
        if (iframes <= 0)
        {
            hp -= amt;

            if (hp <= 0)
            {
                Kill();
            }

            iframes = 0.01f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
