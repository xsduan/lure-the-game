using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantWater : MonoBehaviour
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
        for(int i = 0; i < tileCount; i++) {
            int xCoord = (int)Mathf.Floor(i/resolution);
            int yCoord = i%resolution;

            GameObject copyTile = Instantiate(gameObject);
            Debug.Log(tileRelativeScale);
            copyTile.transform.localScale.Set(tileRelativeScale, 1f, tileRelativeScale);
            Debug.Log(copyTile.transform.localScale);
            copyTile.transform.localPosition.Set(xCoord-tileSideCount+1, 0f, yCoord-tileSideCount+1);

            copyTile.transform.parent = gameObject.transform;
            copyTile.GetComponent<BuoyantWater>().enabled = true;
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        this.mesh = gameObject.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshCollider>().sharedMesh;

        this.initialHeights = new float[resolution*resolution];
        for(int i = 0; i < initialHeights.Length; i++) {
            initialHeights[i] = Random.Range(0f, magnitude);
        }
    }

    void FixedUpdate()
    {
        Vector3[] vertices = new Vector3[resolution*resolution];
        for(int i = 0; i < vertices.Length; i++) {
            //each point goes up and down between its random initial value and 0
    
        }

        for(int i = 0; i < tileCount; i++) {
            Debug.Log(gameObject.transform.GetChild(i).transform.localScale);
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
    public static float DistanceToWater(Vector3 position, float t) {
        return 0f;

    }
}

public class VertexData
{
    //The distance to water from this vertex
    public float distance;
    //An index so we can form clockwise triangles
    public int index;
    //The global Vector3 position of the vertex
    public Vector3 globalVertexPos;
}

//To save space so we don't have to send millions of parameters to each method
public struct TriangleData
{
    //The corners of this triangle in global coordinates
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    //The center of the triangle
    public Vector3 center;

    //The distance to the surface from the center of the triangle
    public float distanceToSurface;

    //The normal to the triangle
    public Vector3 normal;

    //The area of the triangle
    public float area;

    public TriangleData(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;

        //Center of the triangle
        this.center = (p1 + p2 + p3) / 3f;

        //Distance to the surface from the center of the triangle
        this.distanceToSurface = Mathf.Abs(WaterController.current.DistanceToWater(this.center, Time.time));

        //Normal to the triangle
        this.normal = Vector3.Cross(p2 - p1, p3 - p1).normalized;

        //Area of the triangle
        float a = Vector3.Distance(p1, p2);

        float c = Vector3.Distance(p3, p1);

        this.area = (a * c * Mathf.Sin(Vector3.Angle(p2 - p1, p3 - p1) * Mathf.Deg2Rad)) / 2f;
    }
}