using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweezers : MonoBehaviour
{
    private Vector3 startRotation;
    private void Start()
    {
        startRotation = transform.rotation.eulerAngles;
       
        transform.Rotate(new Vector3(0,0,90-startRotation.z));
    }
}

