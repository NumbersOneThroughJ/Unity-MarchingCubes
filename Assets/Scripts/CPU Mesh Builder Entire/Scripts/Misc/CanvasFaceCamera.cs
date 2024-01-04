using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CpuMarchingCubes
{
    public class CanvasFaceCamera : MonoBehaviour
    {
        private Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
            transform.Rotate(0, 180, 0);
        }
    }
}