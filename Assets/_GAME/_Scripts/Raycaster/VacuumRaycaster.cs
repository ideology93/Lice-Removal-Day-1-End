using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using CrazyLabsExtension;
public class VacuumRaycaster : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public Ray ray;
    public RaycastHit hit;
    public GameObject vacuum, spray;
    public Vacuum vacuumScript;
    public Foam foamScript;
    public bool isSucking, vacuuming, sprayingPhase, isSpraying;
    public float time;
    private void Start()
    {
        vacuuming = true;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSucking && vacuuming)
            {
                isSucking = true;
                vacuumScript.vacuumParticle.Play();
                vacuumScript.vacuumCollider.enabled = true;
            }
            else if (sprayingPhase)
            {
                if (!isSpraying)
                {
                    isSpraying = true;
                }
            }

        }
        if (Input.GetMouseButton(0))
        {
            CastRay();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSucking = false;
            vacuumScript.vacuumParticle.Stop();
            vacuumScript.vacuumCollider.enabled = false;
            if (sprayingPhase)
            {
                isSpraying = false;
                foamScript.StopFoaming();
            }
        }

    }
    public void CastRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, IgnoreMe))
        {
            if (hit.transform.tag == "VacuumPlane")
            {
                if (vacuuming)
                {
                    Debug.Log("Hitpoint" + hit.point);
                    vacuum.transform.position = hit.point;
                    HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
                }
                else
                {
                    spray.transform.position = new Vector3(hit.point.x, spray.transform.position.y, hit.point.z);
                    foamScript.time += Time.deltaTime;
                    UI_Manager.Instance.gelSlider.value = foamScript.time;
                    foamScript.StartFoaming();
                    HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
                }
            }
        }
    }
    public void FinishPhase()
    {
        UI_Manager.Instance.HappyEmoji.Play();
    }
    public void StartSprayingPhsae()
    {
        vacuuming = false;
        isSucking = false;
        sprayingPhase = true;
        ToolManager.Instance.foam.SetActive(true);
    }
}
