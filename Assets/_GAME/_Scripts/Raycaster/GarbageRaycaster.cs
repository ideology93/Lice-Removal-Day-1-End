using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageRaycaster : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public Camera main;
    public Transform[] trashPositions;

    private void Start()
    {
        main = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCast();
        }
    }
    public void RayCast()
    {
        ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Garbage")
            {
                hit.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.05f);
            }
        }
    }
}
