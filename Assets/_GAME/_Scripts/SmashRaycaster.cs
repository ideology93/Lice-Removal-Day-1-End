using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CrazyLabsExtension;
public class SmashRaycaster : MonoBehaviour
{

    public Transform tissueLiceParent;
    public bool isSmashing;
    public int liceCount;
    public Ray ray;
    public RaycastHit hit;
    public Camera main;
    public GameObject smashObject;
    public bool animationFinished;
    public GameObject tissueComb;
    public Animator tissueCombAnimator;
    private Vector3 mousePos;
    private Vector3 mousePosPrevious;

    private void Start()
    {
        main = Camera.main;
        tissueCombAnimator = tissueComb.GetComponent<Animator>();
        tissueCombAnimator.Play("Comb_Tissue_Anim");
        tissueCombAnimator.speed = 0;
        liceCount = tissueLiceParent.childCount;
        UI_Manager.Instance.smashSlider.maxValue = liceCount;
    }
    private void Update()
    {
        if (animationFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RayCastSmash();
            }
        }
        if (!animationFinished)
        {

            if (Input.GetMouseButton(0))
            {
                RayCastSpread();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (tissueCombAnimator != null)
                tissueCombAnimator.speed = 0;
        }

    }
    public void RayCastSmash()
    {
        ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.tag == "Tissue" || hit.transform.tag == "TissueLice")
            {
                if (!isSmashing)
                {
                    isSmashing = true;

                    GameObject obj = Instantiate(smashObject, new Vector3(hit.point.x, 0.55f, hit.point.z), smashObject.transform.rotation);
                    obj.SetActive(true);

                    obj.transform.DOMove(new Vector3(hit.point.x, obj.transform.position.y - 0.09f, hit.point.z), 0.5f).OnComplete(() => OnCompleteTween(obj));

                    AudioManager.Instance.PlaySound(3);
                }
            }
        }
    }
    public void RayCastSpread()
    {
        // ray = main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit))
        // {
        //     if (hit.transform.tag == "TissueComb")
        //     {
        //         tissueCombAnimator.speed = 1;
        //     }
        // }



        // mousePos = Input.mousePosition;
        // if (mousePos.y < mousePosPrevious.y)
        // {
        //     if (tissueCombAnimator != null)
        //         tissueCombAnimator.speed = 1;
        //     AudioManager.Instance.PlaySound(1);
        //     HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
        // }
        // else
        // {
        //     if (tissueCombAnimator != null)
        //         tissueCombAnimator.speed = 0;
        //     AudioManager.Instance.StopSound(1);
        // }
        // mousePosPrevious = mousePos;
        tissueCombAnimator.speed = 1;
    }
    public void OnCompleteTween(GameObject o)
    {

        Destroy(o);
        isSmashing = false;

    }

}
