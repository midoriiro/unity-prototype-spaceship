using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class TailEngine : MonoBehaviour {
    public int size = 50;
    private float sample_cooldown;
    private float sample_period = 0.025f;
    private Queue<Vector3> points;
    private new LineRenderer renderer;
    private GameObject trail_start;

    void Start ()
    {
        renderer = GetComponent<LineRenderer>(); 
        points = new Queue<Vector3>(size);
    }

    void Update()
    {
        if(points.Count >= size)
        {
            points.Dequeue();
        }

        if(Time.time >= sample_cooldown)
        {
            points.Enqueue(transform.position);

            sample_cooldown = Time.time + sample_period;
        }

        List<Vector3> list = new List<Vector3>(points.ToArray());

        List<Vector3> positions = new List<Vector3>(list.Count);

        for(int i = 0 ; i < list.Count ; i = i + 3)
        {
            float t = (i) / ( list.Count - 1.0f );

            if(i + 2 >= list.Count)
                break;

            Vector3 p0 = list[i];
            Vector3 p1 = list[i + 1];
            Vector3 p2 = list[i + 2];

            positions.Add(MathHelper.GetQuadraticVector3(t, p0, p1, p2));
        }

        renderer.positionCount = positions.Count;
        renderer.SetPositions(positions.ToArray());
    }

    
}
