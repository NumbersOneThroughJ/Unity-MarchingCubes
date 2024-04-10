using SimplexNoise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPU_MESH_BUILDER
{
    public class Map : MonoBehaviour
    {
        int sizeX = 9;
        int sizeY = 9;
        int sizeZ = 9;
        [SerializeField]
        [Range(0f, 1f)]
        float scale = .00043f;
        [SerializeField]
        [Range(0.1f, 100f)]
        float max = 10;

        [SerializeField]
        private float[,,] MapVals;
        public float[,,] getMap() { return MapVals; }
        public float getVal(int x, int y, int z) { return MapVals[x, y, z]; }

        public float getVal(Vector3 loc)
        {
            return MapVals[(int)loc.x, (int)loc.y, (int)loc.z];
            /*
            print(tempMap.GetLength(0)+" "+tempMap.GetLength(1)+" "+tempMap.GetLength(2));
            return tempMap[(int)loc.y, (int)loc.z, (int)loc.x]; 
            */
        }

        public void Start()
        {
            MapVals = generateMap();
            //logMap(MapVals);
        }

        public float[,,] regenerateMap()
        {
            MapVals = generateMap();
            return MapVals;
        }
        public float[,,] regenerateMap(Vector3 point)
        {
            MapVals = generateMap(point);
            return MapVals;
        }
        public float[,,] regenerateMap(int x, int y, int z)
        {
            MapVals = generateMap(new Vector3(x,y,z));
            return MapVals;
        }

        private float[,,] generateMap()
        {
            return generateMap(Vector3.zero);
        }

        private float[,,] generateMap(Vector3 origin)
        {
            float[,,] valuesMap = new float[sizeX, sizeY, sizeZ];

            //valuesMap = Noise.Calc3D(sizeX, sizeY, sizeZ, scale);
            for (int z = 0; z < sizeZ; z++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        valuesMap[x, y, z] = (Noise.CalcPixel3D(x + (int)origin.x, y + (int)origin.y, z + (int)origin.z, scale) / 255 * max);
                    }
                }
            }
            return valuesMap;
        }

    }
}