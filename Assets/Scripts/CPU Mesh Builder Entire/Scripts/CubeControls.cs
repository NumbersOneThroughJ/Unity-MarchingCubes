using CpuMarchingCubes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControls : MonoBehaviour
{
    [SerializeField]
    CubeMeshBuilderScriptInteractor builder;
    [SerializeField]
    public MeshFilter cubeMesh;


    public void setLocation(float x, float y, float z) {
        transform.localPosition = new Vector3(x + .5f, y + .5f, z + .5f);
    }

    public void stepForward(int limitx, int limity, int limitz)
    {
        Vector3 localpos = transform.localPosition;
        localpos.x++;

        if(localpos.x>limitx-1)
        {
            localpos.x = .5f;
            localpos.z++;
        }
        if (localpos.z > limitz-1)
        {
            localpos.z = .5f;
            localpos.y++;
        }
        if (localpos.y > limity-1)
        {
            localpos.y = .5f;
        }

        transform.localPosition = localpos;
    }
    public Mesh getMesh()
    {
        builder.makeMesh();
        return cubeMesh.mesh;
    }
}
