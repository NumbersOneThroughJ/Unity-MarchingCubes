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
    float scale = 10;
    [SerializeField]
    MapGenerationSettings sets = new MapGenerationSettings();

    [SerializeField]
    private float[,,] tempMap = {
        {
            { 7f, 8f, 0f, 0f },
            { 0f,0f,0f,0f }
        },
        {
            { 0f, 0f, 0f, 0f },
            { 0f,0f,0f,0f }
        }
    };
    private float[,,] MapVals;
    public float[,,] getMap() { return MapVals; }
    public float getVal(int x, int y, int z) { return MapVals[x, y, z]; }

    public float getVal(Vector3 loc) { return MapVals[(int)loc.x, (int)loc.y, (int)loc.z]; }

    public void Start()
    {
        MapVals = generateMap();
        //logMap(MapVals);
    }

    private void logMap(float[,,] map)
    {
        for(int y = 0; y < sizeY; y++)
        {
            print("{");
            for (int z = 0; z < sizeZ; z++)
            {
                print("[");
                for(int x = 0; x < sizeX; x++)
                {
                    print(map[x, y, z]+", ");
                }
                print("]");
            }
            print("}");
        }
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
                    valuesMap[x, y, z] = (Noise.CalcPixel3D(x, y, z, scale) / 255) * maxValue;
                        //evaluatePoint(x, y, z);
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
            f = Noise.CalcPixel3D(x, y, z, scale) * amplitude;
            frequency *= sets.roughness;
            amplitude *= sets.persistance;
        }

        return f;
    }
}
