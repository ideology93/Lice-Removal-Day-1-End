using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class Garbage : MonoBehaviour
{
    public bool isRemoved;
    public ParticleSystem removalParticle;
    public GameObject tweezers;
    public MeshRenderer garbageMeshRenderer;
    public SkinnedMeshRenderer garbageSkinnedMeshRenderer;
    public int garbagePart;

    private void Start()
    {
        garbagePart = CharacterGenerator.Instance.currentHairScript.GarbagePart;
    }
    public void RemoveGarbage()
    {
        if (!isRemoved)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            AudioManager.Instance.PlaySoundWithClipping(0);
            isRemoved = true;
            UI_Manager.Instance.garbageRemoved++;
            UI_Manager.Instance.garbageSlider.value += garbagePart;
            
            if (removalParticle != null)
            {
                ParticleSystem ps = Instantiate(removalParticle, transform.position, Quaternion.identity);
                ps.Play();
            }
            if (UI_Manager.Instance.garbageSlider.value >= 85f)
            {
                for (int i = 0; i < CharacterGenerator.Instance.currentHairScript.Garbage.transform.childCount; i++)
                {
                    CharacterGenerator.Instance.currentHairScript.Garbage.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                }
                UI_Manager.Instance.garbageSlider.value = 100f;
                UI_Manager.Instance.HappyEmoji.Play();
                UI_Manager.Instance.garbagePanel.SetActive(false);
                RaycastController.Instance.garbageDragRaycaster.enabled = false;
               
                StartCoroutine(GameManager.Instance.FuzzPhase());
                UI_Manager.Instance.guide_Garbage.SetActive(false);
                UI_Manager.Instance.step0.SetActive(false);
                UI_Manager.Instance.step1.SetActive(true);
            }
        }
    }
    public void DeleteGarbage()
    {
        Destroy(this.gameObject, 1.3f);
    }
    public void DropGarbage()
    {
        isRemoved = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        DeleteGarbage();
    }
    public void ToggleTweezers()
    {
        //tweezers.SetActive(!tweezers.activeSelf);
    }
}
