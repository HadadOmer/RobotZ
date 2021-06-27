using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer:MonoBehaviour
{
    public float time;
    void Awake()
    {
        time = 0;
    }
    public void SetTimer(float time)
    {
        this.time = time;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       if(time>0)
        {           
            time -= Time.deltaTime;

            //Makes sure the timer is zeroed and not negative
            if (time < 0)
                time =0;
        }
    }
}
