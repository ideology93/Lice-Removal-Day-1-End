using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject comb;
    public string animationName;


    private void Start()
    {
        CombController.Instance.currentHand = this.gameObject;
        // StartCoroutine(ActivateCombCoroutine());
        GetComponent<Animator>().Play(animationName);
    }
    public IEnumerator ActivateCombCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        comb.SetActive(true);
        GetComponent<Animator>().Play(animationName);
    }
    public void ActivateComb()
    {
        StartCoroutine(ActivateCombCoroutine());
    }
    // public void Next()
    // {
    //     CombController.Instance.StartCombing();
    // }
}
