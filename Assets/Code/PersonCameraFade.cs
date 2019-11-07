//Person Camera Fade
//Prevents camera from clipping into person

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCameraFade : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] MeshRenderer headRenderer;
    [SerializeField] MeshRenderer bodyRenderer;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, cameraTransform.position); //The distance from the camera to the player

        if(dist <= 1.2f)
        {
            //Get the colors of the head and body
            Color headColor = headRenderer.material.color;
            Color bodyColor = bodyRenderer.material.color;

            float transparency = Mathf.Lerp(1.0f, 0.0f, 1.0f - dist);

            headColor.a = transparency;
            bodyColor.a = transparency;

            headRenderer.material.color = headColor;
            bodyRenderer.material.color = bodyColor;
        }
    }
}
