using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaves : MonoBehaviour
{
    [SerializeField]
    public float magnitude = 1;
    [SerializeField]
    public float speed = 1;
    [SerializeField]
    public int resolution = 512;
    public int tileSize = 16;
    private float size = 11;

    private Mesh mesh;
    private float[] initialHeights;
    private int tileCount;
   
    void Start()
    {
        size = gameObject.transform.localScale.x;

        //create tiles
        float tileRelativeScale = tileSize/gameObject.transform.localScale.x;
        
        int tileSideCount = (int)gameObject.transform.localScale.x/tileSize;

        tileCount = tileSideCount*tileSideCount;
        Debug.Log(tileCount);
        for(int i = 0; i < tileCount; i++) {
            int xCoord = (int)Mathf.Floor(i/resolution);
            int yCoord = i%resolution;

            GameObject copyTile = Instantiate(gameObject);
            Debug.Log(tileRelativeScale);
            copyTile.transform.localScale.Set(tileRelativeScale, 1f, tileRelativeScale);
            Debug.Log(copyTile.transform.localScale);
            copyTile.transform.localPosition.Set(xCoord-tileSideCount+1f, 0f, yCoord-tileSideCount+1f);

            copyTile.transform.parent = gameObject.transform;
            copyTile.GetComponent<WaterWaves>().enabled = false;
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        this.mesh = gameObject.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshCollider>().sharedMesh;

        this.initialHeights = new float[resolution*resolution];
        for(int i = 0; i < initialHeights.Length; i++) {
            initialHeights[i] = Random.Range(0f, magnitude);
        }
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[resolution*resolution];
        for(int i = 0; i < vertices.Length; i++) {
            //each point goes up and down between its random initial value and 0
            vertices[i].y = Mathf.PingPong(Time.time*speed, initialHeights[i]);
        }

        for(int i = 0; i < tileCount; i++) {
            gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material.SetTexture("_BumpMap", BuildWaterBumpMap(vertices, resolution, magnitude));
        }
        gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BumpMap", BuildWaterBumpMap(vertices, resolution, magnitude));
        
        Resources.UnloadUnusedAssets();
    }

    private Texture2D BuildWaterBumpMap(Vector3[] vertices, int resolution, float magnitude) {
        Color[] colorMap = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            //calculate differences from one pixel to another
            float xDown = 0;
            float xUp = 0;
            if(i%resolution > 0) xDown = vertices[i-1].y;
            else if(i%resolution < resolution - 1) xUp = vertices[i+1].y;

            float yDown = 0;
            float yUp = 0;
            if(i-resolution > 0) yDown = vertices[i-resolution].y;
            else if(i+resolution < resolution*resolution) yUp = vertices[i+resolution].y;

            float xDelta = ((xUp-xDown)+1)*0.5f;
            float yDelta = ((yUp-yDown)+1)*0.5f;

            colorMap[i] = new Color(xDelta, yDelta, 1f, yDelta);
        }

        // create a new texture and set its pixel colors to the calculated deltas
        Texture2D tileTexture = new Texture2D (resolution, vertices.Length / resolution);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        colorMap = null;

        return tileTexture;
    }
}
