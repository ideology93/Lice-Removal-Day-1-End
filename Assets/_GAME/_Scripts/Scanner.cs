using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Scanner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trigger")
        {
            GetComponent<BoxCollider>().enabled = false;
            UI_Manager.Instance.guideScan.SetActive(false);
            RaycastController.Instance.scanRaycaster.monitorPhase = true;
            GameManager.Instance.scanPhase.SetActive(false);
            transform.DOMove(other.transform.position, 1f).OnComplete(OnComplete);
            // transform.position = other.transform.position;
            ToolManager.Instance.monitor.GetComponent<Move>().PlayTween();
        }
    }
    private void OnComplete()
    {
        CameraController.Instance.ScanCamera(0f);
    }
}
