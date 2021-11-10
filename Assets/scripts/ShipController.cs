using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private ThrusterEngine[] thrusters;

	void Start ()
    {
        thrusters = transform.GetComponentsInChildren<ThrusterEngine>();
        NormalizeThrusters();
	}

	void Update ()
    {
		
	}

    public ThrusterEngine[] GetMainThrusters()
    {
        List<ThrusterEngine> result = new List<ThrusterEngine>();

        foreach(ThrusterEngine thruster in thrusters)
        {
            if (thruster.direction == ThrusterEngine.Direction.South)
                result.Add(thruster);
        }

        return result.ToArray();
    }

    public ThrusterEngine GetMainThruster()
    {
        return GetMainThrusters()[0];
    }

    private void NormalizeThrusters()
    {
        foreach (ThrusterEngine thruster in thrusters)
        {
            if (thruster.direction != ThrusterEngine.Direction.South)
                thruster.speed = GetMainThruster().speed;
        }
    }

    public float GetCurrentSpeed()
    {
        return GetMainThruster().GetSpeed();
    }

    public float GetMaxSpeed()
    {
        return GetMainThruster().GetMaxSpeed();
    }
}
