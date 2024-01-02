using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LiquidBottle : MonoBehaviour
{
    public MeshRenderer liquidMesh, bottleMesh;
    private GameObject bottlePrefab;
    public ParticleSystem dropParticles;
    public Transform particleTransform;
    bool hasDripped;
    public bool isPouring;
    public float time;
    private bool isFinished;
    private void Start()
    {
        CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.SetActive(false);
        bottlePrefab = this.gameObject;
        RaycastController.Instance.liquidRaycaster.bottle = bottlePrefab;
        this.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (time >= 6f)
        {
            if (!isFinished)
            {
                isFinished = true;
                dropParticles.Stop();
                UI_Manager.Instance.guide_Liquid.SetActive(false);
                UI_Manager.Instance.liquidPanel.SetActive(false);
                RaycastController.Instance.liquidRaycaster.enabled = false;
                CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().hairBase.SetActive(true);
                CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().hair.paintableHair.GetComponent<CurlHair>().PlayAnimation();
                // CharacterGenerator.Instance.currentCharacter.GetComponent<Character>().hairBase.GetComponent<Animator>().Play("Spawn");
                UI_Manager.Instance.HappyEmoji.Play();
               
                liquidMesh.enabled = false;
                bottleMesh.enabled = false; 
                StartCoroutine(GameManager.Instance.StartCombingPhase());
            }
        }
    }
    [Button]
    public void Drip()
    {
        StartCoroutine(DripCoroutine());
    }
    public void Pour()
    {

        dropParticles.Play();
        AudioManager.Instance.PlaySound(2);

    }
    public void StopPouring()
    {
        isPouring = false;
        dropParticles.Stop();
        AudioManager.Instance.StopSound(2);
    }
    public IEnumerator DripCoroutine()
    {
        if (!hasDripped)
        {
            hasDripped = true;
            ParticleSystem ps = Instantiate(dropParticles, particleTransform.transform.position, particleTransform.transform.rotation, particleTransform);
            ps.Play();
            
            yield return new WaitForSeconds(0.75f);
            hasDripped = false;
        }

    }




}
