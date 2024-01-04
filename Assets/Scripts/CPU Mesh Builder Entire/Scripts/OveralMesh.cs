using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OveralMesh : MonoBehaviour
{
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
            cube.stepForward(16,16,16);
        }
        if (bigStep)
        {
            for(int i = 0; i<(16*16); i++)
            {
                bigStep = false;
                world.mesh = combineMeshes(world.mesh, cube.getMesh());
                cube.stepForward(16, 16, 16);
            }
        }
        if (bigBIGStep)
        {
            for (int i = 0; i < (16 * 16*16); i++)
            {
                bigBIGStep = false;
                world.mesh = combineMeshes(world.mesh, cube.getMesh());
                cube.stepForward(16, 16, 16);
            }
        }

    }
}
