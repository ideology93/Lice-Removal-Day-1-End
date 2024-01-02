using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;
public class FuzzRaycaster : MonoBehaviour
{
    public Ray ray, ray2;
    public RaycastHit hit, hit2;
    public Vector3 startPos, currentPos;
    private Camera main;
    public Fuzz fuzz;
    public GameObject comb;
    public LayerMask layer, fuzzHairLayer;
    public bool isDragging;

    private void Start()
    {
        main = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            MoveComb();
            // RayCast();
            Sklj();
        }
        if (Input.GetMouseButtonUp(0))
        {
            AudioManager.Instance.StopSound(1);
            if (fuzz != null)
            {
                fuzz.isTargeted = false;
                fuzz = null;
            }
            currentPos = new Vector3(0, 0, 0);
            isDragging = false;
            if (fuzz != null)
            {
                fuzz.isDragging = true;
            }
        }
    }


    public void MoveComb()
    {

        ray2 = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hit2, layer))
        {
            if (hit2.transform.tag == "FuzzPlane")
            {
                AudioManager.Instance.PlaySound(1);
                comb.transform.position = new Vector3(hit2.point.x, hit2.point.y, hit2.point.z);
            }
        }
    }
    public bool Dragger()
    {
        startPos = hit2.point;
        if (startPos.y < currentPos.y)
        {
            isDragging = true;
            if (fuzz != null)
                return true;
        }
        currentPos = startPos;
        isDragging = true;
        return false;
    }
    public void Sklj()
    {
        ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, fuzzHairLayer))
        {
            if (hit.transform.tag == "Fuzz")
            {
                fuzz = hit.transform.GetComponent<Fuzz>();
                fuzz.isTargeted = true;
                if (Dragger())
                {
                    fuzz.isDragging = true;
                    fuzz.RemoveFuzzy();
                    HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
                }
            }
        }
    }
}
