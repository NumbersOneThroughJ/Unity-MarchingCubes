using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPU_MESH_BUILDER { 
    public class MapGenerationSettings : MonoBehaviour
    {
        public float strength = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistance = .5f;
        public int numLayers = 3;
    }
}