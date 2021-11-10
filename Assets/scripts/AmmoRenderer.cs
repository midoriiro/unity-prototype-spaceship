using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public abstract class AmmoRenderer : MonoBehaviour
{
    public float speed = 1f;
    public float life_time = 1f;
    public Transform weapon;
    protected Vector3 start;
    protected Rigidbody body_parent;
    protected Rigidbody body;
    protected ThrusterEngine thruster;

    protected void Start ()
    {
        start = transform.position;
        body_parent = transform.parent.gameObject.GetComponent<Rigidbody>();
        body = transform.GetComponent<Rigidbody>();
        thruster = body_parent.gameObject.GetComponentInChildren<ThrusterEngine>();
    }
}
