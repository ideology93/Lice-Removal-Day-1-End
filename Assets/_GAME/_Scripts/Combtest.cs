using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combtest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LiceOut")
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
