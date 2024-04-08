#include "Marching_Cubes_Static_Values.cginc"
#ifndef MARCHINGCOMMONFUNCS
#define MARCHINGCOMMONFUNCS


//Shorthand to get edge index
//returns an int of the corner pair it references
//Either the first or second depending on Bool second
int getCornerIndex(int edgeindex, bool second)
{
    return EdgeVertexIndices[edgeindex][1 * second];
}
int getCornerIndex(int edgeindex, int pointIndex)
{
    return EdgeVertexIndices[edgeindex][pointIndex];
}
//returns Corner float3 point relative to input from corner index
float3 edgeLocFromPoint(float3 input, int cornerIndex)
{
    return CornerLocationRelative[cornerIndex] + input + float3(.5f, .5f, .5f);
}
float3 edgeLocFromPoint(float3 input, int edgeIndex, int pointIndex)
{
    return CornerLocationRelative[getCornerIndex(edgeIndex, pointIndex)] + input + float3(.5f, .5f, .5f);
}
//returns buffer relative position
int BufferPos(float3 id)
{
    return id.z + (id.y * SIZE_Z) + (id.x * SIZE_Z * SIZE_Y);
}
int BufferPos(int x, int y, int z)
{
    return z + (y * SIZE_Z) + (x * SIZE_Z * SIZE_Y);
}
int BufferPos(float3 id, int cX, int cY, int cZ)
{
    return BufferPos(id.x + cX, id.y + cY, id.z + cZ);
}
int BufferPos(float3 id, int cornerIndex)
{
    return BufferPos(edgeLocFromPoint(id, cornerIndex));
}

float3 temp(float3 id, int cornerIndex)
{
    return edgeLocFromPoint(id, cornerIndex);
}


int valuesBufferPos(float3 id)
{
    return id.z + (id.y * (SIZE_Z + 1)) + (id.x * (SIZE_Z + 1) * (SIZE_Y+1));
}
int valuesBufferPos(float3 id, int cornerIndex)
{
    return valuesBufferPos(edgeLocFromPoint(id, cornerIndex));
}
//returns buffer value from position
float PosVal(float3 id)
{
    return point_Values[valuesBufferPos(id)];
}
float PosVal(int x, int y, int z)
{
    return point_Values[valuesBufferPos(float3(x, y, z))];
}
float PosVal(float3 id, int cX, int cY, int cZ)
{
    return point_Values[valuesBufferPos(id + float3(cX, cY, cZ))];
}
float PosVal(float3 id, int CornerIndex)
{
    return point_Values[valuesBufferPos(id, CornerIndex)];
}

//given an ID and minvalue, returns the index for the tri Table Array first index
//Returns an index for the triangle array. Triangle Array contains edge index for reference for the edge between two points
int getTrianglesIndex(float3 id, float minValue)
{
    float val;
    int index = 0;
    int increase = 1;
    for (int i = 0; i < 8; i++)
    {
        val = PosVal(id, i);
        if (val >= minValue)
        {
            index += increase;
        }
        increase = increase << 1;
    }
    return index;
}

float3x3 getTriangleValues(float3 id, float minValue)
{
    float3x3 ret;
    /*
    ret = float3x3(
        float3(PosVal(id, 0), PosVal(id, 1), PosVal(id, 2)),
        float3(PosVal(id, 3), PosVal(id, 4), PosVal(id, 5)),
        float3(PosVal(id, 6), PosVal(id, 7), 99.7788)
    );*/
    ret = float3x3(
    float3(valuesBufferPos(id), 99.7788, 99.7788),
    id,
    float3(PosVal(id, 0), 99.7788, 99.7788)
    );
    return ret;
}

//returns 
//0:X 1:Y 2:Z
int getCornerSign(int index, int axis)
{
    return
		CornerLocationRelative[index].x * (axis == 0) +
		CornerLocationRelative[index].y * (axis == 1) +
		CornerLocationRelative[index].z * (axis == 2);
}
#endif