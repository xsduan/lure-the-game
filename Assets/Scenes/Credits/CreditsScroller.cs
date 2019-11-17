using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{

    [SerializeField] float ScrollSpeed;

    void Update()
    {
        transform.Translate(0.0f, ScrollSpeed * Time.deltaTime * 100, 0.0f);
    }
}
