using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickableCornerBox
{
    public class SphereControl : MonoBehaviour
    {
        [SerializeField]
        private int cornerIndex;
        [SerializeField]
        Material Enabled;
        [SerializeField]
        Material Disabled;

        [SerializeField]
        private bool cornerEnabled = false;

        public bool getEnabled() { return cornerEnabled; }

        // Start is called before the first frame update
        void Start()
        {
            updateColor();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {
            cornerEnabled = !cornerEnabled;
            updateColor();
            transform.parent.GetComponent<CubeMeshBuilderScriptInteractor>().makeMesh();
        }

        private void updateColor()
        {
            GetComponent<Renderer>().material = cornerEnabled ? Enabled : Disabled;
        }
    }
}