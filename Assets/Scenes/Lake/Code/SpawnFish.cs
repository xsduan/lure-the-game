using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    #region Class Variables
    public GameObject fish;
    public float range = 2.0f;
    public float depth = 10f;
    public int popCap = 5;
    private int pop;
    #endregion

    void OnTriggerStay ()
    {
        Spawn();
    }

    // Update is called once per frame
    void Spawn()
    {
        Vector3 pos;
        //if spawn has not hit capacity
        if (pop < popCap){

            //Calculate random position within specified range and depth around spawn point.
            //pos = Random.insideUnitCircle * range;
            //pos.y = -Random.Range(-depth, -1);
            //pos += transform.position;

            //spawn fish
            Object.Instantiate(fish, transform.position, Quaternion.identity);
            pop++;
        }
    }
}
