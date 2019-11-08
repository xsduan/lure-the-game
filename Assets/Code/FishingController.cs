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
        if(Input.GetMouseButtonDown(0) && !throwingLine)
        {
            ThrowLine();
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
    }

}
