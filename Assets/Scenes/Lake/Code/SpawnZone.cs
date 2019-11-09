using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject fish;
    int pop;
    public int popCap;

    void Update() {
        Spawn();
    }

    // Update is called once per frame
    void Spawn() {
        Vector3 pos;
        //if spawn has not hit capacity
        if (pop < popCap) {

            //Calculate random position within specified range and depth around spawn point.
            //pos = Random.insideUnitCircle * range;
            //pos.y = -Random.Range(-depth, -1);
            //pos += transform.position;

            //spawn fish
            GameObject obj = Object.Instantiate(fish, transform.position, Quaternion.identity);
            Debug.Log(transform.position);
            Debug.Log(obj.transform.position);
            obj.transform.position = new Vector3(0,0,0);
            pop++;
        }
    }
}