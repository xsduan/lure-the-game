using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{

    public bool isFishing = false;


    public Transform view;
    GameObject hook;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform rodTip;
    

    [SerializeField] Animator anim;
    bool throwingLine = false;

    public float maxHookDepth; //the theoretical maximum hook depth with the current equipment
    public float maxHookDistance; //the theoretical maximum hook depth with the current equipment

    float hookDepth; //how deep the hook can currently go, if it's reeled in
    float hookDistance; //how far the hook can currently go, if it's reeled in


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !throwingLine && !hook)
        {
            ThrowLine();
        }
        if(hook)
        {
            ConstrainHookPosition();
        }
    }

    private void RotateBody()
    {

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

        hook.GetComponent<Rigidbody>().AddForce(transform.parent.transform.forward * 50f);
        hookDepth = maxHookDepth;
        hookDistance = maxHookDistance;
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
