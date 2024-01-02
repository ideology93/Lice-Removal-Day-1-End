using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class Chair : MonoBehaviour
{
    public BoxCollider chairCollider;
    public Transform moneyPos;
    public List<GameObject> chairMeshes = new List<GameObject>();
    public Image requestSprite;
    public Marker purchaseMarker;
    public bool isUnlocked;
    public GameObject locked, unlocked;
    public Renderer rend;
    public ParticleSystem happy, angry;
    public Sprite sprite;
    public bool hasCustomer;
    public GameObject customer;
    public GameObject marker;
    public GameObject canvas;
    public int currentRequest;
    public Transform target;

    private void Start()
    {
        if (IdleManager.Instance.unlockedChairs.Contains(GetComponent<Chair>()))
        {
            unlocked.SetActive(true);
            SetChair();
            int chance = Random.Range(0, 6);
            if (chance == 0 || chance == 1 || chance == 2 || chance == 3 || chance == 4)
            {
                if (IdleManager.Instance.session)
                {
                    SpawnCustomer();
                }
            }
            else
            {
                RemoveCustomer();
            }
        }
        else
            locked.SetActive(true);

    }
    public void RemoveCustomer()
    {
        hasCustomer = false;
        customer.SetActive(false);
        marker.SetActive(false);
        canvas.SetActive(false);
    }
    [Button]
    public void SpawnCustomer()
    {
        hasCustomer = true;
        customer.SetActive(true);
        marker.SetActive(true);
        canvas.SetActive(true);
        currentRequest = Random.Range(0, IdleManager.Instance.requests.Count);
        requestSprite.sprite = IdleManager.Instance.requests[currentRequest];
    }
    public void Fill()
    {
        rend.material.SetFloat("_Arc2", rend.material.GetFloat("_Arc2") - 360 / 12);
    }
    [Button]
    public void PlayHearts()
    {
        Instantiate(happy, transform.position, happy.transform.rotation);

    }
    public void PlayAngry()
    {
        Instantiate(angry, transform.position, angry.transform.rotation);

    }
    public void SetChair()
    {
        for (int i = 0; i < chairMeshes.Count; i++)
        {
            if (i == PlayerPrefs.GetInt("salonLevel"))
            {
                chairMeshes[i].SetActive(true);

            }
            else
                chairMeshes[i].SetActive(false);
        }
    }
}
