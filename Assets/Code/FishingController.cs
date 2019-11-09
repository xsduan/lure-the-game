using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{

    public bool isFishing = false;

    GameObject hook;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform rodTip;

    [SerializeField] Rope rope;
    

    [SerializeField] Animator anim;
    bool throwingLine = false;


    public float maxHookDepth; //the theoretical maximum hook depth with the current equipment
    public float maxHookDistance; //the theoretical maximum hook depth with the current equipment

    float hookDepth; //how deep the hook can currently go, if it's reeled in
    float hookDistance; //how far the hook can currently go, if it's reeled in
    float reelLength = 1.0f; //


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(isFishing)
            {
                rope.TurnOff();
                rope.endObject = rodTip;
                Destroy(hook);
                reelLength = 1.0f;

            }
            isFishing = !isFishing; 
        }

        if (isFishing)
        {
            RotateBody();
            if (Input.GetMouseButtonDown(0) && !throwingLine && !hook)
            {
                ThrowLine();
            }
            
        }

        if (hook)
        {
            ConstrainHookPosition();
            Reel();
        }
    }

    private void RotateBody()
    {
        Vector3 rotDelta = new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f);
        transform.Rotate(rotDelta);
    }

    private void ThrowLine()
    {
        throwingLine = true;
        anim.SetTrigger("Throw");
    }

    private void ReleaseLine()
    {
        throwingLine = false;
        hook = Instantiate(hookPrefab);
        hook.transform.position = rodTip.transform.position;
        hook.GetComponent<Rigidbody>().AddForce(-transform.right * 50f);
        hookDistance = maxHookDistance;

        rope.startObject = rodTip.transform;
        rope.endObject = hook.transform;
        rope.TurnOn();
    }

    private void Reel()
    {
        if(Input.mouseScrollDelta.y < 0)
        {
            reelLength += Input.mouseScrollDelta.y * 0.01f;
            hookDepth = Mathf.Lerp(rodTip.transform.position.y, maxHookDepth, reelLength);
            hookDistance = Mathf.Lerp(0.0f, maxHookDistance, reelLength);
            Debug.Log(reelLength);
        }
    }


    //Makes it impossible for the hook to move beyond a specified depth and radius.
    private void ConstrainHookPosition()
    {
        Vector3 hookPos = hook.transform.position;
        Vector3 hookPosXZ = new Vector3(hook.transform.position.x, 0.0f, hook.transform.position.z); //The position of the hook along the X and Z axes
        Vector3 rodTipPosXZ = new Vector3(rodTip.transform.position.x, 0.0f, rodTip.transform.position.z);
        Vector3 hookOffset = hookPosXZ - rodTipPosXZ;

        hookOffset = Vector3.ClampMagnitude(hookOffset, hookDistance);
        Vector3 newHookPos = rodTip.transform.position + hookOffset;

        
        newHookPos.y = Mathf.Clamp(hookPos.y, hookDepth, Mathf.Infinity);

        hook.transform.position = newHookPos;
    }

}
