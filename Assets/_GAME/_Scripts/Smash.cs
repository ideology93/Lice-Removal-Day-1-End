using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TissueLice")
        {
            if (other.transform.GetComponent<SquashyBug>() != null)
            {
                other.transform.GetComponent<SquashyBug>().Squash();
            }
        }
    }
}
