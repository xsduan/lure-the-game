using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    #region Class Variables
    public GameObject person;
    public Transform firstPerson;
    public Transform thirdPerson;

    private float direchange;
    private float velo;
    private float dire;

    float maxSpeed = 1.6f;
    float maxTurn = 1.75f;
    float turnVelocityEffectThreshold = 1.5f;

    public float rotateCameraHorizontalSpeed;
    public float rotateCameraVerticalSpeed;
    public float speedThreshold;

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

    private void MoveBoat() {
        rb.velocity = transform.forward * velo;
        rb.rotation = Quaternion.Euler(transform.up * dire);
        dire += direchange*Mathf.Abs(direchange)/2;

        //moving forward & backward-- using GetAxis rather than GetKey for keybinding and cross-platform
        if(Input.GetAxis("Vertical") > 0) {
            //don't accelerate if we're turning too sharply or if we're too fast
            if(velo < speedThreshold && Mathf.Abs(direchange) < turnVelocityEffectThreshold) {
                velo += 0.05f;
            }
        } else if (Input.GetAxis("Vertical") < 0) {
            if(velo > -1*speedThreshold && Mathf.Abs(direchange) < turnVelocityEffectThreshold) {
                velo -= 0.05f/1.25f;
            }        
        } else {
            //decrease the magnitude of the velocity by 0.025
            if(velo > 0.025f) {
                velo -= 0.025f;
            } else if(velo < 0.025f) {
                velo += 0.025f;
            } else {
                //if the magnitude is less than 0.05, snap it to 0
                velo = 0f;
            }
        }
        
        //turning left
        if(Input.GetAxis("Horizontal") < 0) {
            
            if(Mathf.Abs(direchange) < maxTurn) {
                direchange -= 0.03f;
            }
        } else {
            if(direchange < 0) {
                direchange += 0.1f;
            }
        }

        //turning right
        if(Input.GetAxis("Horizontal") > 0) {
            if(Mathf.Abs(direchange) < maxTurn) {
                direchange += 0.03f;
            }
        } else {
            if(direchange > 0f) {
                direchange -= 0.1f;
            }
        }

        if(Mathf.Abs(direchange) > maxTurn && Mathf.Abs(velo) < maxSpeed) {
            if(direchange > 0f) {
                velo += -0.05f;
            } else if(direchange < 0f) {
                velo += -0.05f;
            } else {
                velo = 0f;
            }
        }
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
