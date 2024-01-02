using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extensions : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    public Spray spray;
    public int value;
    private bool isExtending;
    private void Start()
    {
        spray = RaycastController.Instance.extensionRaycaster.spray;
        value = 100 / spray.level;
    }

    public IEnumerator Extend()
    {
        if (smr.GetBlendShapeWeight(0) < value && spray.isSpraying)
            smr.SetBlendShapeWeight(0, smr.GetBlendShapeWeight(0) + 1);
        else
        {
            spray.gameObject.SetActive(false);
        }
        yield return null;
    }
}
