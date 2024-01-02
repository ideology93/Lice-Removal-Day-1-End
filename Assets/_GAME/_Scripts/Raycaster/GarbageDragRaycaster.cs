using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;
public class GarbageDragRaycaster : MonoBehaviour
{
    public Ray ray;
    public Garbage garbage;
    public RaycastHit hit;
    float mZCoord;
    Vector3 mOffset;
    public GameObject obj;
    public bool isDragging;
    public float startPos;
    public float testOffset;
    public GameObject tweezersPrefab;
    public GameObject tweezers;
    private void Start()
    {
        CameraController.Instance.instagramCamera.GetComponent<SR_RenderCamera>().CamCaptureBefore();
        //startPos = obj.transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Garbage")
                {
                    isDragging = true;

                }
            }
        }

        if (isDragging)
        {
            // Vector3 pos = MousePos();
            if (obj != null)
            {
                obj.transform.position = GetMouseAsWorldPoint() + mOffset;
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, startPos);
                HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.Success);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (garbage != null)
            {
                if (garbage.GetComponent<Animator>() != null)
                    garbage.GetComponent<Animator>().enabled = false;
                garbage.RemoveGarbage();
                tweezers.SetActive(false);

                garbage.DropGarbage();
                garbage = null;
            }
        }

    }


    private Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, obj.transform.position.z));
    }
    private Vector3 GetMouseAsWorldPoint()

    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord + testOffset;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    public void SelectObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Garbage")
            {
                if (garbage == null || hit.transform.GetComponent<Garbage>() != garbage)
                {
                    garbage = hit.transform.GetComponent<Garbage>();
                    obj = garbage.transform.gameObject;
                    mZCoord = Camera.main.WorldToScreenPoint(obj.transform.position).z;
                    mOffset = obj.transform.position - GetMouseAsWorldPoint();
                    startPos = obj.transform.position.z - 0.05f;
                    Instantiate(garbage.removalParticle, garbage.transform.position, garbage.removalParticle.transform.rotation);
                    if (garbage.garbageMeshRenderer != null)
                    {
                        tweezers = Instantiate(tweezersPrefab, garbage.garbageMeshRenderer.bounds.center, tweezersPrefab.transform.rotation);
                        tweezers.transform.localScale = tweezersPrefab.transform.localScale;
                        tweezers.transform.parent = garbage.transform;

                    }
                    else
                    {
                        tweezers = Instantiate(tweezersPrefab, garbage.garbageSkinnedMeshRenderer.bounds.center, tweezersPrefab.transform.rotation);
                        tweezers.transform.localScale = tweezersPrefab.transform.localScale;
                        tweezers.transform.parent = garbage.transform;
                    }

                }
            }
        }
    }
}
