using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VertexLookup;

namespace ClickableCornerBox
{
    public class CubeMeshBuilderScriptInteractor : VertexLookup.VertexCorneredCube
    {
        [SerializeField]
        private SphereControl[] Spheres = new SphereControl[8];
        [SerializeField]
        private GameObject mesh;

        /* converts the enabled status of the spheres into
         * usable data for the VertexLookup Class. 
         * 1f for on
         * 0f for off
         */
        private void loadSpheres()
        {
            for (int i = 0; i < Spheres.Length; i++)
            {
                values[i] = Spheres[i].getEnabled() ? 1f : 0f;
            }
        }

        //takes the values in verts and applies them to the mesh
        private void updateMesh(Vector3[] verts)
        {
            Mesh m = new Mesh();
            int[] inds = new int[verts.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                inds[i] = i;
            }
            //applying verts and tris
            m.vertices = verts;
            m.triangles = inds;
            m.RecalculateNormals();
            mesh.GetComponent<MeshFilter>().sharedMesh = m;
        }

        //hard interpolation, only the middle point
        public override Vector3 interpolate(int edgeVertexIndex)
        {
            //getting vects from getEdgeIndices
            Vector3[] vects = getEdgeIndicesPos(edgeVertexIndex, Vector3.zero);
            Vector3 start = vects[0]; Vector3 end = vects[1];

            //interpolating in between
            return new Vector3((start.x + end.x) / 2f, (start.y + end.y) / 2f, (start.z + end.z) / 2f);
        }

        public override Vector3[] getMesh(float minValue)
        {
            int[] TrianglesFromTable = getTriangleArrayFromMinVal(minValue);
            Vector3[] vertexes = new Vector3[TrianglesFromTable.Length - 1];
            for (int i = 0; TrianglesFromTable[i] != -1; i++)
            {
                vertexes[i] = interpolate(TrianglesFromTable[i]);
            }
            return vertexes;
        }

        public void makeMesh()
        {
            print("making mesh");
            loadSpheres();
            updateMesh(getMesh(.5f));
        }
    }
}