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

            #region Randomize Vector (commented out)
            //Calculate random position within specified range and depth around spawn point.
            //pos = Random.insideUnitCircle * range;
            //pos.y = -Random.Range(-depth, -1);
            //pos += transform.position;
            #endregion

            //spawn fish
            GameObject obj = Object.Instantiate(fish, transform.position, Quaternion.identity);
            //obj.transform.position = new Vector3(0,0,0);
            pop++;
        }
    }
}