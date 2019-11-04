using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFollowBoat : MonoBehaviour
{
    public Rigidbody boat;

    // Start is called before the first frame update
    void Start()
    {
       transform.position = new Vector3(boat.position.x, boat.position.y + 0.25f, boat.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(boat.position.x, boat.position.y + 0.25f, boat.position.z);
        transform.rotation = boat.rotation;
    }
}
