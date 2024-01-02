using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CurlHair : MonoBehaviour
{

    public Animator animator;
    public string animationName;
    public void PlayAnimation()
    {
        CharacterGenerator.Instance.currentHairScript.liceParent.SetActive(false);
        animator.Play(animationName);
    }
    public void TogglePundja()
    {
        CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().hairBase.GetComponent<Animator>().Play("Spawn");
    }
    public void TurnOff()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);
    }


}
