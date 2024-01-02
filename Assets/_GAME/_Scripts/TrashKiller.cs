using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash" && !other.GetComponent<VacuumTrash>().isDestroyed)
        {
            other.GetComponent<VacuumTrash>().isDestroyed = true;
            UI_Manager.Instance.vacuumSlider.value += UI_Manager.Instance.trashPart;
            Destroy(other.gameObject, 0.03f);
            if (UI_Manager.Instance.vacuumSlider.value > 90)
            {
                //UI_Manager.Instance.guide_Vacuum.SetActive(false);
                UI_Manager.Instance.HappyEmoji.Play();
                RaycastController.Instance.vacuumRaycaster.vacuuming = false;
                RaycastController.Instance.vacuumRaycaster.enabled = false;
                UI_Manager.Instance.vacuumPanel.SetActive(false);
                UI_Manager.Instance.gelPanel.SetActive(true);
                ToolManager.Instance.vacuum.SetActive(false);
                ToolManager.Instance.foam.SetActive(true);
                
                RaycastController.Instance.vacuumRaycaster.StartSprayingPhsae();
                GameManager.Instance.vacuumTrash.SetActive(false);
            }
        }
    }
}
