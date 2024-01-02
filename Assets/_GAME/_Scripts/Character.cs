using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Character : MonoBehaviour
{
    public enum CharacterGender
    {
        Male, Female
    }
    public GameObject glasses;
    public GameObject hairBase;
    public GameEvent StartMenuEvent;
    public GameObject dirtyParticles;
    public Hair hair;
    public List<Hair> hairs = new List<Hair>();
    public List<GameObject> hairPrefabs = new List<GameObject>();
    public CharacterGender characterGender;
    public Transform head_root;
    public List<GameObject> Shirts = new List<GameObject>();
    public List<GameObject> Pants = new List<GameObject>();
    public List<GameObject> Shoes = new List<GameObject>();
    public List<GameObject> SkinColor = new List<GameObject>();
    public List<GameObject> Hair = new List<GameObject>();
    public List<Texture> TexturesWavy = new List<Texture>();
    public List<Texture> TexturesSpiky = new List<Texture>();
    public List<Texture> TexturesFlat = new List<Texture>();
    public GameObject hairPrefab;
    public int currentHairNumber;
    public HairType.HairTypes hp;
    public Transform hpparent;
    public List<HairPositions> hairPositions = new List<HairPositions>();
    public HairPositions currentHairPosition;
    // public Hair.HairType hairType;

    public GameObject headColliders;
    private void Start()
    {
        RandomizeClothes();
        SpawnHair();
    }
    [Button]
    public void SetHairToStart()
    {
        hairPrefab.transform.position = hairPositions[currentHairNumber].transform.GetChild(0).transform.position;
    }
    [Button]
    public void SetHairToTreatment()
    {
        hairPrefab.transform.position = hairPositions[currentHairNumber].transform.GetChild(1).transform.position;
    }

    [Button]
    public CharacterGender GetGender()
    {
        return this.characterGender;
    }
    [Button]
    public void RandomizeClothes()
    {

        int randomShirt = Random.Range(0, Shirts.Count);
        for (int i = 0; i < Shirts.Count; i++)
        {
            if (i == randomShirt)
                Shirts[i].SetActive(true);
            else
                Shirts[i].SetActive(false);
        }

        int randomPants = Random.Range(0, Pants.Count);
        for (int i = 0; i < Pants.Count; i++)
        {
            if (i == randomPants)
                Pants[i].SetActive(true);
            else
                Pants[i].SetActive(false);
        }

        int randomShoes = Random.Range(0, Shoes.Count);
        for (int i = 0; i < Shoes.Count; i++)
        {
            if (i == randomShoes)
                Shoes[i].SetActive(true);
            else
                Shoes[i].SetActive(false);
        }
    }
    public void ToggleHeadColliders()
    {
        headColliders.SetActive(!headColliders.activeSelf);
    }
    public void RaiseEvent()
    {
        StartMenuEvent.Raise();
    }
    public void SpawnHair()
    {
        Debug.Log("spawning hair");
        hairPrefab = Instantiate(hairPrefabs[Random.Range(0, hairPrefabs.Count)], head_root);
        //  hairPrefab = Instantiate(hairPrefabs[0], head_root);
        CharacterGenerator.Instance.currentHair = hairPrefab;
        for (int i = 0; i < hpparent.childCount; i++)
        {
            hairPositions.Add(hpparent.GetChild(i).GetComponent<HairPositions>());
        }
        hp = hairPrefab.GetComponent<Hair>().hairType;
        for (int i = 0; i < hairPositions.Count; i++)
        {
            if (hairPositions[i].hairType == hp)
            {
                hairPrefab.transform.position = hairPositions[i].transform.GetChild(0).transform.position;
                hairPrefab.transform.rotation = hairPositions[i].transform.GetChild(0).transform.rotation;
                currentHairPosition = hairPositions[i];
                currentHairNumber = i;
            }
        }
        hair = hairPrefab.GetComponent<Hair>();
        CharacterGenerator.Instance.currentHairScript = hair;
        Debug.Log("hair should be spawned HAIR   "+hair);
        Debug.Log("hair should be spawned HAIR PREFAB   "+hairPrefab);
        Debug.Log("Head Root" + head_root.gameObject.name);
        hair.RandomizeHair();
    }
}
