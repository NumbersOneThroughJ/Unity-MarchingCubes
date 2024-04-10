using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MapGenerationSettings : MonoBehaviour
{
    [Range(0,5)]
    public float strength = 1;
    [Range(0, 5)]
    public float baseRoughness = 1;
    [Range(0, 5)]
    public float roughness = 2;
    [Range(0, 5)]
    public float persistance = .5f;
    [Range(0, 5)]
    public int numLayers = 3;
    [Range(0, 1)]
    public float valleyFactor = 0;
}
