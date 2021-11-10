using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class WeaponController : MonoBehaviour
{
    private int current_weapon = 0;
    private Rigidbody body;
    private GameObject[] list_weapon;
    private string[] list_available_weapon;

    // Use this for initialization
    void Start ()
    {
        body = transform.GetComponent<Rigidbody>();
        list_available_weapon = new string[]
        {
            "Laser Beam",
            "Laser Long Pulse",
            "Laser Short Pulse"
        };

        SetWeapon();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            for(int i = 0 ; i < list_weapon.Length ; ++i)
            {
                AmmoController controller = list_weapon[i].GetComponent<AmmoController>();
                controller.Fire();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            for(int i = 0 ; i < list_weapon.Length ; ++i)
            {
                AmmoController controller = list_weapon[i].GetComponent<AmmoController>();
                controller.UnFire();
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            current_weapon += 1;

            if(current_weapon == list_available_weapon.Length)
                current_weapon = 0;

            SetWeapon();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            current_weapon -= 1;

            if(current_weapon < 0)
                current_weapon = list_available_weapon.Length - 1;

            SetWeapon();
        }
    }

    void SetWeapon()
    {
        list_weapon = GameObjectHelper.GetChildsByTag(transform.gameObject, list_available_weapon[current_weapon]);
    }
}
