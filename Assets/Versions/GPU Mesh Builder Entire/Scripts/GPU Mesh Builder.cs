using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using GPU_MESH_BUILDER;
public class GPUMeshBuilder : MonoBehaviour
{
    [SerializeField]
    bool outputLength = false;
    [SerializeField]
    MeshFilter meshFil;
    [SerializeField]
    ComputeShader GPU_Marcher;
    [SerializeField]
    GPU_MESH_BUILDER.Map map;

    //currentty in the file, sizes are set to 16 on all axises
    readonly int SIZE_X = 9, SIZE_Y = 9, SIZE_Z = 9;

    //Buffer Ids
    readonly int BUFFER_ID_POINT_VALUE = Shader.PropertyToID("point_Values");
    readonly int BUFFER_ID_VERTEX_RETURN = Shader.PropertyToID("VertexPoints");

    //Kernel Ids
    int KERNEL_ID_CALCVERTS;

    //Buffers
    ComputeBuffer Point_Value;
    ComputeBuffer Vertex_Return;

    void InitializeBuffers()
    {
        Point_Value = new ComputeBuffer(SIZE_X * SIZE_Y * SIZE_Z, sizeof(float));
        Vertex_Return = new ComputeBuffer((SIZE_X - 1) * (SIZE_Y - 1) * (SIZE_Z - 1) * 15, sizeof(float) * 3);

    }

    void ReleaseBuffers()
    {
        if (Point_Value != null)
        {
            Point_Value.Release();
            Point_Value = null;
        }
        if (Vertex_Return != null)
        {
            Vertex_Return.Release();
            Vertex_Return = null;
        }
    }

    void doShader()
    {
        GPU_Marcher.SetBuffer(KERNEL_ID_CALCVERTS, BUFFER_ID_POINT_VALUE, Point_Value);
        GPU_Marcher.SetBuffer(KERNEL_ID_CALCVERTS, BUFFER_ID_VERTEX_RETURN, Vertex_Return);

        clearVerts();

        GPU_Marcher.Dispatch(KERNEL_ID_CALCVERTS, 1, 1, 1);
    }

    Vector3[] retrieveVerteces()
    {
        Vector3[] rArr = new Vector3[Vertex_Return.count];
        Vertex_Return.GetData(rArr);
        return rArr;
    }

    int[] generateVertIndicies(Vector3[] arr)
    {
        List<int> indicies = new List<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            indicies.Add(i);
        }
        return indicies.ToArray();
    }

    void setMap(float[,,] map)
    {
        Point_Value.SetData(map);
    }
    void setMap()
    {
        Transform parentObject = transform.parent;
        Vector3 position = transform.localPosition;
        while(parentObject.parent != null)
        {
            position += parentObject.localPosition;
            parentObject = parentObject.parent;
        }

        setMap(map.regenerateMap(position));
    }

    private void setMesh(Vector3[] verts, int[] indicies)
    {
        meshFil.mesh.Clear();
        meshFil.mesh.vertices = verts;
        meshFil.mesh.triangles = indicies;
        meshFil.mesh.RecalculateNormals();
    }

    private void clearVerts()
    {
        Vertex_Return.SetData(new Vector3[Vertex_Return.count]);
    }

    public void Update()
    {
        setMap();
        doShader();
        //foreach (Vector3 v in retrieveVerteces()) print(v.ToString());
        Vector3[] verts = cleanverts(retrieveVerteces());
        int[] indicies = generateVertIndicies(verts);
        setMesh(verts, indicies);
        if (outputLength)
        {
            outputLength = false;
            print(map.getMap().Length);
        }
    }

    Vector3[] cleanverts(Vector3[] verts)
    {
        Vector3[] clean = verts.Where(v => v != Vector3.zero).ToArray();
        return clean.ToArray();
    }

        //Alocation and dealocation
    private void Awake()
    {
        KERNEL_ID_CALCVERTS = GPU_Marcher.FindKernel("CalculateVerts");
        InitializeBuffers();
    }
    private void OnEnable()
    {
        InitializeBuffers();
    }
    private void OnDisable()
    {
        ReleaseBuffers();
    }
    private void OnDestroy()
    {
        ReleaseBuffers();
    }
}