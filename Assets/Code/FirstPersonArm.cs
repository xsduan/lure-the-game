using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonArm : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        anim.SetTrigger("Open");
    }
    public void Close()
    {
        anim.SetTrigger("Close");
    }
}
