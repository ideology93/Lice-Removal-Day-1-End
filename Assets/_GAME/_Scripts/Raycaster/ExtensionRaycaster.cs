using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;
public class ExtensionRaycaster : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public Camera main;
    public Spray spray;
    public GameObject sprayGameObject;
    public bool isPicked;
    public Animator animator;

    private void Start()
    {
        main = Camera.main;
        CameraController.Instance.ExtensionCamera();

    }
    private void Update()
    {

        if (isPicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                spray.UseSpray();
                // CharacterGenerator.Instance.currentHairScript.hairExtensionObject.GetComponent<Animator>().speed = 1;
            }
            if (Input.GetMouseButton(0))
            {
                RayCast();
                HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
            }
            if (Input.GetMouseButtonUp(0))
            {
                spray.StopSpray();
            }
        }
    }

    public void RayCast()
    {
        int layerMask = 1 << 7;
        ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            if (hit.transform.tag == "Extension")
            {
                if (spray != null)
                {
                    sprayGameObject.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.1f);
                    if (!spray.isSpraying)
                    {
                        spray.isSpraying = true;
                    }
                    spray.UseSpray();
                }
            }
        }
    }
    public void StartCast()
    {
        isPicked = true;
    }
}
