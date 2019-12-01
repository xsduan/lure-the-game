using UnityEngine;

public class SpawnFish : MonoBehaviour {
    //TODO: Select fish to be spawned using probabilities instead of serialized
    //      GameObject field.

    private void Start() {
        //Populate area with fish at start
        fishArray = new GameObject[population];
        for (int i = 0; i < population; i++) {
            if (fishArray[i] == null) {
                fishArray[i] = Spawn();
            }
        }
    }

    private void Update() {
        //Each fish that is too far away from the player gets depawned and a new fish spawns within range
        for (int i = 0; i < population; i++) {
            GameObject currentFish = fishArray[i];

            if (Vector3.Distance(transform.position, currentFish.transform.position) >= despawnDistance) {
                Destroy(currentFish);

                fishArray[i] = Spawn();
            }
        }
    }

    private GameObject Spawn() {
        //Determine random spawn position based on player's position, then spawn new fish
        float randX = transform.position.x + Random.Range(-range, range);
        float randY = transform.position.y - Random.Range(1, depth);
        float randZ = transform.position.z + Random.Range(-range, range);

        Vector3 spawnPos = new Vector3(randX, randY, randZ);

        return Instantiate(fish, spawnPos, Quaternion.identity);
    }

    #region Class Variables

    [SerializeField] private readonly GameObject fish;
    [SerializeField] private readonly int population = 5;
    [SerializeField] private readonly float range = 10f;
    [SerializeField] private readonly float depth = 5f;
    [SerializeField] private readonly float despawnDistance = 10f;

    private GameObject[] fishArray;

    #endregion
}