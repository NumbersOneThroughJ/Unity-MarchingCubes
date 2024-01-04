using SimplexNoise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    int sizeX = 16;
    [SerializeField]
    int sizeY = 16;
    [SerializeField]
    int sizeZ = 16;
    [SerializeField]
    float maxValue = 10;

    [SerializeField]
    MapGenerationSettings sets = new MapGenerationSettings();

    private float[,,] MapVals;
    public float[,,] getMap() { return MapVals; }
    public float getVal(int x, int y, int z) { return MapVals[x, y, z]; }

    public void Start()
    {
        MapVals = generateMap();
    }

    public float[,,] generateMap()
    {
        float[,,] valuesMap = new float[sizeX, sizeY, sizeZ];

        for(int z = 0; z<sizeZ; z++)
        {
            for(int y = 0; y<sizeY; y++)
            {
                for(int x = 0; x<sizeX; x++)
                {
                    valuesMap[x, y, z] = evaluatePoint(x, y, z);
                }
            }
        }

        return valuesMap;
    }

    public float evaluatePoint(int x, int y, int z)
    {
        x = (int)(((float)x) * sets.roughness);
        y = (int)(((float)y) * sets.roughness);
        z = (int)(((float)z) * sets.roughness);
        float f = 0;
        float frequency = sets.baseRoughness;
        float amplitude = 1;

        for(int i = 0; i<sets.numLayers; i++)
        {
            f = Noise.CalcPixel3D(x, y, z, maxValue) * amplitude;
            frequency *= sets.roughness;
            amplitude *= sets.persistance;
        }

        return f;
    }
}
