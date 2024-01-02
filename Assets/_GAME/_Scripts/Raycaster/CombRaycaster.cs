using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;
public class CombRaycaster : MonoBehaviour
{
    public GameObject currentComb;
    public Animator currentAnimator;
    public Vector3 mousePos;
    public Vector3 mousePosPrevious;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CastRay();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (currentAnimator != null)
            {

                currentAnimator.speed = 0;
                mousePosPrevious = new Vector3(0, 0, 0);
            }
        }
    }

    public void CastRay()
    {

        mousePos = Input.mousePosition;
        if (mousePos.y < mousePosPrevious.y)
        {
            if (currentAnimator != null)
                currentAnimator.speed = 1;
            AudioManager.Instance.PlaySound(1);
            HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
        }
        else
        {
            if (currentAnimator != null)
                currentAnimator.speed = 0;
            AudioManager.Instance.StopSound(1);
        }
        mousePosPrevious = mousePos;
    }
}
