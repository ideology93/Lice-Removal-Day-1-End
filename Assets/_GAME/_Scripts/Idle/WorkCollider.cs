using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkCollider : MonoBehaviour
{
    public bool isInUse, isDone;
    public Chair chair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IdleManager.Instance.currentChair = chair;
            Idle_UIManager.Instance.panelWork.SetActive(true);
            IdleManager.Instance.targetIndicator.arrowMesh.enabled = false;
            Debug.Log("Disabling mesh");
            IdleManager.Instance.currentRequest = chair.currentRequest;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IdleManager.Instance.currentChair = null;
        Idle_UIManager.Instance.panelWork.SetActive(false);
        IdleManager.Instance.targetIndicator.arrowMesh.enabled = true;
        IdleManager.Instance.currentRequest = 0;
    }
}
