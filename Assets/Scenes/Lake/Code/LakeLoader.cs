using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeLoader : MonoBehaviour
{
    public Transform player;
    public Terrain tileTemplate;
    public int renderDistance = 1;

    private Vector3 lastPlayerGridPos;
    private GameObject[] terrains;

    [SerializeField]
    private int symmetryOffset;
    [SerializeField]
    private float islandChance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null) player = GameObject.FindWithTag("Player").transform;
        int terrainRes = renderDistance * 2 + 1;
        int terrainCount = terrainRes*terrainRes;

        terrains = new GameObject[terrainCount];

        for(int x = 0; x < terrainRes; x++) {
            for(int y = 0; y < terrainRes; y++) {
                terrains[x*terrainRes + y] = Terrain.CreateTerrainGameObject(Instantiate(tileTemplate.terrainData));
                float terrainSize = tileTemplate.terrainData.size.x;

                terrains[x*terrainRes + y].transform.parent = gameObject.transform;

                terrains[x*terrainRes + y].transform.localPosition = new Vector3(
                    (x-renderDistance)*terrainSize,
                    0,
                    (y-renderDistance)*terrainSize
                );
            }
        }
        tileTemplate.enabled = false;
        UpdateLakefloor(Vector3.zero, Vector3.zero);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float terrainSize = tileTemplate.terrainData.size.x;

        Vector3 currentPlayerPos = player.transform.position;
        Vector3 currentPlayerGridPos = new Vector3(
            Mathf.Floor(currentPlayerPos.x/terrainSize),
            Mathf.Floor(currentPlayerPos.y/terrainSize),
            Mathf.Floor(currentPlayerPos.z/terrainSize)
        );

        if(currentPlayerGridPos != lastPlayerGridPos) {
            gameObject.transform.localPosition = currentPlayerGridPos * terrainSize;
            UpdateLakefloor(currentPlayerGridPos, lastPlayerGridPos);
        }

        lastPlayerGridPos = currentPlayerGridPos;
    }

    void changeResolution(int newRes) {

    }

    void UpdateLakefloor(Vector3 currentPlayerGridPos, Vector3 lastPlayerGridPos) {
        int terrainRes = renderDistance * 2 + 1;

        for(int x = 0; x < terrainRes; x++) {
            for(int y = 0; y < terrainRes; y++) {
                
                Vector3 offset = currentPlayerGridPos+(new Vector3(x,0f,y));

                float[,] terrainMap = GenerateNoiseTerrainMap(offset*33, 33, islandChance, terrainRes);

                terrains[x*3+y].GetComponent<Terrain>().terrainData.SetHeightsDelayLOD(0,0,terrainMap);
            }
        }
    }

    float[,] GenerateNoiseTerrainMap(Vector3 offset, int resolution, float islandChance, int terrainRes) {
        float[,] terrainMap = new float[resolution,resolution];

        for(int x = 0; x < resolution; x++) {
            for(int y = 0; y < resolution; y++) {
                float xco = (offset.z + y + (y==0?-1:0) + 1 + symmetryOffset)/resolution*terrainRes;
                float yco = (offset.x + x + (x==0?-1:0) + 1 + symmetryOffset)/resolution*terrainRes;
                float num = Mathf.PerlinNoise(xco, yco);

                terrainMap[y,x] = Mathf.Pow(num,islandChance);
            }
        }

        return terrainMap;
    }
}
