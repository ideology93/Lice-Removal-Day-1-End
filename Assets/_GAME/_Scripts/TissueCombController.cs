using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueCombController : MonoBehaviour
{
    public void FinishAnimation()
    {
        UI_Manager.Instance.guide_Smash.SetActive(false);
        UI_Manager.Instance.guideSmash2.SetActive(true);
        RaycastController.Instance.smashRaycaster.animationFinished = true;
        Destroy(this.gameObject);
    }
}
