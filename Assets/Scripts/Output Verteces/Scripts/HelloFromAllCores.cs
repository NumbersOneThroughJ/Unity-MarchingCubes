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
        Hellos = new ComputeBuffer(3*3, 3*4);
    }

    void OnDisable()
    {
        Hellos.Release();
        Hellos = null;
    }

    private void OnEnable()
    {
        Hellos = new ComputeBuffer(3 * 3, 3 * 4);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        HelloShader.SetBuffer(0, helloBufferID, Hellos);
        HelloShader.Dispatch(0, 1, 1, 1);
        Vector3[] HelloVectors = new Vector3[9]; Hellos.GetData(HelloVectors);

        print("Hello");

        foreach(Vector3 v in HelloVectors)
        {
            print("Hello from " + v);
        }
    }
}
