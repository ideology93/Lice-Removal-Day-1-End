using System.Collections;
using System.Collections.Generic;
using MagicaCloth;
using NaughtyAttributes;
using UnityEngine;

public class CharacterGenerator : GloballyAccessibleBase<CharacterGenerator>
{
    public Hair currentHairScript;
    public GameObject currentHair;
    public Character character;
    public GameObject characterFemale, characterMale;
    public GameObject characterPrefab;
    public GameObject headRootObject;
    public Animator extensionAnimator;
    public ParticleSystem headrootParticles;
    public GameObject characterGameObject;
    public Transform sittingPosition, sittingPosition2;
    public List<MagicaBoneCloth> mbcloth = new List<MagicaBoneCloth>();
    public Transform characterStartPosition;

    private void Start()
    {
        KillCharacter();
        Invoke("Spawn", 0.5f);
    }

    [Button]
    public void SpawnRandomCharacter()
    {
        KillCharacter();
        Invoke("Spawn", 0.5f);
    }
    [Button]
    public void KillCharacter()
    {
        if (characterGameObject != null)
        {
            Destroy(characterGameObject);
        }

    }

    public GameObject RandomizeGender()
    {
        int genderNumber = Random.Range(0, 3);
        if (genderNumber == 0)
            return characterMale;
        else return characterFemale;
        // return characterFemale;
        


    }
    public void Sit()
    {
        if (characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Female)
            characterGameObject.GetComponent<Character>().hairBase.SetActive(true);
        characterGameObject.GetComponent<Animator>().Play("Sit");
        characterGameObject.transform.position = sittingPosition.position;
        characterGameObject.transform.rotation = sittingPosition.rotation;
        CameraController.Instance.StartScanCamera(ToolManager.Instance.scanEvent);
        // CameraController.Instance.SittingCamera(0);
        characterGameObject.GetComponent<Character>().dirtyParticles.SetActive(false);

        characterGameObject.GetComponent<Character>().hairPrefab.transform.position = characterGameObject.GetComponent<Character>().currentHairPosition.transform.GetChild(1).transform.position;
        if (characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Male)
        {
            characterGameObject.GetComponent<Character>().hairPrefab.transform.rotation = characterGameObject.GetComponent<Character>().currentHairPosition.transform.GetChild(1).transform.rotation;
        }
        // TurnOnBase();
        UI_Manager.Instance.step0.SetActive(true);
        if (currentHairScript.decorations != null)
        {
            currentHairScript.decorations.SetActive(false);
        }

    }

    public void ToggleHeadColliders()
    {
        characterGameObject.GetComponent<Character>().ToggleHeadColliders();
    }
    public void EnableFuzzColliders()
    {
        currentHairScript.GetComponent<Hair>().EnableFuzzColliders();

    }
    public void StartCombing()
    {
        CombController.Instance.StartCombing();
    }
    public void TurnOnBase()
    {
        characterGameObject.GetComponent<Character>().hair.hairBase.SetActive(true);
    }
    public void Spawn()
    {
        Debug.Log("Spawning");



        characterGameObject = Instantiate(RandomizeGender(), characterStartPosition.position, characterStartPosition.rotation);
        character = characterGameObject.GetComponent<Character>();
        GameManager.Instance.character = character;
        foreach (MagicaBoneCloth mbc in mbcloth)
        {
            mbc.AddCollider(characterGameObject.GetComponent<MagicaCapsuleCollider>());
        }
        Debug.Log("Spawning");
    }
}
