using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace slidableCornerBox
{
    public class TargeteValue : MonoBehaviour
    {
        [SerializeField]
        CubeMeshBuilderScriptInteractor interactor;

        private Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate { onValueChange(); });
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onValueChange()
        {
            SphereControl.targetValue = slider.value;
            interactor.makeMesh();
        }
    }
}