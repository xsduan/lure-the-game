using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    #region Class Variables
    [SerializeField] GameObject fish;
    [SerializeField] int population = 5;
    [SerializeField] float range = 10f;
    [SerializeField] float depth = 5f;
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
        float randX = transform.position.x + Random.Range(-range, range);
        float randY = transform.position.y + Random.Range(1, depth);
        float randZ = transform.position.z + Random.Range(-range, range);

        Vector3 spawnPos = new Vector3(randX, randY, randZ);

        return Object.Instantiate(fish, spawnPos, Quaternion.identity);
    }
}
