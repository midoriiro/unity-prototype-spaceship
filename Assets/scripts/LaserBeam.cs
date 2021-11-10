using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : AmmoRenderer
{
    private LineRenderer line_renderer;

    void Start()
    {
        base.Start();

        line_renderer = transform.GetComponent<LineRenderer>();
        line_renderer.positionCount = 2;
    }

    void Update()
    {
        line_renderer.SetPosition(0, weapon.position);
        line_renderer.SetPosition(1, weapon.position + weapon.up * life_time);
    }
}
