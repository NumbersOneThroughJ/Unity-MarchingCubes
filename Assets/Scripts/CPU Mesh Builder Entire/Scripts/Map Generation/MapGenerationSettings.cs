using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MapGenerationSettings : MonoBehaviour
{
    public float strength = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistance = .5f;
    public int numLayers = 3;
}
