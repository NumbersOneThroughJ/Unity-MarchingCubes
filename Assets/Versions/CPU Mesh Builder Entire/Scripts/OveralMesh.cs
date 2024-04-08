using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OveralMesh : MonoBehaviour
{
    private readonly int MAPRANGE = 9;

    [SerializeField]
    private CubeControls cube;
    [SerializeField]
    MeshFilter world;

    [SerializeField]
    public bool step = false;
    [SerializeField]
    public bool bigStep = false;
    [SerializeField]
    public bool bigBIGStep = false;

    //rough way to combine meshes
    private Mesh combineMeshes(Mesh m1, Mesh m2)
    {
        Vector3[] m2v = m2.vertices;
        int[] tris = new int[m2v.Length];

        for (int i = 0; i<m2v.Length; i++) 
        { 
            m2v[i] += cube.transform.localPosition;
            tris[i] = i+m1.triangles.Length;
        }
        m1.vertices = m1.vertices.Concat(m2v).ToArray();
        m1.triangles = m1.triangles.Concat(tris).ToArray();
        m1.RecalculateNormals();

        return m1;
    }

    // Update is called once per frame
    void Update()
    {
        if (step)
        {
            step = false;
            world.mesh = combineMeshes(world.mesh, cube.getMesh());
            cube.stepForward(MAPRANGE,MAPRANGE,MAPRANGE);
        }
        if (bigStep)
        {
            for(int i = 0; i<(MAPRANGE*MAPRANGE); i++)
            {
                bigStep = false;
                world.mesh = combineMeshes(world.mesh, cube.getMesh());
                cube.stepForward(MAPRANGE, MAPRANGE, MAPRANGE);
            }
        }
        if (bigBIGStep)
        {
            world.mesh = new Mesh();
            cube.regenerateMap();
            for (int i = 0; i < (MAPRANGE * MAPRANGE*MAPRANGE); i++)
            {
                bigBIGStep = false;
                world.mesh = combineMeshes(world.mesh, cube.getMesh());
                cube.stepForward(MAPRANGE, MAPRANGE, MAPRANGE);
            }
        }

    }
}
