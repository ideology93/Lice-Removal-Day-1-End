using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public Transform TrashParent;
    public int totalTrash, gatheredTrash, trashPart;

    private void Start()
    {
        
        for (int i = 0; i < TrashParent.childCount; i++)
        {
            int rand = Random.Range(0, 3);
            if (rand == 0 || rand == 1)
            {
                totalTrash++;
                TrashParent.GetChild(i).gameObject.SetActive(true);
            }
        }
        float trashCount = totalTrash;
        float part = 100f / trashCount;
        trashPart = Mathf.CeilToInt(part);
        UI_Manager.Instance.trashPart = part;

    }
}
