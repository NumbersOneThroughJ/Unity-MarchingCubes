using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickableCornerBox
{
    public class CubeRotator : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 100f)]
        float rotateAmount = 1.0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 rotateAmount3 = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.RightArrow)) rotateAmount3 += new Vector3(0f, -1f, 0f);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) rotateAmount3 += new Vector3(0f, 1f, 0f);
            gameObject.transform.Rotate(rotateAmount * rotateAmount3);
        }

    }
}