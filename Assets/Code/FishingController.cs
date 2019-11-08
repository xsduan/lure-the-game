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

    public float maxHookDepth;
    public float maxHookDistance;


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
    }

    private void ConstrainHookPosition()
    {
        if(hook.transform.position.y < maxHookDepth)
        {
            Vector3 newHookPos = hook.transform.position;
            newHookPos.y = Mathf.Clamp(newHookPos.y, maxHookDepth, Mathf.Infinity);
            hook.transform.position = newHookPos;
        }
    }

}
