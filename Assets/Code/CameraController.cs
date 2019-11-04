using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] viewPoints;
    public float transformSpeed = 1.0f;
    private Transform currentView;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentView = viewPoints[currentIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(currentIndex == 0)
            {
                currentIndex = 1;
            }else if(currentIndex == 1)
            {
                currentIndex = 0;
            }
            currentView = viewPoints[currentIndex];
        }
        
    }

    void LateUpdate()
    {
        //Lerp position
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transformSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transformSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transformSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transformSpeed)
        );

        transform.eulerAngles = currentAngle;
        
    }
}
