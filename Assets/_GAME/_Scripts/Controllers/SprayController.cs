using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : GloballyAccessibleBase<SprayController>
{
    public Spray currentSpray;
    public GameObject currentSprayGameObject;
    public Transform spawnPos;

    public void SetSpray(Spray spray)
    {
        if (currentSprayGameObject != null)
        {
            Destroy(currentSprayGameObject);
        }
        currentSprayGameObject = Instantiate(spray.gameObject, spawnPos.position, spray.gameObject.transform.rotation);
        currentSpray = currentSprayGameObject.GetComponent<Spray>();
        RaycastController.Instance.extensionRaycaster.spray = currentSpray;
        RaycastController.Instance.extensionRaycaster.sprayGameObject = currentSprayGameObject;
        SetAnimation();
    }
    public void SetAnimation()
    {
        currentSpray.animator = CharacterGenerator.Instance.extensionAnimator;
    }
    public void StartSpray()
    {
        currentSpray.SelectSpray();
    }
}
