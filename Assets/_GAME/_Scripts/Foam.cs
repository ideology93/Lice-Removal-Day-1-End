using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foam : MonoBehaviour
{
    public ParticleSystem ps;
    public bool isFoaming;
    public float time;
    public void StartFoaming()
    {
        isFoaming = true;
        ps.Play();
        AudioManager.Instance.PlaySound(4);
        if (time >= 7f)
        {
            AudioManager.Instance.StopSound(4);
            UI_Manager.Instance.guide_Vacuum.SetActive(false);
            UI_Manager.Instance.HappyEmoji.Play();
            RaycastController.Instance.vacuumRaycaster.enabled = false;
            ToolManager.Instance.foam.SetActive(false);
            UI_Manager.Instance.gelPanel.SetActive(false);
            CharacterGenerator.Instance.currentHairScript.TransitionRoots();
            UI_Manager.Instance.step2.SetActive(false);
            UI_Manager.Instance.step3.SetActive(true);  
        }
    }
    public void StopFoaming()
    {
        isFoaming = false;
        ps.Pause();
        AudioManager.Instance.StopSound(4);
    }

}
