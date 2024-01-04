using ClickableCornerBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace slidableCornerBox
{
    public class SliderControl : MonoBehaviour
    {
        [SerializeField]
        private GameObject parentSphere;
        [SerializeField]
        public static float maxValue = 10;
        [SerializeField]
        public static float minValue = 0;

        private Slider slider;
        private SphereControl sphereControl;

        private void OnValueChange()
        {
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            sphereControl.setValue(slider.value);
            sphereControl.manualUpdate();
        }

        void Start()
        {
            slider = GetComponent<Slider>();
            sphereControl = parentSphere.GetComponent<SphereControl>();
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            slider.onValueChanged.AddListener(delegate { OnValueChange(); });
            slider.value = minValue;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
