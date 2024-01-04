using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CpuMarchingCubes
{
    public class SphereControl : MonoBehaviour
    {
        [SerializeField]
        [Range(0f,10f)]
        public static float targetValue = 6;

        [SerializeField]
        private GameObject sliderContainer;
        [SerializeField]
        Material Enabled;
        [SerializeField]
        Material Disabled;

        [SerializeField]
        private bool cornerEnabled = false;
        [SerializeField]
        private float value;

        public bool getEnabled() { return cornerEnabled; }
        public float getValue() { return value; }
        public void setValue(float val) { value = val; } 

        // Start is called before the first frame update
        void Start()
        {
            enabled = checkValue();
            updateColor();
            value = SliderControl.minValue;
        }

        private void OnMouseDown()
        {
            sliderContainer.SetActive(!sliderContainer.activeInHierarchy);
        }

        public void manualUpdate()
        {
            cornerEnabled = checkValue();
            updateColor();
            transform.parent.GetComponent<CubeMeshBuilderScriptInteractor>().makeMesh();
        }

        //checks if the corner has a visable value
        private bool checkValue()
        {
            return value >= targetValue;
        }

        private void updateColor()
        {
            GetComponent<Renderer>().material = cornerEnabled ? Enabled : Disabled;
        }
    }
}