using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFollowBoat : MonoBehaviour
{
    public Rigidbody boat;

    void Start()
    {
       //sets position of the Person object to 0.25 unity units above the center of the boat as default
       transform.position = new Vector3(boat.position.x, boat.position.y + 0.25f, boat.position.z);
    }

    void Update()
    {
        //updates the position every frame to make sure the person is always in the center of the boat
        transform.position = new Vector3(boat.position.x, boat.position.y + 0.25f, boat.position.z);
        transform.rotation = boat.rotation;

        //we will have to add more here if we want the person to move around on the boat,
        //but since that isn't a thing right now we don't need to worry.
    }
}
