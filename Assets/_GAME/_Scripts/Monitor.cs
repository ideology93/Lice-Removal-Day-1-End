using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monitor : MonoBehaviour
{
    public MeshRenderer mesh;
    public GameObject scanner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trigger")
        {
            RaycastController.Instance.scanRaycaster.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            UI_Manager.Instance.guideMonitor.SetActive(false);
            RaycastController.Instance.scanRaycaster.monitorPhase = true;
            GameManager.Instance.scanPhase.SetActive(false);
            transform.DOMove(other.transform.position, 1f).OnComplete(() => OnComplete());

        }
    }
    public void FinishAnim()
    {
        RaycastController.Instance.scanRaycaster.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        UI_Manager.Instance.guideMonitor.SetActive(false);
        RaycastController.Instance.scanRaycaster.monitorPhase = true;
        GameManager.Instance.scanPhase.SetActive(false);
        OnComplete();
    }
    private void OnComplete()
    {
        mesh.enabled = true;
        CameraController.Instance.SittingCamera(3f);
        GameManager.Instance.monitorPhase.SetActive(false);

        StartCoroutine(StartNextPhase());
    }
    private IEnumerator StartNextPhase()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.CleanPhase();
        UI_Manager.Instance.garbagePanel.SetActive(true);
        UI_Manager.Instance.guide_Garbage.SetActive(true);
        scanner.transform.DOMove(new Vector3(scanner.transform.position.x + 3, scanner.transform.position.y, scanner.transform.position.z), 2f);
        GetComponent<Animator>().enabled = false;
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), 2f);

    }

}
