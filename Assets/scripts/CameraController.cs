using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Rigidbody player;
    private ShipController ship;
    private float speed = 2.0f; 

	void Start () 
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();
    }

	void LateUpdate ()
	{
        speed = Mathf.Lerp(speed, ship.GetCurrentSpeed() / 8, Time.smoothDeltaTime);

        float interpolation = speed * Time.smoothDeltaTime;

        Vector3 position = transform.position;

		position.x = Mathf.Lerp (transform.position.x, player.transform.position.x, interpolation);
        position.y = Mathf.Lerp(transform.position.y, player.transform.position.y, interpolation);
 
		transform.position = position;
    }
}


