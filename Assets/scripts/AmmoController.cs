using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class AmmoController : MonoBehaviour
{
    public enum Type
    {
        LaserBeam,
        LaserLongPulse,
        LaserShortPulse
    }

    private float cooldown = 0f;
    public float fire_rate = 0.25f;
    public Type type;
    private bool is_fire = false;
    private AssetBundle bundle;
    private Rigidbody body;
    private GameObject ammo;
    private GameObject clone;

    void Start ()
    {
        body = transform.parent.gameObject.GetComponent<Rigidbody>();
        bundle = BundleHelper.GetBundle("spaceship.ammo");

        switch(type)
        {
            case Type.LaserBeam:
            ammo = BundleHelper.GetPrefab(bundle, "ammo_laser_beam");
            break;

            case Type.LaserLongPulse:
            ammo = BundleHelper.GetPrefab(bundle, "ammo_laser_long_pulse");
            break;

            case Type.LaserShortPulse:
            ammo = BundleHelper.GetPrefab(bundle, "ammo_laser_short_pulse");
            break;
        }
    }

    public void Fire()
    {
        if(Time.time >= cooldown)
        {
            if(!is_fire && type == Type.LaserBeam)
            {
                clone = GetClone();
                is_fire = true;
            }
            else if(type == Type.LaserLongPulse || type == Type.LaserShortPulse)
            {
                clone = GetClone();
            }            

            cooldown = Time.time + fire_rate;
        }        
    }

    public void UnFire()
    {
        is_fire = false;

        if(!is_fire && type == Type.LaserBeam)
        {
            Destroy(clone);
        }
    }

    private GameObject GetClone()
    {
        RectTransform rt = (RectTransform)ammo.transform;
        Vector3 position = transform.position + transform.up * rt.rect.height * 2f;

        GameObject clone = Instantiate<GameObject>(ammo, position, transform.rotation);
        clone.transform.SetParent(body.gameObject.transform);

        AmmoRenderer laser = clone.GetComponent<AmmoRenderer>();
        laser.weapon = transform;

        return clone;        
    }
}
