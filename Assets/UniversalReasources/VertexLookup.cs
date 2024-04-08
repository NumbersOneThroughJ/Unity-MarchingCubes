using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertexLookup
{
    public abstract class VertexCorneredCube : MonoBehaviour
    {
        // Axes are:
        //
        //      y
        //      |     z
        //      |   /
        //      | /
        //      +----- x
        //
        // Vertex and edge layout:
        //
        //            6             7
        //            +-------------+               +-----6-------+   
        //          / |           / |             / |            /|   
        //        /   |         /   |          11   7         10   5
        //    2 +-----+-------+  3  |         +-----+2------+     |   
        //      |   4 +-------+-----+ 5       |     +-----4-+-----+   
        //      |   /         |   /           3   8         1   9
        //      | /           | /             | /           | /       
        //    0 +-------------+ 1             +------0------+       
        //

        //based on the values of the array values
        //this checks if each value is greater than
        //the minimum value and returns the apropriate
        //Triangle Table index.
        protected int getTrianglesIndex(float minValue)
        {
            int index = 0;
            int increase = 1;
            foreach (float val in values)
            {
                if (val >= minValue)
                {
                    index += increase;
                }
                increase <<= 1;
            }
            return index;
        }

        //gets edgeIndex corosponding Positions relative to origin
        protected Vector3[] getEdgeIndicesPos(int index, Vector3 origin)
        {
            int[] edgeIndex = EdgeVertexIndices[index];
            return new Vector3[]
            {
                new Vector3(
                    origin.x + edgeLocationRelative[edgeIndex[0]].x,
                    origin.y + edgeLocationRelative[edgeIndex[0]].y,
                    origin.z + edgeLocationRelative[edgeIndex[0]].z),
                new Vector3(
                    origin.x + edgeLocationRelative[edgeIndex[1]].x,
                    origin.y + edgeLocationRelative[edgeIndex[1]].y,
                    origin.z + edgeLocationRelative[edgeIndex[1]].z)
            };
        }

        //Returns the requested Triangle array located at index
        protected int[] getTriangleArrayFromIndex(int index) { return TriangleTable[index]; }
        protected int[] getTriangleArrayFromMinVal(float minValue) { return TriangleTable[getTrianglesIndex(minValue)]; }
        protected int[] getEdgeIndices(int index) {  return EdgeVertexIndices[index]; } 

        //interpolates between 2 points
        public abstract Vector3 interpolate(int edgeVertexIndex);

        public abstract Vector3[] getMesh(float minValue);

        //values of each corner of the cube.
        public float[] values = new float[8];

        //relative location to the center of a cube
        protected static readonly Vector3[] edgeLocationRelative = 
        {
            new Vector3(-.5f,-.5f,-.5f),//0
            new Vector3(.5f,-.5f,-.5f),//1
            new Vector3(-.5f,.5f,-.5f),//2
            new Vector3(.5f,.5f,-.5f),//3
            new Vector3(-.5f,-.5f,.5f),//4
            new Vector3(.5f,-.5f,.5f),//5
            new Vector3(-.5f,.5f,.5f),//6
            new Vector3(.5f,.5f,.5f),//7

        };

        //referenced edge from triangle table
        static readonly int[][] EdgeVertexIndices = {
        new int[] {1, 0},
        new int[] {3, 1},
        new int[] {2, 3},
        new int[] {0, 2},
        new int[] {5, 4},
        new int[] {7, 5},
        new int[] {6, 7},
        new int[] {4, 6},
        new int[] {4, 0},
        new int[] {5, 1},
        new int[] {7, 3},
        new int[] {6, 2}
        };

        
        
        // For each MC case, a list of triangles, specified as triples of edge indices, terminated by -1
        static readonly int[][] TriangleTable = {
        new int[] {-1},
        new int[] {0, 3, 8, -1},
        new int[] {0, 9, 1, -1},
        new int[] {3, 8, 1, 1, 8, 9, -1},
        new int[] {2, 11, 3, -1},
        new int[] {8, 0, 11, 11, 0, 2, -1},
        new int[] {3, 2, 11, 1, 0, 9, -1},
        new int[] {11, 1, 2, 11, 9, 1, 11, 8, 9, -1},
        new int[] {1, 10, 2, -1},
        new int[] {0, 3, 8, 2, 1, 10, -1},
        new int[] {10, 2, 9, 9, 2, 0, -1},
        new int[] {8, 2, 3, 8, 10, 2, 8, 9, 10, -1},
        new int[] {11, 3, 10, 10, 3, 1, -1},
        new int[] {10, 0, 1, 10, 8, 0, 10, 11, 8, -1},
        new int[] {9, 3, 0, 9, 11, 3, 9, 10, 11, -1},
        new int[] {8, 9, 11, 11, 9, 10, -1},
        new int[] {4, 8, 7, -1},
        new int[] {7, 4, 3, 3, 4, 0, -1},
        new int[] {4, 8, 7, 0, 9, 1, -1},
        new int[] {1, 4, 9, 1, 7, 4, 1, 3, 7, -1},
        new int[] {8, 7, 4, 11, 3, 2, -1},
        new int[] {4, 11, 7, 4, 2, 11, 4, 0, 2, -1},
        new int[] {0, 9, 1, 8, 7, 4, 11, 3, 2, -1},
        new int[] {7, 4, 11, 11, 4, 2, 2, 4, 9, 2, 9, 1, -1},
        new int[] {4, 8, 7, 2, 1, 10, -1},
        new int[] {7, 4, 3, 3, 4, 0, 10, 2, 1, -1},
        new int[] {10, 2, 9, 9, 2, 0, 7, 4, 8, -1},
        new int[] {10, 2, 3, 10, 3, 4, 3, 7, 4, 9, 10, 4, -1},
        new int[] {1, 10, 3, 3, 10, 11, 4, 8, 7, -1},
        new int[] {10, 11, 1, 11, 7, 4, 1, 11, 4, 1, 4, 0, -1},
        new int[] {7, 4, 8, 9, 3, 0, 9, 11, 3, 9, 10, 11, -1},//Index 30
        new int[] {7, 4, 11, 4, 9, 11, 9, 10, 11, -1},
        new int[] {9, 4, 5, -1},
        new int[] {9, 4, 5, 8, 0, 3, -1},
        new int[] {4, 5, 0, 0, 5, 1, -1},
        new int[] {5, 8, 4, 5, 3, 8, 5, 1, 3, -1},
        new int[] {9, 4, 5, 11, 3, 2, -1},
        new int[] {2, 11, 0, 0, 11, 8, 5, 9, 4, -1},
        new int[] {4, 5, 0, 0, 5, 1, 11, 3, 2, -1},
        new int[] {5, 1, 4, 1, 2, 11, 4, 1, 11, 4, 11, 8, -1},
        new int[] {1, 10, 2, 5, 9, 4, -1},
        new int[] {9, 4, 5, 0, 3, 8, 2, 1, 10, -1},
        new int[] {2, 5, 10, 2, 4, 5, 2, 0, 4, -1},
        new int[] {10, 2, 5, 5, 2, 4, 4, 2, 3, 4, 3, 8, -1},
        new int[] {11, 3, 10, 10, 3, 1, 4, 5, 9, -1},
        new int[] {4, 5, 9, 10, 0, 1, 10, 8, 0, 10, 11, 8, -1},
        new int[] {11, 3, 0, 11, 0, 5, 0, 4, 5, 10, 11, 5, -1},
        new int[] {4, 5, 8, 5, 10, 8, 10, 11, 8, -1},
        new int[] {8, 7, 9, 9, 7, 5, -1},
        new int[] {3, 9, 0, 3, 5, 9, 3, 7, 5, -1},
        new int[] {7, 0, 8, 7, 1, 0, 7, 5, 1, -1},
        new int[] {7, 5, 3, 3, 5, 1, -1},
        new int[] {5, 9, 7, 7, 9, 8, 2, 11, 3, -1},
        new int[] {2, 11, 7, 2, 7, 9, 7, 5, 9, 0, 2, 9, -1},
        new int[] {2, 11, 3, 7, 0, 8, 7, 1, 0, 7, 5, 1, -1},
        new int[] {2, 11, 1, 11, 7, 1, 7, 5, 1, -1},
        new int[] {8, 7, 9, 9, 7, 5, 2, 1, 10, -1},
        new int[] {10, 2, 1, 3, 9, 0, 3, 5, 9, 3, 7, 5, -1},
        new int[] {7, 5, 8, 5, 10, 2, 8, 5, 2, 8, 2, 0, -1},
        new int[] {10, 2, 5, 2, 3, 5, 3, 7, 5, -1},
        new int[] {8, 7, 5, 8, 5, 9, 11, 3, 10, 3, 1, 10, -1},//index 60
        new int[] {5, 11, 7, 10, 11, 5, 1, 9, 0, -1},
        new int[] {11, 5, 10, 7, 5, 11, 8, 3, 0, -1},
        new int[] {5, 11, 7, 10, 11, 5, -1},
        new int[] {6, 7, 11, -1},
        new int[] {7, 11, 6, 3, 8, 0, -1},
        new int[] {6, 7, 11, 0, 9, 1, -1},
        new int[] {9, 1, 8, 8, 1, 3, 6, 7, 11, -1},
        new int[] {3, 2, 7, 7, 2, 6, -1},
        new int[] {0, 7, 8, 0, 6, 7, 0, 2, 6, -1},
        new int[] {6, 7, 2, 2, 7, 3, 9, 1, 0, -1},
        new int[] {6, 7, 8, 6, 8, 1, 8, 9, 1, 2, 6, 1, -1},
        new int[] {11, 6, 7, 10, 2, 1, -1},
        new int[] {3, 8, 0, 11, 6, 7, 10, 2, 1, -1},
        new int[] {0, 9, 2, 2, 9, 10, 7, 11, 6, -1},
        new int[] {6, 7, 11, 8, 2, 3, 8, 10, 2, 8, 9, 10, -1},
        new int[] {7, 10, 6, 7, 1, 10, 7, 3, 1, -1},
        new int[] {8, 0, 7, 7, 0, 6, 6, 0, 1, 6, 1, 10, -1},
        new int[] {7, 3, 6, 3, 0, 9, 6, 3, 9, 6, 9, 10, -1},
        new int[] {6, 7, 10, 7, 8, 10, 8, 9, 10, -1},
        new int[] {11, 6, 8, 8, 6, 4, -1},
        new int[] {6, 3, 11, 6, 0, 3, 6, 4, 0, -1},
        new int[] {11, 6, 8, 8, 6, 4, 1, 0, 9, -1},
        new int[] {1, 3, 9, 3, 11, 6, 9, 3, 6, 9, 6, 4, -1},
        new int[] {2, 8, 3, 2, 4, 8, 2, 6, 4, -1},
        new int[] {4, 0, 6, 6, 0, 2, -1},
        new int[] {9, 1, 0, 2, 8, 3, 2, 4, 8, 2, 6, 4, -1},
        new int[] {9, 1, 4, 1, 2, 4, 2, 6, 4, -1},
        new int[] {4, 8, 6, 6, 8, 11, 1, 10, 2, -1},
        new int[] {1, 10, 2, 6, 3, 11, 6, 0, 3, 6, 4, 0, -1},
        new int[] {11, 6, 4, 11, 4, 8, 10, 2, 9, 2, 0, 9, -1},
        new int[] {10, 4, 9, 6, 4, 10, 11, 2, 3, -1},
        new int[] {4, 8, 3, 4, 3, 10, 3, 1, 10, 6, 4, 10, -1},
        new int[] {1, 10, 0, 10, 6, 0, 6, 4, 0, -1},
        new int[] {4, 10, 6, 9, 10, 4, 0, 8, 3, -1},
        new int[] {4, 10, 6, 9, 10, 4, -1},
        new int[] {6, 7, 11, 4, 5, 9, -1},
        new int[] {4, 5, 9, 7, 11, 6, 3, 8, 0, -1},
        new int[] {1, 0, 5, 5, 0, 4, 11, 6, 7, -1},
        new int[] {11, 6, 7, 5, 8, 4, 5, 3, 8, 5, 1, 3, -1},
        new int[] {3, 2, 7, 7, 2, 6, 9, 4, 5, -1},
        new int[] {5, 9, 4, 0, 7, 8, 0, 6, 7, 0, 2, 6, -1},
        new int[] {3, 2, 6, 3, 6, 7, 1, 0, 5, 0, 4, 5, -1},
        new int[] {6, 1, 2, 5, 1, 6, 4, 7, 8, -1},
        new int[] {10, 2, 1, 6, 7, 11, 4, 5, 9, -1},
        new int[] {0, 3, 8, 4, 5, 9, 11, 6, 7, 10, 2, 1, -1},
        new int[] {7, 11, 6, 2, 5, 10, 2, 4, 5, 2, 0, 4, -1},
        new int[] {8, 4, 7, 5, 10, 6, 3, 11, 2, -1},
        new int[] {9, 4, 5, 7, 10, 6, 7, 1, 10, 7, 3, 1, -1},
        new int[] {10, 6, 5, 7, 8, 4, 1, 9, 0, -1},
        new int[] {4, 3, 0, 7, 3, 4, 6, 5, 10, -1},
        new int[] {10, 6, 5, 8, 4, 7, -1},
        new int[] {9, 6, 5, 9, 11, 6, 9, 8, 11, -1},
        new int[] {11, 6, 3, 3, 6, 0, 0, 6, 5, 0, 5, 9, -1},
        new int[] {11, 6, 5, 11, 5, 0, 5, 1, 0, 8, 11, 0, -1},
        new int[] {11, 6, 3, 6, 5, 3, 5, 1, 3, -1},
        new int[] {9, 8, 5, 8, 3, 2, 5, 8, 2, 5, 2, 6, -1},
        new int[] {5, 9, 6, 9, 0, 6, 0, 2, 6, -1},
        new int[] {1, 6, 5, 2, 6, 1, 3, 0, 8, -1},
        new int[] {1, 6, 5, 2, 6, 1, -1},
        new int[] {2, 1, 10, 9, 6, 5, 9, 11, 6, 9, 8, 11, -1},
        new int[] {9, 0, 1, 3, 11, 2, 5, 10, 6, -1},
        new int[] {11, 0, 8, 2, 0, 11, 10, 6, 5, -1},
        new int[] {3, 11, 2, 5, 10, 6, -1},
        new int[] {1, 8, 3, 9, 8, 1, 5, 10, 6, -1},
        new int[] {6, 5, 10, 0, 1, 9, -1},
        new int[] {8, 3, 0, 5, 10, 6, -1},
        new int[] {6, 5, 10, -1},
        new int[] {10, 5, 6, -1},
        new int[] {0, 3, 8, 6, 10, 5, -1},
        new int[] {10, 5, 6, 9, 1, 0, -1},
        new int[] {3, 8, 1, 1, 8, 9, 6, 10, 5, -1},
        new int[] {2, 11, 3, 6, 10, 5, -1},
        new int[] {8, 0, 11, 11, 0, 2, 5, 6, 10, -1},
        new int[] {1, 0, 9, 2, 11, 3, 6, 10, 5, -1},
        new int[] {5, 6, 10, 11, 1, 2, 11, 9, 1, 11, 8, 9, -1},
        new int[] {5, 6, 1, 1, 6, 2, -1},
        new int[] {5, 6, 1, 1, 6, 2, 8, 0, 3, -1},
        new int[] {6, 9, 5, 6, 0, 9, 6, 2, 0, -1},
        new int[] {6, 2, 5, 2, 3, 8, 5, 2, 8, 5, 8, 9, -1},
        new int[] {3, 6, 11, 3, 5, 6, 3, 1, 5, -1},
        new int[] {8, 0, 1, 8, 1, 6, 1, 5, 6, 11, 8, 6, -1},
        new int[] {11, 3, 6, 6, 3, 5, 5, 3, 0, 5, 0, 9, -1},
        new int[] {5, 6, 9, 6, 11, 9, 11, 8, 9, -1},
        new int[] {5, 6, 10, 7, 4, 8, -1},
        new int[] {0, 3, 4, 4, 3, 7, 10, 5, 6, -1},
        new int[] {5, 6, 10, 4, 8, 7, 0, 9, 1, -1},
        new int[] {6, 10, 5, 1, 4, 9, 1, 7, 4, 1, 3, 7, -1},
        new int[] {7, 4, 8, 6, 10, 5, 2, 11, 3, -1},
        new int[] {10, 5, 6, 4, 11, 7, 4, 2, 11, 4, 0, 2, -1},
        new int[] {4, 8, 7, 6, 10, 5, 3, 2, 11, 1, 0, 9, -1},
        new int[] {1, 2, 10, 11, 7, 6, 9, 5, 4, -1},
        new int[] {2, 1, 6, 6, 1, 5, 8, 7, 4, -1},
        new int[] {0, 3, 7, 0, 7, 4, 2, 1, 6, 1, 5, 6, -1},
        new int[] {8, 7, 4, 6, 9, 5, 6, 0, 9, 6, 2, 0, -1},
        new int[] {7, 2, 3, 6, 2, 7, 5, 4, 9, -1},
        new int[] {4, 8, 7, 3, 6, 11, 3, 5, 6, 3, 1, 5, -1},
        new int[] {5, 0, 1, 4, 0, 5, 7, 6, 11, -1},
        new int[] {9, 5, 4, 6, 11, 7, 0, 8, 3, -1},//this is the equivalent one to 160
        new int[] {11, 7, 6, 9, 5, 4, -1},
        new int[] {6, 10, 4, 4, 10, 9, -1},//This is index 160
        new int[] {6, 10, 4, 4, 10, 9, 3, 8, 0, -1},
        new int[] {0, 10, 1, 0, 6, 10, 0, 4, 6, -1},
        new int[] {6, 10, 1, 6, 1, 8, 1, 3, 8, 4, 6, 8, -1},
        new int[] {9, 4, 10, 10, 4, 6, 3, 2, 11, -1},
        new int[] {2, 11, 8, 2, 8, 0, 6, 10, 4, 10, 9, 4, -1},
        new int[] {11, 3, 2, 0, 10, 1, 0, 6, 10, 0, 4, 6, -1},
        new int[] {6, 8, 4, 11, 8, 6, 2, 10, 1, -1},
        new int[] {4, 1, 9, 4, 2, 1, 4, 6, 2, -1},
        new int[] {3, 8, 0, 4, 1, 9, 4, 2, 1, 4, 6, 2, -1},
        new int[] {6, 2, 4, 4, 2, 0, -1},
        new int[] {3, 8, 2, 8, 4, 2, 4, 6, 2, -1},
        new int[] {4, 6, 9, 6, 11, 3, 9, 6, 3, 9, 3, 1, -1},
        new int[] {8, 6, 11, 4, 6, 8, 9, 0, 1, -1},
        new int[] {11, 3, 6, 3, 0, 6, 0, 4, 6, -1},
        new int[] {8, 6, 11, 4, 6, 8, -1},
        new int[] {10, 7, 6, 10, 8, 7, 10, 9, 8, -1},
        new int[] {3, 7, 0, 7, 6, 10, 0, 7, 10, 0, 10, 9, -1},
        new int[] {6, 10, 7, 7, 10, 8, 8, 10, 1, 8, 1, 0, -1},
        new int[] {6, 10, 7, 10, 1, 7, 1, 3, 7, -1},
        new int[] {3, 2, 11, 10, 7, 6, 10, 8, 7, 10, 9, 8, -1},
        new int[] {2, 9, 0, 10, 9, 2, 6, 11, 7, -1},
        new int[] {0, 8, 3, 7, 6, 11, 1, 2, 10, -1},
        new int[] {7, 6, 11, 1, 2, 10, -1},
        new int[] {2, 1, 9, 2, 9, 7, 9, 8, 7, 6, 2, 7, -1},
        new int[] {2, 7, 6, 3, 7, 2, 0, 1, 9, -1},
        new int[] {8, 7, 0, 7, 6, 0, 6, 2, 0, -1},
        new int[] {7, 2, 3, 6, 2, 7, -1},
        new int[] {8, 1, 9, 3, 1, 8, 11, 7, 6, -1},
        new int[] {11, 7, 6, 1, 9, 0, -1},
        new int[] {6, 11, 7, 0, 8, 3, -1},
        new int[] {11, 7, 6, -1},
        new int[] {7, 11, 5, 5, 11, 10, -1},
        new int[] {10, 5, 11, 11, 5, 7, 0, 3, 8, -1},
        new int[] {7, 11, 5, 5, 11, 10, 0, 9, 1, -1},
        new int[] {7, 11, 10, 7, 10, 5, 3, 8, 1, 8, 9, 1, -1},
        new int[] {5, 2, 10, 5, 3, 2, 5, 7, 3, -1},
        new int[] {5, 7, 10, 7, 8, 0, 10, 7, 0, 10, 0, 2, -1},
        new int[] {0, 9, 1, 5, 2, 10, 5, 3, 2, 5, 7, 3, -1},
        new int[] {9, 7, 8, 5, 7, 9, 10, 1, 2, -1},
        new int[] {1, 11, 2, 1, 7, 11, 1, 5, 7, -1},
        new int[] {8, 0, 3, 1, 11, 2, 1, 7, 11, 1, 5, 7, -1},
        new int[] {7, 11, 2, 7, 2, 9, 2, 0, 9, 5, 7, 9, -1},
        new int[] {7, 9, 5, 8, 9, 7, 3, 11, 2, -1},
        new int[] {3, 1, 7, 7, 1, 5, -1},
        new int[] {8, 0, 7, 0, 1, 7, 1, 5, 7, -1},
        new int[] {0, 9, 3, 9, 5, 3, 5, 7, 3, -1},
        new int[] {9, 7, 8, 5, 7, 9, -1},
        new int[] {8, 5, 4, 8, 10, 5, 8, 11, 10, -1},
        new int[] {0, 3, 11, 0, 11, 5, 11, 10, 5, 4, 0, 5, -1},
        new int[] {1, 0, 9, 8, 5, 4, 8, 10, 5, 8, 11, 10, -1},
        new int[] {10, 3, 11, 1, 3, 10, 9, 5, 4, -1},
        new int[] {3, 2, 8, 8, 2, 4, 4, 2, 10, 4, 10, 5, -1},
        new int[] {10, 5, 2, 5, 4, 2, 4, 0, 2, -1},
        new int[] {5, 4, 9, 8, 3, 0, 10, 1, 2, -1},
        new int[] {2, 10, 1, 4, 9, 5, -1},
        new int[] {8, 11, 4, 11, 2, 1, 4, 11, 1, 4, 1, 5, -1},
        new int[] {0, 5, 4, 1, 5, 0, 2, 3, 11, -1},
        new int[] {0, 11, 2, 8, 11, 0, 4, 9, 5, -1},
        new int[] {5, 4, 9, 2, 3, 11, -1},
        new int[] {4, 8, 5, 8, 3, 5, 3, 1, 5, -1},
        new int[] {0, 5, 4, 1, 5, 0, -1},
        new int[] {5, 4, 9, 3, 0, 8, -1},
        new int[] {5, 4, 9, -1},
        new int[] {11, 4, 7, 11, 9, 4, 11, 10, 9, -1},
        new int[] {0, 3, 8, 11, 4, 7, 11, 9, 4, 11, 10, 9, -1},
        new int[] {11, 10, 7, 10, 1, 0, 7, 10, 0, 7, 0, 4, -1},
        new int[] {3, 10, 1, 11, 10, 3, 7, 8, 4, -1},
        new int[] {3, 2, 10, 3, 10, 4, 10, 9, 4, 7, 3, 4, -1},
        new int[] {9, 2, 10, 0, 2, 9, 8, 4, 7, -1},
        new int[] {3, 4, 7, 0, 4, 3, 1, 2, 10, -1},
        new int[] {7, 8, 4, 10, 1, 2, -1},
        new int[] {7, 11, 4, 4, 11, 9, 9, 11, 2, 9, 2, 1, -1},
        new int[] {1, 9, 0, 4, 7, 8, 2, 3, 11, -1},
        new int[] {7, 11, 4, 11, 2, 4, 2, 0, 4, -1},
        new int[] {4, 7, 8, 2, 3, 11, -1},
        new int[] {9, 4, 1, 4, 7, 1, 7, 3, 1, -1},
        new int[] {7, 8, 4, 1, 9, 0, -1},
        new int[] {3, 4, 7, 0, 4, 3, -1},
        new int[] {7, 8, 4, -1},
        new int[] {11, 10, 8, 8, 10, 9, -1},
        new int[] {0, 3, 9, 3, 11, 9, 11, 10, 9, -1},
        new int[] {1, 0, 10, 0, 8, 10, 8, 11, 10, -1},
        new int[] {10, 3, 11, 1, 3, 10, -1},
        new int[] {3, 2, 8, 2, 10, 8, 10, 9, 8, -1},
        new int[] {9, 2, 10, 0, 2, 9, -1},
        new int[] {8, 3, 0, 10, 1, 2, -1},
        new int[] {2, 10, 1, -1},
        new int[] {2, 1, 11, 1, 9, 11, 9, 8, 11, -1},
        new int[] {11, 2, 3, 9, 0, 1, -1},
        new int[] {11, 0, 8, 2, 0, 11, -1},
        new int[] {3, 11, 2, -1},
        new int[] {1, 8, 3, 9, 8, 1, -1},
        new int[] {1, 9, 0, -1},
        new int[] {8, 3, 0, -1},
        new int[] {-1}
    };
    }
}
