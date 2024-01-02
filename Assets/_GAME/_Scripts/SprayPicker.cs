using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPicker : MonoBehaviour
{
    public Spray spray;
    
    private void Start()
    {

    }
    public void SetSpray()
    {
        SprayController.Instance.SetSpray(spray);
    }
}
