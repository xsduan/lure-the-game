using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    #region Class Variables
    [SerializeField] GameObject fish;
    [SerializeField] int population = 5;
    [SerializeField] float despawnDistance = 10f;

    private GameObject[] fishArray;
    #endregion

    void Start () {
        fishArray = new GameObject[population];
        for (int i = 0; i < population; i++) {
            if (fishArray[i] == null) {
                fishArray[i] = Spawn();
            }
        }
    }

    void Update () {
        for (int i = 0; i < population; i++) {
            if (Vector3.Distance(transform.position, fishArray[i].transform.position) >= despawnDistance) {
                Object.Destroy(fishArray[i]);
                fishArray[i] = Spawn();
            }
        }
    }

    GameObject Spawn()
    {
        float randX = transform.position.x + Random.Range(1, 10);
        float randY = transform.position.y + Random.Range(1, 5);
        float randZ = transform.position.z + Random.Range(1, 10);

        Vector3 spawnPos = new Vector3(randX, randY, randZ);

        return Object.Instantiate(fish, spawnPos, Quaternion.identity);
    }
}
