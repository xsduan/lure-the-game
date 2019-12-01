using UnityEngine;

public class CameraController : MonoBehaviour {
    private void Start() {
        //currentView = viewPoints[currentIndex];
    }

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            //increases the index of where the current camera should be pointing to
            currentIndex += 1;

            //resets the camera to the first view if the end of the array is reached 
            if(currentIndex >= viewPoints.Length)
            {
                currentIndex = 0;
            }
            currentView = viewPoints[currentIndex];
            //ChangeView();
        }*/
    }

    private void LateUpdate() {
        transform.position = player.TransformPoint(new Vector3(0, 2f, -4.0f));

        Vector3 eulerAnglesOfCam = transform.rotation.eulerAngles;
        Vector3 eulerAnglesOfView = player.transform.rotation.eulerAngles;
        float speed = Time.deltaTime * transformSpeed;

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(eulerAnglesOfCam.x, eulerAnglesOfView.x, speed),
            Mathf.LerpAngle(eulerAnglesOfCam.y, eulerAnglesOfView.y, speed),
            Mathf.LerpAngle(eulerAnglesOfCam.z, eulerAnglesOfView.z, speed)
        );

        transform.eulerAngles = currentAngle;
        transform.LookAt(player);
    }

    private void ChangeView() {
        while (transform.position != currentView.position) {
            //Linear interpolate position between the current position of the camera and the views current position
            transform.position =
                Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transformSpeed);

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

    #region Class Variables

    //public Transform[] viewPoints;
    public float transformSpeed = 1.0f;

    public Transform player;
    //public int lookOffsetCount = 10;

    private readonly Transform currentView;
    private readonly int currentIndex = 0;

    #endregion
}