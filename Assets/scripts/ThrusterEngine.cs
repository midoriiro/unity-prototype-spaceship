using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterEngine : MonoBehaviour
{
    public float speed = 25f;
    private float speed_max = 50f;
    private float torque;
    private float torque_max;
    private float boost_factor = 2f;
    private float boost_factor_max = 1.5f;
    private float boost_max;
    private float super_boost_factor = 20f;
    private float super_boost_factor_max = 5f;
    private float super_boost_max;
    private float super_boost_capacitor_factor = 0.1f;
    private float super_boost_capacitor = 0f;
    private float light_speed = 1000000;
    private float decelerate_factor = 0.9f;
    private float drag_min = 3.75f;
    private float drag_normal = 7.5f;
    private float drag_max = 15f;
    private float drag_factor = 2f;
    private float drag_factor_max = 1.5f;
    public Direction direction;
    public enum Direction { North, South, East, West, Center};
    private bool boost;
    private bool super_boost;
    private bool shift;
    private Vector3 previous_velocity = Vector3.zero;
    private Vector3 current_torque = Vector3.zero;
    public bool gyroscope = false;   
    private Rigidbody body;

    void Start()
    {
        speed_max = speed * 2f;
        torque = speed / 64;
        torque_max = torque * 2;
        drag_max = (int)speed_max / 2;
        drag_normal = (int)speed_max / 4;
        drag_min = (int)speed_max / 16;
        boost_max = speed_max * boost_factor_max;
        super_boost_max = speed_max * super_boost_factor_max;
        super_boost_capacitor += super_boost_capacitor_factor;
        body = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && !super_boost)
        {
            shift = true;
            body.angularDrag = Mathf.Lerp(body.angularDrag, drag_min, Time.smoothDeltaTime);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            shift = false;
            body.angularDrag = Mathf.Lerp(body.angularDrag, drag_normal, Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !super_boost)
        {
            boost = true;
            body.angularDrag = Mathf.Lerp(body.angularDrag, drag_max, Time.smoothDeltaTime);
        }
        else if(!Input.GetKeyUp(KeyCode.LeftShift))
        {
            boost = false;
            body.angularDrag = Mathf.Lerp(body.angularDrag, drag_normal, Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl) && !shift)
        {
            if (super_boost_capacitor < super_boost_max)
                super_boost_capacitor += super_boost_capacitor_factor;

            super_boost = true;
            //body.angularDrag = Mathf.Lerp(body.angularDrag, light_speed, Time.smoothDeltaTime);
            body.angularDrag = light_speed;
        }
        else if (!Input.GetKeyUp(KeyCode.LeftControl) && !shift)
        {
            super_boost_capacitor /= 2f;

            if (super_boost_capacitor < 0f)
                super_boost_capacitor = super_boost_capacitor_factor;

            super_boost = false;
            body.angularDrag = Mathf.Lerp(body.angularDrag, drag_normal, Time.smoothDeltaTime);
        }


        if (Input.GetKey(KeyCode.Z) && direction == Direction.South)
        {
            FireSouth();
        }
        else if(Input.GetKey(KeyCode.S) && direction == Direction.North)
        {
            FireNorth();
        }
        else
        {
            DecelerateForce();
        }

        if(Input.GetKey(KeyCode.Q) && direction == Direction.Center && gyroscope && !super_boost)
        {
            TorqueEast();
        }
        else if(Input.GetKey(KeyCode.Q) && direction == Direction.East && !gyroscope && !super_boost)
        {
            FireEast();
        }
        else if(Input.GetKey(KeyCode.D) && direction == Direction.Center && gyroscope && !super_boost)
        {
            TorqueWest();
        }
        else if(Input.GetKey(KeyCode.D) && direction == Direction.West && !gyroscope && !super_boost)
        {
            FireWest();
        }
        else if(gyroscope)
        {
            body.angularDrag = 0.05f;
            DecelerateTorque();
        }

        Normalize();

        previous_velocity = body.velocity;
    }

    public bool OnBoost()
    {
        return boost;
    }

    public bool OnSuperBoost()
    {
        return super_boost;
    }

    public bool OnShift()
    {
        return shift;
    }    

    bool IsMoving()
    {
         return previous_velocity.magnitude < body.velocity.magnitude;
    }

    public Vector3 GetVelocity()
    {
        return body.velocity;
    }

    public Vector3 GetPreviousVelocity()
    {
        return previous_velocity;
    }

    public float GetMagnitudeThreshold()
    {
        if(!boost && !super_boost)
            return speed / 2;
        else if(boost)
            return speed * boost_factor_max + speed;
        else
            return speed * super_boost_factor_max + speed;
    }

    public float GetSpeed()
    {
        if (!boost && !super_boost)
            return speed;
        else if (boost && !super_boost)
            return speed * boost_factor;
        else
            return super_boost_capacitor * super_boost_factor;         
    }

    public float GetMaxSpeed()
    {
        if (!boost && !super_boost)
            return speed_max;
        else if (boost && !super_boost)
            return boost_max;
        else
            return GetSpeed() < super_boost_max ? super_boost_max : super_boost_max * light_speed;
    }

    void FireNorth()
    {
        if(!boost)
        {
            body.AddForceAtPosition(Vector3.Lerp(body.velocity, transform.up * -( speed / 2 ), Time.smoothDeltaTime), transform.position);
        }            
        else
            body.AddForceAtPosition(transform.up * -(speed * boost_factor), transform.position);
    }

    void FireSouth()
    {
        if (body.velocity.magnitude < GetMagnitudeThreshold())
            body.AddForceAtPosition(Vector3.Lerp(body.velocity, transform.up * GetSpeed(), Time.smoothDeltaTime), transform.position);
        else
            body.AddForceAtPosition(transform.up * GetSpeed(), transform.position);
    }

    void FireEast()
    {
        body.AddForceAtPosition(Vector3.left * speed * Time.deltaTime, transform.position);
    }

    void FireWest()
    {
        body.AddForceAtPosition(Vector3.right * speed * Time.deltaTime, transform.position);
    }

    void TorqueEast()
    {
        current_torque = Vector3.forward;

        if(!shift)
            body.AddRelativeTorque(current_torque * speed);
        else
            body.AddRelativeTorque(current_torque * (speed * drag_factor * drag_factor_max), ForceMode.Acceleration);
    }

    void TorqueWest()
    {
        current_torque = -Vector3.forward;

        if(!shift)
            body.AddRelativeTorque(-Vector3.forward * speed);
        else
            body.AddRelativeTorque(-Vector3.forward * (speed * drag_factor * drag_factor_max), ForceMode.Acceleration);        
    }

    void DecelerateForce()
    {
        if(body.angularVelocity == Vector3.zero)
            body.velocity = Vector3.Lerp(body.velocity, body.velocity * decelerate_factor, Time.smoothDeltaTime);
    }

    void DecelerateTorque()
    {
        body.angularVelocity = Vector3.Lerp(body.angularVelocity, -body.angularVelocity * decelerate_factor, Time.smoothDeltaTime);

        if (body.angularVelocity.magnitude < 0.1f)
            body.angularVelocity = Vector3.zero;
    }

    void Normalize()
    {
        if(body.velocity.magnitude > GetMaxSpeed())
        {
            body.velocity = Vector3.Lerp(body.velocity, transform.up * GetMaxSpeed(), Time.smoothDeltaTime);
        }

        if (body.angularVelocity.magnitude > torque_max)
        {
            body.angularVelocity = Vector3.ClampMagnitude(body.angularVelocity, torque_max);
        }
    }
}
