using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Class Variables
    public Transform[] viewPoints;
    public float transformSpeed = 1.0f;
    private Transform currentView;
    private int currentIndex = 0;
    #endregion
    


    void Start()
    {
        currentView = viewPoints[currentIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //increases the index of where the current camera should be pointing to
            currentIndex += 1;

            //resets the camera to the first view if the end of the array is reached 
            if(currentIndex >= viewPoints.Length)
            {
                currentIndex = 0;
            }
            currentView = viewPoints[currentIndex];

        }
    }

    void LateUpdate()
    {
        //Linear interpolate position between the current position of the camera and the views current position
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transformSpeed);


        //Linear interpolation for rotating the camera between the camera's current angle and the angle of the current viewpoint
        Vector3 eulerAnglesOfCam = transform.rotation.eulerAngles;
        Vector3 eulerAnglesOfView = currentView.transform.rotation.eulerAngles;
        float speed = Time.deltaTime * transformSpeed;

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(eulerAnglesOfCam.x, eulerAnglesOfView.x, speed),
            Mathf.LerpAngle(eulerAnglesOfCam.y, eulerAnglesOfView.y, speed),
            Mathf.LerpAngle(eulerAnglesOfCam.z, eulerAnglesOfView.z, speed)
        );

        transform.eulerAngles = currentAngle;
    }
}
