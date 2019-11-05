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
    public int resolution = 11;

    public Color darkWater;
    public Color lightWater;

    private Mesh mesh;
    private float[] initialHeights;
   
    void Start()
    {
        this.mesh = gameObject.GetComponent<MeshFilter>().mesh;
        this.initialHeights = new float[mesh.vertices.Length];
        for(int i = 0; i < initialHeights.Length; i++) {
            initialHeights[i] = Random.Range(0f, magnitude);
        }
    }

    void Update()
    {
        Vector3[] vertices = mesh.vertices;
        Color[] vertexColors = new Color[vertices.Length];
        for(int i = 0; i < vertices.Length; i++) {
            //each vertex goes up and down between its random initial value and 0
            vertices[i].y = Mathf.PingPong(Time.time*speed, initialHeights[i]);
        }

        //set texture for lake
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = BuildWaterTexture(vertices, resolution, magnitude);
    }

    private Texture2D BuildWaterTexture(Vector3[] vertices, int resolution, float magnitude) {

        Color[] colorMap = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            // assign as color a shade of proportional to the height value
            colorMap [i] = Color.Lerp(darkWater, lightWater, vertices[i].y/magnitude);
        }

        // create a new texture and set its pixel colors
        Texture2D tileTexture = new Texture2D (resolution, vertices.Length / resolution);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply ();

        return tileTexture;
    }
}
