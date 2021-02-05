using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarRotation : MonoBehaviour
{
    public BezierCurve curve;
    GameObject[] points;
    int currentPoint = 0;
    public Vector3 path;
    float distance;
    public float rotateSpd = 5f;

    //public GameObject nextCar;
    //Vector3 nextCarStartPos;

    private void Awake()
    {
        path = points[currentPoint].transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Rotate train towards news point on the path
        Vector3 targetDirection = path - transform.position;
        float singleStep = rotateSpd * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        distance = Vector3.Distance(transform.position, path);
        if (distance <= 0.1f)
        {
            //StopCoroutine(LerpPosition(path, spd));
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
