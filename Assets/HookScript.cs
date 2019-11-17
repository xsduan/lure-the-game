using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{

    public bool isUnderwater = false;
    float timeCounter = 10.0f; //used to calculate dangling

    private Rigidbody rb;

    //Start is called before blah blah blah we fucking get it
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called every WE KNOW THIS ALREADY
    void LateUpdate()
    {
        if(rb.velocity.magnitude <= 0.1f && !isUnderwater && timeCounter <= 0.0f)
        {
            rb.AddForce(Vector3.forward * 0.1f);
        }
        timeCounter -= Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Water")
        {
            isUnderwater = !isUnderwater;
        }
    }
}
