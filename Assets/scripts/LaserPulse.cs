using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPulse : AmmoRenderer
{
	void Start ()
    {
        base.Start();

        body.AddForce(body.transform.up * speed);
    }

	void FixedUpdate ()
    {
        float distance = life_time * thruster.GetMaxSpeed();

        if(Vector3.Distance(start, transform.position) >= distance)
            Destroy(transform.gameObject);
    }
}
