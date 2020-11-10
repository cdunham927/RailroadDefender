using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRenderer : MonoBehaviour
{
    public GameObject[] points;
    public float size = 2f;

    private void OnDrawGizmos()
    {
        if (points.Length > 0) 
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                //Draw line to next waypoint in path
                Gizmos.color = Color.green;
                Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
                //Draw line for which direction the path is facing
                //Gizmos.color = Color.red;
                //Gizmos.DrawLine(points[i].transform.position, transform.InverseTransformDirection(points[i].transform.forward));
            }
            //Gizmos.DrawLine(points[points.Length - 1].transform.position, points[points.Length - 1].transform.localRotation * Vector3.forward * size);
            Gizmos.color = Color.white;
        }
    }
}
