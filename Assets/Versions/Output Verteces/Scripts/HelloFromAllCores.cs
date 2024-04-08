using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelloFromAllCores : MonoBehaviour
{
    [SerializeField]
    ComputeShader HelloShader;

    ComputeBuffer Hellos;

    readonly int helloBufferID = Shader.PropertyToID("Result");

    void Awake()
    {
        Hellos = new ComputeBuffer(2*2*2, sizeof(float)*3);
    }

    void OnDisable()
    {
        Hellos.Release();
        Hellos = null;
    }

    private void OnEnable()
    {
        Hellos = new ComputeBuffer(2*2*2, 3 * 4);
    }

    void OnMouseDown()
    {
        int HelloID = HelloShader.FindKernel("Hello");
        HelloShader.SetBuffer(HelloID, helloBufferID, Hellos);
        HelloShader.Dispatch(HelloID, 1, 1, 1);
        Vector3[] HelloVectors = new Vector3[Hellos.count]; Hellos.GetData(HelloVectors);
        
        print("Hello");

        foreach(Vector3 v in HelloVectors)
        {
            print("Hello from " + v);
        }
    }
}
