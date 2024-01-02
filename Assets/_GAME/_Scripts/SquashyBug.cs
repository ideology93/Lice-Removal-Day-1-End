using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashyBug : MonoBehaviour
{
    public SkinnedMeshRenderer mesh, mesh2;
    public ParticleSystem ps;
    public Collider col;
    private void Start()
    {
        if (mesh != null)
        {
            mesh.enabled = false;
        }
        if (mesh2 != null)
        {
            mesh2.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TissueComb")
        {
            if (mesh != null)
            {
                mesh.enabled = true;
            }
            if (mesh2 != null)
            {
                mesh2.enabled = true;
            }
        }
    }
    public void Squash()
    {
        if (mesh != null)
        {
            mesh.SetBlendShapeWeight(0, 100);
        }
        if (mesh2 != null)
        {
            mesh2.SetBlendShapeWeight(0, 100);
        }
        ps.Play();
        UI_Manager.Instance.smashSlider.value += 1;
        if (UI_Manager.Instance.smashSlider.value == UI_Manager.Instance.smashSlider.maxValue - 1)
        {
            UI_Manager.Instance.guideSmash2.SetActive(false);
            StartCoroutine(VacuumPhase());
        }
    }
    public IEnumerator VacuumPhase()
    {

        UI_Manager.Instance.HappyEmoji.Play();
        RaycastController.Instance.smashRaycaster.enabled = false;
        yield return new WaitForSeconds(0.2f);
        CameraController.Instance.VacuumCamera();
        UI_Manager.Instance.smashPanel.SetActive(false);
        UI_Manager.Instance.guide_Smash.SetActive(false);
        GameManager.Instance.vacuumPhase.SetActive(true);
        UI_Manager.Instance.vacuumPanel.SetActive(true);
        UI_Manager.Instance.guide_Vacuum.SetActive(true);
        ToolManager.Instance.vacuum.SetActive(true);
        UI_Manager.Instance.step1.SetActive(false);
        UI_Manager.Instance.step2.SetActive(true);

    }


}
