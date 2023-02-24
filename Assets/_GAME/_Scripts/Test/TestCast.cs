using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

namespace NLO
{
    public class TestCast : MonoBehaviour
    {
        public Ray ray;
        public P3dPaintSphere p3d;
        public P3dBlendMode p3dbm;
        public RaycastHit hit;
        public Camera mainCamera;
        public GameObject shower;
        public GameObject comb;
        public Vector3 incomingVec;
        public Vector3 testPoint;
        public Vector3 testNormal;
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
                CastRay();
        }

        public void CastRay()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Shower")
                {
                    shower.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    shower.transform.localRotation = Quaternion.LookRotation(new Vector3(hit.normal.x, hit.normal.y, shower.transform.position.z+2));
                }
                // testPoint = hit.point;
                // testNormal = hit.normal;
                // shower.transform.position = testPoint;
                // incomingVec = hit.point - shower.transform.position;
                // Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                // // brush.transform.rotation = Quaternion.FromToRotation(brush.transform.up, testNormal)*brush.transform.rotation ;
                // shower.transform.localRotation = Quaternion.LookRotation(hit.normal);
                // // brush.transform.rotation = Vector3.Angle(hit.normal, brush.transform.position);
                // // brush.transform.rotation = Quaternion.Slerp(brush.transform.rotation, hit.normal.normalized, Time.deltaTime * 1);




            }

        }
        public void ControlBrush()
        {
            P3dBlendMode blendMode = P3dBlendMode.ReplaceOriginal(Vector4.one);
            p3d.BlendMode = blendMode;
        }
    }
}
