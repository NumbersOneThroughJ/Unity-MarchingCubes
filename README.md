# Unity Marching Cubes#

## Marching Cubes
Marching cubes is the process of creating a 3d mesh from a 3d array of single floating point values.
While this may sound daunting, luckily there are a set number of meshes that can be made within a single cube.

Specifically, 254 different shapes (256 if you count not having a shape as a shape). Its a lot.

Each mesh that can be created inside a cube consists of anywhere from 0 to 5 triangles, each a set of 3 Vector3 points (essentially three floats). 
To decide your cube's inner mesh shape, you do some bitwise shifting alongside adding to choose which mesh to make. Depending on which corners have 
values above the minimum or 'taget' value determines which mesh to make.
Simple enough, though the hard part of the algorithm and most complex is deciding where to put those triangle points.

How you do this is you have a target value you want to find and you compare that against what value 
is at the two ends of the line. Essentially, you interpolate where on the line a point should fall
by comparing that floating point value from a 3d array to your target value.

Just do this up to a maximum of 15 times however large you want a 3d marching cubes mesh to be and boom presto, you have a 3d marching cubes mesh.

To boil it down, you take a 3d float array and put a cube in there with the corners at relative index of the array then make a smaller mesh inside of the cube which will be part of a larger mesh.

## Purpose
I embarked on this large task soley for the reason that I wanted to push my skills. Unity was chosen for its simplicity, built in 3d rendering engine, and widely available online support.
Initially, I tried this project back in 2020, however I stopped at pushing the algorithm to the GPU. However, overcoming my fear of HLSL and Unity's compute shaders, I was able to overcome 
my past inabilities.

Standalone, the project generates a cave like environment or rather its inverse. Right now it only exists to generate pure random shapes. The values are 
generated via simplex noise. I implemented the 3d for the marching cubes large mesh and used the 2d for heighmap and terrain genration work.

Do note, I plan to split off the Heightmap and Terrain Generation to a seperate project with ComputeShader erosion on top of existing work. Though now, the heightmap does function.

After figuring out and becoming familiar with the algorithm on the cpu, I realized it was way to slow to do all this computation one at a time. So it forced me to learn how to use Shader code and how HLSL's work.
Previously I had stopped at this point, though this time I was able to overcome myself and managed to port code over the GPU!

Whereas before it took 10-20 seconds to generate a 8x8 mesh of marching cubes, porting it to the GPU I can do 96 individual 8x8 meshes many times a frame. This allows for 
live updates to the noise generation to view many possibilities and change current shape of the mesh in real time.

> [Benjamin Ward's Simplex Noise (no relation)] (https://github.com/WardBenjamin/SimplexNoise)

## Overview
This project comes with a couple of different versions inside the assets folder each with a subobjects folder containing 
demonstration object to showcase each step of the project. The name of the folder is in parenthises.
 - Static Point Interpolation (Clickable Corner Boxes)
   - In this version, the premade object of note is a cube with clickable spheres on each corner.
     clicking the spheres will toggle them on or off. This demonstrates the function ability to choose the correct submesh to build inside of the cube.
     There is no interpolation for this object.
 - Sliding Point Interpolation (Sliding Corner)
   - In this version, it is essentially the same as the previous, however this time it include interpolation. Upon clicking a corner a slider will show.
     Editing this slider changes the value of that corner. There is a threshold you have to meet for the corner to be considered "on" as defined in the sphere script's target value,
     however you can watch as the shapes inside of the cube grow and expand as you change each corner's value. You can also use the left and right arrow keys to rotate the cube.
 - CPU Marching Cubes Whole (CPU Mesh Builder Entire)
   - This version includes two objects of note, the mesh builder and a Unity height map. The height map allows for playing with Terrain Generation, though it was a side project before moving to the GPU for
     building the mesh and not fully implemented with Marching cubes despite working. The 'Main Mesh' object is the object of true note. In this prefab object, you can click the boolean boxes
     as if they were buttons in order to cause the mesh to generate at a point then move. The objects 3d array of values is set from the simplex noise algorithm.
     - step generates one mesh then moves
     - big step generates one layer
     - big big step generates the whole max size cube
     **DOWNSIDE** it is slow to do the full big big step and not very phesable for showcasing the power of simplex noise and this.
 - GPU Marching Cubes Whole (GPU Mesh Builder Entire)
  - This version is the magnum opis of this project. It makes the generated cube from the cpu seem miniscule in size, just becuase it is. The object of note is the GPU Terrain.
    Using the editor, position yourself to be able to view the mesh after pressing play. Then within the GPU Terrain's subobject (Full terrain) editor, you can change the scale and max values of the
    noise generation settings. Changing the scale will shrink or increase the size of the displayed shapes. Changing the max is essentially changing the target value and will change the shape of the generated
    meshes. You can also move around Full Terrain within the GPU Terrain object to move the terrain as if it were a continuous terrain (With a slight bit of choppiness).
    * The sub terrain objects are made to be movable and will locate where they should generate by combining the local position of all parent objects that don't have a parent.
    * Choose the grandest parent object of the bunch to move all without changing the generated interior.
    It can be improved by deciding which vertices to combine and porting over the vertex indexing code to the gpu, however at this state it is very much functional.
