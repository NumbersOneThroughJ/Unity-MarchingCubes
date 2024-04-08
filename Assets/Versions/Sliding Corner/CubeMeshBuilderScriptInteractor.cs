using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VertexLookup;

namespace slidableCornerBox
{
    public class CubeMeshBuilderScriptInteractor : VertexLookup.VertexCorneredCube
    {
        [SerializeField]
        public slidableCornerBox.SphereControl[] Spheres = new slidableCornerBox.SphereControl[8];
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
                values[i] = Spheres[i].getValue();
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
            int[] indicies = getEdgeIndices(edgeVertexIndex);

            //interpolating in between
            return
                calculateInterpolation(
                    SphereControl.targetValue,
                    start,
                    end,
                    values[indicies[0]],
                    values[indicies[1]]
                );
        }

        private Vector3 calculateInterpolation(float targetValue, Vector3 p1, Vector3 p2, float p1Val, float p2Val)
        {
            float mu;
            Vector3 returnPoint;
            if (Mathf.Abs(targetValue - p1Val) < .0001) return p1;
            if (Mathf.Abs(targetValue - p2Val) < .0001) return p2;
            if (Mathf.Abs(p1Val - p2Val) < .0001) return p1;

            mu = (targetValue - p1Val) / (p2Val - p1Val);
            
            returnPoint.x = (p1.x + (mu * (p2.x - p1.x)));
            returnPoint.y = (p1.y + (mu * (p2.y - p1.y)));
            returnPoint.z = (p1.z + (mu * (p2.z - p1.z)));

            return returnPoint;
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
            loadSpheres();
            updateMesh(getMesh(SphereControl.targetValue));
        }
    }
}