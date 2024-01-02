using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CrazyLabsExtension;
public class LiquidRaycaster : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public Camera main;
    public GameObject bottle;
    public LiquidBottle bottleScript;
    private float mZCoord;
    private Vector3 mOffset;
    private bool isDragging;
    private Vector3 startPos;
    public GameObject sphereCollider;

    private void Start()
    {
        main = Camera.main;
        startPos = bottle.transform.position;
        bottleScript = bottle.GetComponent<LiquidBottle>();

    }

    public void RayCast()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "HeadCollider")
                {
                    bottleScript.time += Time.deltaTime;
                    UI_Manager.Instance.liquidSlider.value = bottleScript.time;
                    bottle.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.05f);
                    bottle.transform.forward = -hit.normal;
                    HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.Selection);
                }
            }
        }
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (!bottleScript.isPouring)
            {
                bottleScript.Pour();
            }
        }
        if (Input.GetMouseButton(0))
        {
            AudioManager.Instance.PlaySound(2);
            RayCast();
        }
        if (Input.GetMouseButtonUp(0))
        {
            bottleScript.StopPouring();
        }
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



}
