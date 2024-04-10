using GPU_MESH_BUILDER;
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
    public float Height=.1f;
    [SerializeField]
    [Range(0,1)]
    public float scale = 1;
    [Range(0, 1)]
    public float min = 0;
    [SerializeField]
    MapGenerationSettings sets;


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
        float[,] map = generateMap(Vector2.zero);
        hmap.terrainData.SetHeights(0, 0, map);
    }

    private float[,] generateMap(Vector2 origin)
    {
        float[,] valuesMap = new float[lengthX, lengthY];

        //valuesMap = Noise.Calc3D(sizeX, sizeY, sizeZ, scale);
        for (int y = 0; y < lengthY; y++)
        {
            for (int x = 0; x < lengthX; x++)
            {
                valuesMap[x, y] = //((Noise.CalcPixel2D(x + (int)origin.x, y + (int)origin.y, scale) / 255) * Height);
                evaluatePoint(x, y, Height);
            }
        }
        return valuesMap;
    }

    public float evaluatePoint(int x, int y, float h)
    {
        float f = 0;
        float frequency = sets.baseRoughness;
        float amplitude = 1;
        float strength = 1;
        float valley = 0;
        float previousIncrease = .5f;

        for (int i = 0; i < sets.numLayers; i++)
        {
            x = (int)(((float)x) * frequency);
            y = (int)(((float)y) * frequency);
            float increase = Mathf.Max(Noise.CalcPixel2D(x, y, scale * frequency) / 255, valley);
            f += increase * h * amplitude * strength;
            strength /= sets.strength;
            frequency *= sets.roughness;
            amplitude *= sets.persistance;
            valley = sets.valleyFactor / (previousIncrease);//(1/(previousIncrease));
            previousIncrease = increase;
        }

        return Mathf.Max(f, min);
    }
}
