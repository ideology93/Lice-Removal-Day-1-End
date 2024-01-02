using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Spray : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public float time;
    public ParticleSystem ps;
    public Hair hair;
    public bool isSpraying, isFull;
    public int level;
    int amount;
    int counter;
    public Animator animator;
    public string animationName;
    public bool isPlaying;
    private void Start()
    {
        hair = CharacterGenerator.Instance.currentHairScript;
    }

    public void UseSpray()
    {
        time += Time.deltaTime;
        UI_Manager.Instance.extensionSlider.value = time;
        AudioManager.Instance.PlaySound(5);
        ps.Play();
        animator.speed = 1;
        if (time >= 3f)
        {
            RaycastController.Instance.extensionRaycaster.enabled = false;
            UI_Manager.Instance.extensionPanel.SetActive(false);
            UI_Manager.Instance.glow.Play();
            StartCoroutine(GameManager.Instance.EndGame());
            UI_Manager.Instance.guide_Spray.SetActive(false);
            mesh.enabled = false;
            ps.gameObject.SetActive(false);
            UI_Manager.Instance.step3.SetActive(false);
            UI_Manager.Instance.step4.SetActive(true);
        }

    }
    public void StopSpray()
    {
        AudioManager.Instance.StopSound(5);
        animator.speed = 0;
        isSpraying = false;
        ps.Stop();
    }
    public void SelectSpray()
    {
        animationName = level.ToString();
        animator.Play(animationName);
        animator.speed = 0;
    }


}
