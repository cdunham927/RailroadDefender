using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    public float bgSpd;
    public Renderer rend;


    // Update is called once per frame
    void Update()
    {
        rend.material.mainTextureOffset += new Vector2(bgSpd * Time.deltaTime, 0f);
    }
}
