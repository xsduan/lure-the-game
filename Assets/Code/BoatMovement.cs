using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    #region Class Variables
    public GameObject person;
    public Transform firstPerson;
    public Transform thirdPerson;

    public float turnSpeed;
    public float accelerateSpeed;
    public float rotateCameraHorizontalSpeed;
    public float rotateCameraVerticalSpeed;

    private Rigidbody rb;

    private bool isFishing = false;
    private float yaw = -90.0f;
    private float pitch = 0.0f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isFishing = !isFishing;
            pitch = 0.0f;
        }

        if (!isFishing)
        {
            //Movement mode
            MoveBoat();
        }
        else
        {
            //Fishing mode
            RotateHeadInFishMode();
        }
    }

    private void MoveBoat()
    {
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

    private void RotateHeadInFishMode()
    {
        //change rotation about the y axis in unity
        yaw += rotateCameraHorizontalSpeed * Input.GetAxis("Horizontal");

        //change pitch of camera to look up or down
        float pitchDelta = rotateCameraVerticalSpeed * Input.GetAxis("Vertical");
        if (Mathf.Abs(pitch - pitchDelta) > 20) //limits the camera to only look 20 degrees up or 20 degrees down from the horizon
        {
            pitchDelta = 0.0f;
        }
        pitch -= pitchDelta;

        firstPerson.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
