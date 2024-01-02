using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanRaycaster : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public Ray ray;
    public RaycastHit hit;
    public float mZCoord;
    public Vector3 startPos;
    public GameObject scanner, monitor;
    public Camera main;
    public bool monitorPhase;
    public Animator monitorAnimator;
    public Vector3 mousePos;
    public Vector3 mousePosPrevious;
    private void Start()
    {
        main = Camera.main;
        startPos = scanner.transform.position;
    }
    private void Update()
    {
        if (!monitorPhase)
        {

            if (Input.GetMouseButton(0))
            {
                RayCast();
            }
        }
        else
        {

            if (Input.GetMouseButton(0))
            {
                MonitorDragging();
            }
            if (Input.GetMouseButtonUp(0))
            {
                monitorAnimator.speed = 0;
                mousePosPrevious = new Vector3(0, 0, 0);
            }
        }

    }
    public void RayCast()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask ignoreMe = ~IgnoreMe;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreMe))
        {
            if (hit.transform.tag == "Scanner" && !monitorPhase)
            {
                scanner.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            // if (hit.transform.tag == "Monitor" && monitorPhase)
            // {
            //     monitor.transform.localPosition = new Vector3(monitor.transform.localPosition.x, hit.point.y, monitor.transform.localPosition.z);
            // }
        }
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return main.ScreenToWorldPoint(mousePoint);
    }
    public void MonitorDragging()
    {
        mousePos = Input.mousePosition;
        if (mousePos.y < mousePosPrevious.y)
        {
            if (monitorAnimator != null)
                monitorAnimator.speed = 1;

        }
        else
        {
            if (monitorAnimator != null)
                monitorAnimator.speed = 0;

        }
        mousePosPrevious = mousePos;
    }

}

