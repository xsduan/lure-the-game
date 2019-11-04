using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Camera boatCam;
    public Camera personCam;
    public GameObject person;

    public float turnSpeed;
    public float accelerateSpeed;

    private Rigidbody rb;

    private bool isFishing = false;
    private float lastFPress = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        boatCam.enabled = true;
        personCam.enabled = false;
    } //Start()

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            Debug.Log("Pressed f at " + lastFPress + " Current game time is: " + Time.time);

            //Makes sure the user can't spam press the f key to change camera rapidly
            if (Time.time - lastFPress > 1)
            {
                //changes "game mode" and updates time
                isFishing = !isFishing;
                lastFPress = Time.time;

                //sets the cameras to the opposite of what they were set to
                boatCam.enabled = !boatCam.enabled;
                personCam.enabled = !personCam.enabled;

                //activates and deactivates cameras accordingly
                if (boatCam.enabled)
                {
                    boatCam.gameObject.SetActive(true);
                    personCam.gameObject.SetActive(false);
                }
                else
                {
                    boatCam.gameObject.SetActive(false);
                    personCam.gameObject.SetActive(true);
                }
            }
        }

        if (!isFishing)
        {
            //Movement mode

            //gets the horiztontal and vertical input from the keyboard (arrow keys)
            float moveLeftRight = Input.GetAxis("Horizontal");
            float moveFrontBack = Input.GetAxis("Vertical");

            float trueTurnSpeed = turnSpeed;
            float trueAccelerateSpeed = accelerateSpeed;

            //used to fine tune the turn speed while the boat is moving
            //there will be a larger turning radius while the boat is moving forward, accounted for by decreasing the turn speed
            if (moveFrontBack != 0)
            {
                trueTurnSpeed /= 1.25f;
            }

            //used to fine tune the movement speed
            //boats struggle to go in reverse, so to account for this we lower the true acceleration of the boat when the user tells the boat to go backwards
            if (moveFrontBack < 0)
            {
                trueAccelerateSpeed /= 1.5f;
                moveLeftRight *= -1;
            }

            //changes rotation by 0.0 in the x and z axes, and rotates by modified turn speed * horizontal input * Time.deltatime
            rb.AddTorque(0f, moveLeftRight * trueTurnSpeed * Time.deltaTime, 0f);

            //moves the rigidbody along the current view direction (transform.forward) by true acceleration * vertical input * Time.deltatime
            rb.AddForce(transform.forward * moveFrontBack * trueAccelerateSpeed * Time.deltaTime);
        }
        else
        {
            //Fishing mode
        }
    } //Update()
}
