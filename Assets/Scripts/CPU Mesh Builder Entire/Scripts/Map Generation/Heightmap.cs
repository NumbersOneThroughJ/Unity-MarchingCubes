using SimplexNoise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heightmap : MonoBehaviour
{
    [SerializeField]
    public int lengthX=100;
    [SerializeField]
    public int lengthY=100;
    [SerializeField]
    [Range(0,1)]
    public float scale = 1;
    [SerializeField]


    private Terrain hmap;
    // Start is called before the first frame update
    void Start()
    {
        hmap = GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        generateMap();
    }

    void generateMap()
    {
        float[,] map = Noise.Calc2D(lengthX,lengthY, scale);
        hmap.terrainData.SetHeights(0, 0, map);
    }
}
