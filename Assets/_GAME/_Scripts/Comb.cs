using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comb : MonoBehaviour
{
    public Transform liceParent;
    public ParticleSystem ps;
    public Stack<GameObject> liceStack = new Stack<GameObject>();
    public string AnimationName;
    private void Start()
    {
        CombController.Instance.currentComb = this.gameObject;
        RaycastController.Instance.combRaycaster.currentAnimator = GetComponent<Animator>();
        GetComponent<Animator>().Play(AnimationName);
        GetComponent<Animator>().speed = 0;
        for (int i = 0; i < liceParent.childCount; i++)
        {
            liceStack.Push(liceParent.GetChild(i).gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Lice")
        {

            if (liceStack.Count > 0)
            {
                liceStack.Peek().SetActive(true);
                liceStack.Pop();
            }
            other.transform.GetComponent<Lice>().Remove();
            ps.Play();
        }
    }
    public void Next()
    {
        CombController.Instance.StartCombing();
    }
}
