using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCollider : MonoBehaviour
{

    public GameObject fill;
    public Animator fillAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
