using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PaintIn3D;
using UnityEngine;

public class Hair : MonoBehaviour
{
    public GameObject decorations;
    public GameObject startingLiceParent;
    public GameObject fuzzyHairParent;
    public GameObject liceParent;
    public GameObject paintableHair;
    public GameObject hairPrefab, hairExtensionObject;
    public GameObject hairBase, leftCurl, rightCurl;
    public GameObject Garbage, Lice;
    public GameObject headColliders;
    public ParticleSystem glowParticles;
    public Material leftFuzz, middleFuzz, rightFuzz, hairMaterial, hairBaseMaterial, hairRootMaterial, headRootMaterial, hairBottomMaterial;
    public Material dyeableHairMaterial;
    public Character character;
    public int currentHeadRoot;
    public List<Texture> maleHairTextures = new List<Texture>();
    public List<Texture> maleHairRootTextures = new List<Texture>();
    public List<HairRoots> rootsTextures = new List<HairRoots>();
    public List<List<Texture>> hairRootTextures = new List<List<Texture>>();
    public List<Texture> hairBaseTextures = new List<Texture>();
    public List<Texture> hairBaseTextures_flat = new List<Texture>();
    public List<Texture> hairTexture = new List<Texture>();
    public List<Texture> hairTextureBottom = new List<Texture>();
    public List<Texture> leftFuzzTextures = new List<Texture>();
    public List<Texture> middleFuzzTextures = new List<Texture>();
    public List<Texture> rightFuzzTextures = new List<Texture>();
    public List<GameObject> physics = new List<GameObject>();
    public List<SkinnedMeshRenderer> fuzzyHair = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> extensionHair = new List<SkinnedMeshRenderer>();
    public List<GameObject> dyeableHair = new List<GameObject>();
    public CurlHair curlHair;
    public int garbageCount, fuzzCount, GarbagePart, FuzzPart;
    public float fuzzTotal;
    public HairType.HairTypes hairType;
    public int randomNumber;
    [Button]
    public void SetHairToStart()
    {
        hairPrefab.transform.position = character.hairPositions[0].transform.GetChild(0).transform.position;
        hairPrefab.transform.rotation = character.hairPositions[0].transform.GetChild(0).transform.rotation;
    }
    [Button]
    public void SetHairToTreatment()
    {
        hairPrefab.transform.position = character.hairPositions[0].transform.GetChild(1).transform.position;
        hairPrefab.transform.rotation = character.hairPositions[0].transform.GetChild(1).transform.rotation;
    }
    private void Start()
    {

    }
    public void RandomizeHair()
    {
        randomNumber = Random.Range(0, hairTexture.Count);
        // randomNumber = 0;
        UI_Manager.Instance.fuzzPart = Mathf.CeilToInt(fuzzCount);
        character = CharacterGenerator.Instance.character;
        Debug.Log("Character");
        CharacterGenerator.Instance.extensionAnimator = hairExtensionObject.GetComponent<Animator>();
        Debug.Log("Extension animator" + CharacterGenerator.Instance.extensionAnimator);
        hairMaterial.SetTexture("_BaseMap", hairTexture[randomNumber]);
        leftFuzz.SetTexture("_BaseMap", leftFuzzTextures[randomNumber]);
        rightFuzz.SetTexture("_BaseMap", rightFuzzTextures[randomNumber]);
        middleFuzz.SetTexture("_BaseMap", middleFuzzTextures[randomNumber]);
        dyeableHairMaterial.SetTexture("_MainTex", hairTexture[randomNumber]);
        hairBaseMaterial.SetTexture("_BaseMap", hairBaseTextures[randomNumber]);
        hairRootMaterial.SetTexture("BaseMap", hairBaseTextures[randomNumber]);
        headRootMaterial.SetTexture("_BaseMap", rootsTextures[0].hairRootTextures[randomNumber]);

        currentHeadRoot = randomNumber;


        for (int i = 0; i < Garbage.transform.childCount; i++)
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                Garbage.transform.GetChild(i).gameObject.SetActive(true);
                garbageCount++;
            }
            else
                Garbage.transform.GetChild(i).gameObject.SetActive(false);
        }
        GarbagePart = GetGarbagePart();
        fuzzCount = fuzzyHair.Count;
        FuzzPart = GetFuzzPart();
        Debug.Log("Hair Spawned");
        if (CharacterGenerator.Instance.character.characterGender == Character.CharacterGender.Female)
        {
            hairBottomMaterial.SetTexture("_BaseMap", hairTextureBottom[randomNumber]);
        }
    }
    [Button]
    public void FinishCurl()
    {
        hairBase.SetActive(true);
    }
    public void EnableFuzzColliders()
    {
        foreach (SkinnedMeshRenderer fuzz in fuzzyHair)
        {
            fuzz.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    public float GetFuzzCount()
    {
        int temp = 0;
        foreach (SkinnedMeshRenderer fuzz in fuzzyHair)
        {
            if (fuzz.gameObject.activeSelf)
            {
                temp++;
            }
        }
        return temp;
    }
    public int GetGarbagePart()
    {

        float GarbageCount = garbageCount;

        float part = 100f / GarbageCount;

        return Mathf.CeilToInt(part);
    }
    public int GetFuzzPart()
    {
        float FuzzCount = fuzzCount;

        float part = 100f / FuzzCount;

        return Mathf.CeilToInt(part);
    }
    [Button]
    public void TransitionRoots()
    {
        StartCoroutine(TransitionRootsCoroutine());
    }
    public IEnumerator TransitionRootsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CharacterGenerator.Instance.headrootParticles.Play();

        foreach (HairRoots roots in rootsTextures)
        {
            headRootMaterial.SetTexture("_BaseMap", roots.hairRootTextures[currentHeadRoot]);
            yield return new WaitForSeconds(0.75f);

        }
        if (CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Female)
        {

            character.hairBase.SetActive(false);
            hairExtensionObject.SetActive(true);
        }
        CameraController.Instance.SittingCamera(1f);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ExtensionPhase.SetActive(true);
        UI_Manager.Instance.extensionPanel.SetActive(true);
    }
}

[System.Serializable]
public class HairRoots
{
    public List<Texture> hairRootTextures = new List<Texture>();
}

