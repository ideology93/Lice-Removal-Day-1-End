using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMarker : MonoBehaviour
{
    public GameObject fill;
    public GameObject col;
    public Animator animator;

    public void Buy()
    {
        IdleManager.Instance.UpgradeSalon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IdleManager.Instance.cash >= IdleManager.Instance.baseSalonPrice)
            if (other.tag == "Player")
            {
                animator.SetBool("flag", true);
            }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("flag", false);

    }
}
